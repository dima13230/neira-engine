using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NeiraEngine;
using NeiraEngine.Render;
using NeiraEngine.Render.OpenGL;

namespace NativeOpenGL
{
    public class fx_DepthOfField : RenderEffect
    {
        // Properties
        private const float _max_blur = 150.0f;
        private const float _focus_length = 10.0f;
        private const float _fStop = 0.15f;
        private const float _sensor_width = 33.0f;
        private float _PPM;

        private const int _bokeh_max_shapes = 35000;
        private int _bokeh_indirect_buffer = 0;
        private int _bokeh_vao = 0;

        struct DrawArraysIndirectCommand
        {
            public uint count;
            public uint primCount;
            public uint first;
            public uint reservedMustBeZero;
        }
        private DrawArraysIndirectCommand _bokeh_point_counter;

        // Programs
        private Program _pAutoFocus;
        private Program _pCOC;
        private Program _pCOC_Fix;
        private Program _pCOC_Combine;
        private Program _pBokeh_Reset;
        private Program _pBokeh_Extract;
        private Program _pBokeh_Render;
        private Program _pDOF_Blur;
        private Program _pDOF_Blend;

        // Frame Buffers
        public FrameBuffer _fHalfResolution;
        private FrameBuffer _fFullResoution;

        // Textures - COC

        private Texture _tCOC;
        public Texture tCOC
        {
            get { return _tCOC; }
        }

        private Texture _tCOC_Foreground_Blurred;
        public Texture tCOC_Foreground_Blurred
        {
            get { return _tCOC_Foreground_Blurred; }
        }

        private Texture _tCOC_Foreground;
        public Texture tCOC_Foreground
        {
            get { return _tCOC_Foreground; }
        }

        private Texture _tCOC_Foreground_Final;
        public Texture tCOC_Foreground_Final
        {
            get { return _tCOC_Foreground_Final; }
        }


        private Texture _tCOC_Final;
        public Texture tCOC_Final
        {
            get { return _tCOC_Final; }
        }

        // Textures - Bokeh
        private Image _iBokehShape;

        private Texture _tBokeh_Positions;
        private Texture _tBokeh_Colors;

        private Texture _tBokeh_Points;
        public Texture tBokeh_Points
        {
            get { return _tBokeh_Points; }
        }

        public Texture tDOF_Scene { get; private set; }
        public Texture tDOF_Scene_2 { get; private set; }


        // Other Buffers
        private ShaderStorageBuffer _ssboAutoFocus;


        public fx_DepthOfField(ProgramLoader pLoader, StaticImageLoader tLoader, string resource_folder_name, Resolution full_resolution)
            : base(pLoader, tLoader, resource_folder_name, full_resolution)
        {
            _PPM = (float)Math.Sqrt((_resolution_half.W_2) + (_resolution_half.H_2)) / _sensor_width;
        }

        protected override void load_Programs()
        {
            string[] autofocus_helpers = new string[]
            {
                EngineHelper.path_glsl_common_helper_interpolation
            };
            _pAutoFocus = _pLoader.createProgram(new ShaderFile[]
            {
                new ShaderFile(ShaderType.ComputeShader, _path_glsl_effect + "dof_AutoFocus.comp", autofocus_helpers)
            });
            _pAutoFocus.enable_Samplers(1);
            _pAutoFocus.addUniform("focus_delay");

            _pCOC = _pLoader.createProgram_PostProcessing(new ShaderFile[]
            {
                new ShaderFile(ShaderType.FragmentShader, _path_glsl_effect + "dof_COC.frag", null)
            });
            _pCOC.enable_Samplers(1);
            _pCOC.addUniform("PPM");
            _pCOC.addUniform("focus_length");
            _pCOC.addUniform("fStop");
            _pCOC.addUniform("max_blur");

            _pCOC_Fix = _pLoader.createProgram_PostProcessing(new ShaderFile[]
            {
                new ShaderFile(ShaderType.FragmentShader, _path_glsl_effect + "dof_COC_Fix.frag", null)
            });
            _pCOC_Fix.enable_Samplers(2);

            _pCOC_Combine = _pLoader.createProgram_PostProcessing(new ShaderFile[]
            {
                new ShaderFile(ShaderType.FragmentShader, _path_glsl_effect + "dof_COC_Combine.frag", null)
            });
            _pCOC_Combine.enable_Samplers(3);


            _pBokeh_Reset = _pLoader.createProgram(new ShaderFile[]
            {
                new ShaderFile(ShaderType.ComputeShader, _path_glsl_effect + "dof_Bokeh_Reset.comp", null)
            });

            _pBokeh_Extract = _pLoader.createProgram_PostProcessing(new ShaderFile[]
            {
                new ShaderFile(ShaderType.FragmentShader, _path_glsl_effect + "dof_Bokeh_Extract.frag", null)
            });
            _pBokeh_Extract.enable_Samplers(5);
            _pBokeh_Extract.addUniform("bokeh_counter");


            _pBokeh_Render = _pLoader.createProgram(new ShaderFile[]
            {
                new ShaderFile(ShaderType.VertexShader, _path_glsl_effect + "dof_Bokeh_Render.vert", null),
                new ShaderFile(ShaderType.GeometryShader, _path_glsl_effect + "dof_Bokeh_Render.geom", null),
                new ShaderFile(ShaderType.FragmentShader, _path_glsl_effect + "dof_Bokeh_Render.frag", null)
            });
            _pBokeh_Render.enable_Samplers(5);
            _pBokeh_Render.addUniform("texture_size");
            _pBokeh_Render.addUniform("max_blur");

            //_pDOF_Blur = _pLoader.createProgram_PostProcessing(new ShaderFile[]
            //{
            //    new ShaderFile(ShaderType.FragmentShader, _path_glsl_effect + "dof_DOF_Blur.frag", null)
            //});
            _pDOF_Blur = _pLoader.createProgram(new ShaderFile[]
            {
                new ShaderFile(ShaderType.ComputeShader, _path_glsl_effect + "dof_DOF_Blur.comp", null)
            });
            _pDOF_Blur.enable_Samplers(3);
            _pDOF_Blur.addUniform("texture_size");
            _pDOF_Blur.addUniform("max_blur");
            _pDOF_Blur.addUniform("direction_selector");

            _pDOF_Blend = _pLoader.createProgram_PostProcessing(new ShaderFile[]
            {
                new ShaderFile(ShaderType.FragmentShader, _path_glsl_effect + "dof_DOF_Blend.frag", null)
            });
            _pDOF_Blend.enable_Samplers(4);
        }

        protected override void load_Buffers()
        {
            //------------------------------------------------------
            // AutoFocus
            //------------------------------------------------------

            _ssboAutoFocus = new ShaderStorageBuffer(new EngineHelper.size[]
            {
                EngineHelper.size.vec2
            });

            //------------------------------------------------------
            // COC
            //------------------------------------------------------
            _tCOC = new Texture(TextureTarget.Texture2D,
                _resolution_half.W, _resolution_half.H, 0,
                false, false,
                PixelInternalFormat.R32f, PixelFormat.Red, PixelType.Float,
                TextureMinFilter.Linear, TextureMagFilter.Linear, TextureWrapMode.Clamp);
            _tCOC.load();

            _tCOC_Foreground_Blurred = new Texture(TextureTarget.Texture2D,
                _resolution_half.W, _resolution_half.H, 0,
                false, false,
                PixelInternalFormat.R32f, PixelFormat.Red, PixelType.Float,
                TextureMinFilter.Linear, TextureMagFilter.Linear, TextureWrapMode.Clamp);
            _tCOC_Foreground_Blurred.load();

            _tCOC_Foreground = new Texture(TextureTarget.Texture2D,
                _resolution_half.W, _resolution_half.H, 0,
                false, false,
                PixelInternalFormat.R32f, PixelFormat.Red, PixelType.Float,
                TextureMinFilter.Linear, TextureMagFilter.Linear, TextureWrapMode.Clamp);
            _tCOC_Foreground.load();

            _tCOC_Foreground_Final = new Texture(TextureTarget.Texture2D,
                _resolution_half.W, _resolution_half.H, 0,
                false, false,
                PixelInternalFormat.R32f, PixelFormat.Red, PixelType.Float,
                TextureMinFilter.Linear, TextureMagFilter.Linear, TextureWrapMode.Clamp);
            _tCOC_Foreground_Final.load();

            _tCOC_Final = new Texture(TextureTarget.Texture2D,
                _resolution.W, _resolution.H, 0,
                false, false,
                PixelInternalFormat.R32f, PixelFormat.Red, PixelType.Float,
                TextureMinFilter.Linear, TextureMagFilter.Linear, TextureWrapMode.Clamp);
            _tCOC_Final.load();

            //------------------------------------------------------
            // Bokeh
            //------------------------------------------------------
            _iBokehShape = _tLoader.createImage(_path_static_textures + "bokeh_circle.png", TextureTarget.Texture2D, TextureWrapMode.Clamp, true);

            _tBokeh_Positions = new Texture(TextureTarget.Texture1D,
                _bokeh_max_shapes, 0, 0,
                false, false,
                PixelInternalFormat.Rgba32f, PixelFormat.Rgba, PixelType.Float,
                TextureMinFilter.Nearest, TextureMagFilter.Nearest, TextureWrapMode.Clamp);
            _tBokeh_Positions.load();

            _tBokeh_Colors = new Texture(TextureTarget.Texture1D,
                _bokeh_max_shapes, 0, 0,
                false, false,
                PixelInternalFormat.Rgba32f, PixelFormat.Rgba, PixelType.Float,
                TextureMinFilter.Nearest, TextureMagFilter.Nearest, TextureWrapMode.Clamp);
            _tBokeh_Colors.load();

            _tBokeh_Points = new Texture(TextureTarget.Texture2D,
                _resolution_half.W, _resolution_half.H, 0,
                false, false,
                PixelInternalFormat.Rgb16f, PixelFormat.Rgb, PixelType.Float,
                TextureMinFilter.Linear, TextureMagFilter.Linear, TextureWrapMode.Clamp);
            _tBokeh_Points.load();


            load_BuffersBokeh();


            //------------------------------------------------------
            // DOF
            //------------------------------------------------------
            tDOF_Scene = new Texture(TextureTarget.Texture2D,
                _resolution_half.W, _resolution_half.H, 0,
                false, false,
                PixelInternalFormat.Rgba16f, PixelFormat.Rgba, PixelType.Float,
                TextureMinFilter.Linear, TextureMagFilter.Linear, TextureWrapMode.Clamp);
            tDOF_Scene.load();

            tDOF_Scene_2 = new Texture(TextureTarget.Texture2D,
                _resolution_half.W, _resolution_half.H, 0,
                false, false,
                PixelInternalFormat.Rgba16f, PixelFormat.Rgba, PixelType.Float,
                TextureMinFilter.Linear, TextureMagFilter.Linear, TextureWrapMode.Clamp);
            tDOF_Scene_2.load();

            //------------------------------------------------------
            // Frame Buffers
            //------------------------------------------------------
            _fHalfResolution = new FrameBuffer("DOF - Half Resolution");
            _fHalfResolution.load(new Dictionary<FramebufferAttachment, Texture>()
            {
                { FramebufferAttachment.ColorAttachment0, _tCOC },
                { FramebufferAttachment.ColorAttachment1, _tCOC_Foreground_Blurred },
                { FramebufferAttachment.ColorAttachment2, _tCOC_Foreground },
                { FramebufferAttachment.ColorAttachment3, _tCOC_Foreground_Final },
                { FramebufferAttachment.ColorAttachment4, _tBokeh_Points },
                { FramebufferAttachment.ColorAttachment5, tDOF_Scene },
                { FramebufferAttachment.ColorAttachment6, tDOF_Scene_2 }
            });

            _fFullResoution = new FrameBuffer("DOF - Full Resolution");
            _fFullResoution.load(new Dictionary<FramebufferAttachment, Texture>()
            {
                { FramebufferAttachment.ColorAttachment0, _tCOC_Final }
            });

        }

        private void load_BuffersBokeh()
        {
            //INDIRECT BUFFER
            OGL.GenBuffers(1, out _bokeh_indirect_buffer);
            OGL.BindBuffer(BufferTarget.DrawIndirectBuffer, _bokeh_indirect_buffer);

            _bokeh_point_counter.count = 1;
            _bokeh_point_counter.primCount = 0;
            _bokeh_point_counter.first = 0;
            _bokeh_point_counter.reservedMustBeZero = 0;

            int icmdSize = System.Runtime.InteropServices.Marshal.SizeOf(_bokeh_point_counter);

            OGL.BufferData(BufferTarget.DrawIndirectBuffer, (IntPtr)icmdSize, ref _bokeh_point_counter, BufferUsageHint.DynamicDraw);

            // Ghost VAO for indirect draw calls later
            float[] temp = { 0.0f, 0.0f, 0.0f, 0.0f };

            int bSize = 4 * sizeof(float);
            int bokeh_vbo = 0;
            OGL.GenBuffers(1, out bokeh_vbo);
            OGL.BindBuffer(BufferTarget.ArrayBuffer, bokeh_vbo);
            OGL.BufferData(
                BufferTarget.ArrayBuffer,
                (IntPtr)bSize,
                temp,
                BufferUsageHint.StaticDraw);

            OGL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            _bokeh_vao = 0;
            OGL.GenVertexArrays(1, out _bokeh_vao);
            OGL.BindVertexArray(_bokeh_vao);

            OGL.BindBuffer(BufferTarget.ArrayBuffer, bokeh_vbo);
            OGL.EnableVertexAttribArray(0);
            OGL.VertexAttribPointer(0, 4, VertexAttribPointerType.Float, false, 0, 0);

            OGL.BindVertexArray(0);
        }

        public override void load()
        {
            load_Programs();
            load_Buffers();
        }

        public override void unload()
        {

        }

        public override void reload()
        {

        }

        //------------------------------------------------------
        // Auto Focus
        //------------------------------------------------------
        private void autoFocus(Texture depth_texture)
        {
            _pAutoFocus.bind();

            _ssboAutoFocus.bind(0);
            depth_texture.bind(_pAutoFocus.getSamplerUniform(0), 0);

            OGL.DispatchCompute(1, 1, 1);
            OGL.MemoryBarrier(MemoryBarrierFlags.ShaderStorageBarrierBit);
        }

        private void printFocusDistance()
        {
            int exp_size = System.Runtime.InteropServices.Marshal.SizeOf(typeof(Vector2));

            OGL.MemoryBarrier(MemoryBarrierFlags.AllBarrierBits);
            Vector2 lumRead = new Vector2();

            _ssboAutoFocus.bind();
            OGL.GetBufferSubData(BufferTarget.ShaderStorageBuffer, (IntPtr)0, exp_size, ref lumRead);

            Debug.logInfo(1, "Focus Distance", lumRead.ToString());
        }

        //------------------------------------------------------
        // COC
        //------------------------------------------------------
        private void genCOC(fx_Quad quad, fx_Special special, Texture depth_texture)
        {
            //------------------------------------------------------
            // Calculate COC
            //------------------------------------------------------

            _fHalfResolution.bind(new DrawBuffersEnum[]
            {
                DrawBuffersEnum.ColorAttachment0,
                DrawBuffersEnum.ColorAttachment1,
                DrawBuffersEnum.ColorAttachment2
            });
            OGL.Clear(ClearBufferMask.ColorBufferBit);
            OGL.Viewport(0, 0, _tCOC.width, _tCOC.height);

            _pCOC.bind();


            _ssboAutoFocus.bind(0);
            depth_texture.bind(_pCOC.getSamplerUniform(0), 0);

            OGL.Uniform(_pCOC.getUniform("PPM"), _PPM);
            OGL.Uniform(_pCOC.getUniform("focus_length"), _focus_length);
            OGL.Uniform(_pCOC.getUniform("fStop"), _fStop);
            OGL.Uniform(_pCOC.getUniform("max_blur"), _max_blur);

            quad.render();

            special.blur_Gauss(quad, 60, _tCOC_Foreground_Blurred, _fHalfResolution, DrawBuffersEnum.ColorAttachment1);


            //------------------------------------------------------
            // Fix COC
            //------------------------------------------------------

            _fHalfResolution.bind(new DrawBuffersEnum[]
            {
                DrawBuffersEnum.ColorAttachment3,
            });
            OGL.Clear(ClearBufferMask.ColorBufferBit);
            OGL.Viewport(0, 0, _tCOC.width, _tCOC.height);

            _pCOC_Fix.bind();

            _tCOC_Foreground.bind(_pCOC_Fix.getSamplerUniform(0), 0);
            _tCOC_Foreground_Blurred.bind(_pCOC_Fix.getSamplerUniform(1), 1);

            quad.render();

            special.blur_Gauss(quad, 10, _tCOC_Foreground_Final, _fHalfResolution, DrawBuffersEnum.ColorAttachment3);


            //------------------------------------------------------
            // Combine COC
            //------------------------------------------------------
            _fFullResoution.bind(new DrawBuffersEnum[]
            {
                DrawBuffersEnum.ColorAttachment0,
            });
            OGL.Clear(ClearBufferMask.ColorBufferBit);
            OGL.Viewport(0, 0, _tCOC_Final.width, _tCOC_Final.height);

            _pCOC_Combine.bind();

            _tCOC.bind(_pCOC_Combine.getSamplerUniform(0), 0);
            _tCOC_Foreground_Final.bind(_pCOC_Combine.getSamplerUniform(1), 1);

            quad.render();

        }


        //------------------------------------------------------
        // Bokeh
        //------------------------------------------------------
        private void resetBokeh()
        {
            OGL.MemoryBarrier(MemoryBarrierFlags.AllBarrierBits);

            _pBokeh_Reset.bind();

            OGL.BindBufferBase(BufferRangeTarget.ShaderStorageBuffer, 0, _bokeh_indirect_buffer);
            OGL.DispatchCompute(1, 1, 1);

            OGL.MemoryBarrier(MemoryBarrierFlags.ShaderStorageBarrierBit);
            OGL.BindBufferBase(BufferRangeTarget.ShaderStorageBuffer, 0, 0);
        }

        private void printBokehCount()
        {
            OGL.MemoryBarrier(MemoryBarrierFlags.AllBarrierBits);

            int icmdSize = System.Runtime.InteropServices.Marshal.SizeOf(_bokeh_point_counter);
            DrawArraysIndirectCommand temp_bokeh_point_counter = new DrawArraysIndirectCommand();

            OGL.BindBuffer(BufferTarget.DrawIndirectBuffer, _bokeh_indirect_buffer);
            OGL.GetBufferSubData(BufferTarget.DrawIndirectBuffer, (IntPtr)0, icmdSize, ref temp_bokeh_point_counter);

            Debug.logInfo(1, "Bokeh Count", temp_bokeh_point_counter.primCount.ToString());
        }

        private void extractBokeh(fx_Quad quad, Texture depth_texture, Texture scene_texture)
        {
            _fHalfResolution.bind(DrawBuffersEnum.ColorAttachment5);
            OGL.Clear(ClearBufferMask.ColorBufferBit);
            OGL.Viewport(0, 0, tDOF_Scene.width, tDOF_Scene.height);

            _pBokeh_Extract.bind();

            scene_texture.bind(_pBokeh_Extract.getSamplerUniform(0), 0);
            depth_texture.bind(_pBokeh_Extract.getSamplerUniform(1), 1);
            _tCOC_Final.bind(_pBokeh_Extract.getSamplerUniform(2), 2);

            _tBokeh_Positions.bindImageUnit(_pBokeh_Extract.getSamplerUniform(3), 3, TextureAccess.WriteOnly);
            _tBokeh_Colors.bindImageUnit(_pBokeh_Extract.getSamplerUniform(4), 4, TextureAccess.WriteOnly);

            OGL.BindBufferRange(BufferRangeTarget.AtomicCounterBuffer, 0, _bokeh_indirect_buffer, (IntPtr)4, (IntPtr)sizeof(uint));
            OGL.Uniform(_pBokeh_Extract.getUniform("bokeh_counter"), 0);

            quad.render();

            OGL.MemoryBarrier(MemoryBarrierFlags.AllBarrierBits);
        }

        private void genBokeh(Texture depth_texture)
        {
            _fHalfResolution.bind(DrawBuffersEnum.ColorAttachment4);
            OGL.Clear(ClearBufferMask.ColorBufferBit);
            OGL.Viewport(0, 0, _tBokeh_Points.width, _tBokeh_Points.height);

            _pBokeh_Render.bind();


            _iBokehShape.bind(_pBokeh_Render.getSamplerUniform(0), 0);
            depth_texture.bind(_pBokeh_Render.getSamplerUniform(1), 1);
            _tCOC_Final.bind(_pBokeh_Render.getSamplerUniform(2), 2);

            OGL.Uniform(_pBokeh_Render.getUniform("texture_size"), 1.0f / _tBokeh_Points.width, 1.0f / _tBokeh_Points.height);
            OGL.Uniform(_pBokeh_Render.getUniform("max_blur"), _max_blur);

            OGL.Enable(EnableCap.Blend);
            OGL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.One);

            OGL.BindVertexArray(_bokeh_vao);
            OGL.BindBuffer(BufferTarget.DrawIndirectBuffer, _bokeh_indirect_buffer);
            OGL.DrawArraysIndirect(PrimitiveType.Points, (IntPtr)null);
            OGL.BindVertexArray(0);

            OGL.Disable(EnableCap.Blend);
        }

        //------------------------------------------------------
        // Depth Of Field
        //------------------------------------------------------
        private void genDOF(fx_Quad quad, Texture depth_texture)
        {

            _pDOF_Blur.bind();
            OGL.Uniform(_pDOF_Blur.getUniform("max_blur"), _max_blur);


            OGL.Uniform(_pDOF_Blur.getUniform("texture_size"), tDOF_Scene.dimensions.Xy);
            _tCOC_Final.bind(_pDOF_Blur.getSamplerUniform(2), 2);

            int fragmentation = 2;

            //------------------------------------------------------
            // Horizontal
            //------------------------------------------------------
            OGL.Uniform(_pDOF_Blur.getUniform("direction_selector"), 0);
            tDOF_Scene.bind(_pDOF_Blur.getSamplerUniform(0), 0);
            tDOF_Scene_2.bindImageUnit(_pDOF_Blur.getSamplerUniform(1), 1, TextureAccess.WriteOnly);

            OGL.DispatchCompute((int)tDOF_Scene.dimensions.Y, fragmentation, 1);

            OGL.MemoryBarrier(MemoryBarrierFlags.ShaderImageAccessBarrierBit);

            //------------------------------------------------------
            // Vertical
            //------------------------------------------------------
            OGL.Uniform(_pDOF_Blur.getUniform("direction_selector"), 1);
            tDOF_Scene_2.bind(_pDOF_Blur.getSamplerUniform(0), 0);
            tDOF_Scene.bindImageUnit(_pDOF_Blur.getSamplerUniform(1), 1, TextureAccess.WriteOnly);

            OGL.DispatchCompute((int)tDOF_Scene.dimensions.X, fragmentation, 1);

            OGL.MemoryBarrier(MemoryBarrierFlags.ShaderImageAccessBarrierBit);

        }

        private void blendDOF(fx_Quad quad, fx_Special special, FrameBuffer scene_fbo, Texture scene_texture)
        {
            OGL.CopyImageSubData(scene_texture.id, ImageTarget.Texture2D, 0, 0, 0, 0,
                    special.tSpecial.id, ImageTarget.Texture2D, 0, 0, 0, 0,
                    _resolution.W, _resolution.H, 1);

            scene_fbo.bind(DrawBuffersEnum.ColorAttachment0);
            OGL.Clear(ClearBufferMask.ColorBufferBit);
            OGL.Viewport(0, 0, _resolution.W, _resolution.H);

            _pDOF_Blend.bind();

            special.tSpecial.bind(_pDOF_Blend.getSamplerUniform(0), 0);
            tDOF_Scene.bind(_pDOF_Blend.getSamplerUniform(1), 1);
            _tCOC_Final.bind(_pDOF_Blend.getSamplerUniform(2), 2);
            _tBokeh_Points.bind(_pDOF_Blend.getSamplerUniform(3), 3);

            quad.render();
        }


        public void render(fx_Quad quad, fx_Special special, Texture depth_texture, FrameBuffer scene_fbo, Texture scene_texture)
        {
            autoFocus(depth_texture);

            //printFocusDistance();
            genCOC(quad, special, depth_texture);

            resetBokeh();
            extractBokeh(quad, depth_texture, scene_texture);
            //printBokehCount();
            genBokeh(depth_texture);

            genDOF(quad, depth_texture);
            blendDOF(quad, special, scene_fbo, scene_texture);
        }
    }
}

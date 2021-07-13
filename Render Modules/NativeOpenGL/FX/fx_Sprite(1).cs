using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NeiraEngine;
using NeiraEngine.Render;
using NeiraEngine.Render.OpenGL;
using NeiraEngine.Output;

namespace NativeOpenGL
{
    public class fx_Sprite : RenderEffect
    {

        private Matrix4 projection;

        private int _vao;

        // Program
        private Program _pRenderSprite;

        public fx_Sprite(string glsl_effect_path, Resolution full_resolution)
            : base(glsl_effect_path, full_resolution)
        { }

        protected override void load_Programs()
        {
            string[] texture_cube_helpers = new string[]
            {
                EngineHelper.path_glsl_common_ubo_cameraSpatials
            };

            _pRenderSprite = ProgramLoader.createProgram_PostProcessing(new ShaderFile[]
            {
                new ShaderFile(ShaderType.FragmentShader, _path_glsl_effect + "sprite_Render.frag", null)
            });
            _pRenderSprite.enable_Samplers(3);
            _pRenderSprite.addUniform("model");
            _pRenderSprite.addUniform("projection");
            _pRenderSprite.addUniform("spriteColor");
        }

        public void updateProjection(Matrix4 value)
        {
            if(projection != value)
            {
                projection = value;
                OGL.Uniform(_pRenderSprite.getUniform("projection"), true, projection);
            }
        }

        protected override void load_Buffers()
        {
            float[] temp = {
                  -1.0f, -1.0f, 0.0f,
                  3.0f, -1.0f, 0.0f,
                  -1.0f, 3.0f, 0.0f
            };

            int bSize = sizeof(float) * temp.Length;

            OGL.GenVertexArrays(1, out _vao);

            int vbo = 0;
            OGL.GenBuffers(1, out vbo);
            OGL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            OGL.BufferData(
                BufferTarget.ArrayBuffer,
                (IntPtr)bSize,
                temp,
                BufferUsageHint.StaticDraw);

            OGL.BindVertexArray(_vao);
            OGL.EnableVertexAttribArray(0);
            OGL.VertexAttribPointer(0, 4, VertexAttribPointerType.Float, false, 0, 0);

            OGL.BindBuffer(BufferTarget.ArrayBuffer, 0);
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
        // Render Full Screen Quad
        //------------------------------------------------------

        public void render()
        {
            OGL.BindVertexArray(_vao);
            OGL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            OGL.BindVertexArray(0);
        }


        public void renderBlend_Blend()
        {
            OGL.Enable(EnableCap.Blend);
            OGL.BlendEquation(BlendEquationMode.FuncAdd);
            OGL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.One);

            render();

            OGL.Disable(EnableCap.Blend);
        }

        // Renders full quad instead of hacked triangles
        public void renderFullQuad()
        {
            OGL.BindVertexArray(_vao);
            OGL.DrawArrays(PrimitiveType.Triangles, 0, 6);
            OGL.BindVertexArray(0);
        }

        public void renderFullQuad_Blend()
        {
            OGL.Enable(EnableCap.Blend);
            OGL.BlendEquation(BlendEquationMode.FuncAdd);
            OGL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.One);

            renderFullQuad();

            OGL.Disable(EnableCap.Blend);
        }


        //------------------------------------------------------
        // Render Textures
        //------------------------------------------------------

        public void render_Texture(Texture texture, int channel = -1)
        {
            render_Texture(texture, Vector3.One, Vector2.One, new Vector2(), 0, channel);
        }

        public void render_Texture(Texture texture, int layer = 0, int channel = -1)
        {
            render_Texture(texture, Vector3.One, Vector2.One, new Vector2(), layer, channel);
        }

        public void render_Texture(Texture texture, Vector3 color, Vector2 size, Vector2 position, float angle, int layer = 0, int channel = -1)
        {
            Matrix4 model = Matrix4.CreateTranslation(new Vector3(position));
            model *= Matrix4.CreateTranslation(new Vector3(0.5f * size.X, 0.5f * size.Y, 0.0f));
            model *= Matrix4.CreateRotationZ(angle);
            model *= Matrix4.CreateTranslation(new Vector3(-0.5f * size.X, -0.5f * size.Y, 0.0f));

            model *= Matrix4.CreateScale(new Vector3(size.X, size.Y, 1));

            // Render it!
            OGL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, 0);

            //GL.Enable(EnableCap.Blend);
            //GL.BlendFunc(BlendingFactorSrc.SrcColor, BlendingFactorDest.OneMinusSrcColor);

            //GL.Viewport(pos_x, pos_y, size_x, size_y);

            // Clamp requested layer to texture's depth
            layer = MathHelper.Clamp(layer, 0, texture.depth);
            channel = MathHelper.Clamp(channel, -1, 3);

            switch (texture.target)
            {
                case TextureTarget.Texture2D:
                    _pRenderSprite.bind();
                    texture.bind(_pRenderSprite.getSamplerUniform(0), 0);
                    OGL.Uniform(_pRenderSprite.getUniform("model"), true, model);
                    OGL.Uniform(_pRenderSprite.getUniform("spriteColor"), color);
                    break;
                default:
                    throw new Exception($"Render Texture: It's sprite renderer, so only Texture2D supported! For other cases use fx_Quad [ {texture.target.ToString()} ]");
            }

            render();
        }

    }
}
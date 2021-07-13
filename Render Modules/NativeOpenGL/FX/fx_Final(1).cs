﻿using System;
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
    public class fx_Final : RenderEffect
    {

        // Programs
        private Program _pFinalScene;

        // Frame Buffers
        private FrameBuffer _fFinalScene;
        public FrameBuffer fFinalScene
        {
            get
            {
                return _fFinalScene;
            }
        }
        
        // Textures
        private Texture _tFinalScene;
        public Texture tFinalScene
        {
            get
            {
                return _tFinalScene;
            }
        }




        public fx_Final(ProgramLoader pLoader, string glsl_effect_path, Resolution full_resolution)
            : base(pLoader, glsl_effect_path, full_resolution)
        { }

        protected override void load_Programs()
        {
            string[] final_helpers = new string[]
            {
                EngineHelper.path_glsl_common_helper_fxaa
            };

            // Render to screen and apply tone mapping and gamma correction
            _pFinalScene = _pLoader.createProgram_PostProcessing(new ShaderFile[]
            {
                new ShaderFile(ShaderType.FragmentShader, _path_glsl_effect + "final_Scene.frag", final_helpers)
            });
            _pFinalScene.enable_Samplers(1);
        }

        protected override void load_Buffers()
        {
            _tFinalScene = new Texture(TextureTarget.Texture2D,
                _resolution.W, _resolution.H,
                0, false, false,
                PixelInternalFormat.Rgba16f, PixelFormat.Rgba, PixelType.Float,
                TextureMinFilter.Linear, TextureMagFilter.Linear, TextureWrapMode.Clamp);
            _tFinalScene.load();

            _fFinalScene = new FrameBuffer("Final Scene");
            _fFinalScene.load(new Dictionary<FramebufferAttachment, Texture>()
            {
                { FramebufferAttachment.ColorAttachment0, _tFinalScene }
            });
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


        public void render(fx_Quad quad)
        {
            OGL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, 0);
            OGL.Clear(ClearBufferMask.ColorBufferBit);

            OGL.Viewport(0, 0, _resolution.W, _resolution.H);

            _pFinalScene.bind();

            _tFinalScene.bind(_pFinalScene.getSamplerUniform(0), 0);

            quad.render();
        }


    }
}

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
    public class fx_SkyBox : RenderEffect
    {

        // Programs
        private Program _pSkyBox;

        // Frame Buffers

        // Textures
        private Image _iSkyBox;
        public Image iSkyBox
        {
            get { return _iSkyBox; }
        }


        public fx_SkyBox(string resource_folder_name, Resolution full_resolution)
            : base(resource_folder_name, full_resolution)
        { }

        protected override void load_Programs()
        {
            string[] skybox_vert_helpers = new string[]
            {
                EngineHelper.path_glsl_common_ubo_cameraSpatials
            };
            string[] skybox_frag_helpers = new string[]
            {
                EngineHelper.path_glsl_common_ubo_gameConfig
            };

            _pSkyBox = ProgramLoader.createProgram(new ShaderFile[]
            {
                new ShaderFile(ShaderType.VertexShader, _path_glsl_effect + "skybox_Render.vert", skybox_vert_helpers),
                new ShaderFile(ShaderType.FragmentShader, _path_glsl_effect + "skybox_Render.frag", skybox_frag_helpers)
            });
            _pSkyBox.enable_Samplers(1);
            _pSkyBox.addUniform("circadian_position");
        }

        protected override void load_Buffers()
        {
            // Load Lens Images
            _iSkyBox = StaticImageLoader.createImage(
                new string[]{
                    _path_static_textures + "space_right1.png",
                    _path_static_textures + "space_left2.png",
                    _path_static_textures + "space_top3.png",
                    _path_static_textures + "space_bottom4.png",
                    _path_static_textures + "space_front5.png",
                    _path_static_textures + "space_back6.png"
                }, TextureTarget.TextureCubeMap, TextureWrapMode.ClampToEdge, true);
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


        public void render(fx_Quad quad, FrameBuffer gbuffer_fbo, Vector3 circadian_position)
        {
            // Write into gBuffer's frame buffer attachemnts
            gbuffer_fbo.bind(new DrawBuffersEnum[]
            {
                DrawBuffersEnum.ColorAttachment0,
                DrawBuffersEnum.ColorAttachment1,
                DrawBuffersEnum.ColorAttachment3
            });
            OGL.Viewport(0, 0, _resolution.W, _resolution.H);

            OGL.DepthMask(true);
            OGL.Enable(EnableCap.DepthTest);

            _pSkyBox.bind();

            _iSkyBox.bind(_pSkyBox.getSamplerUniform(0), 0);
            OGL.Uniform(_pSkyBox.getUniform("circadian_position"), Vector3.Normalize(circadian_position));

            quad.renderFullQuad();

        }
    }
}

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
    public class fx_AtmosphericScattering : RenderEffect
    {
        // Properties
        private const float SCALE = 1.0f;
        private const float Rg = 6360.0f * SCALE;
        private const float Rt = 6420.0f * SCALE;
        private const float RL = 6421.0f * SCALE;

        private const int TRANSMITTANCE_W = 256;
        private const int TRANSMITTANCE_H = 64;

        private const int SKY_W = 64;
        private const int SKY_H = 16;

        private const int RES_R = 32;
        private const int RES_MU = 128;
        private const int RES_MU_S = 32;
        private const int RES_NU = 8;

        private bool _precomputed;
        public bool precomputed
        {
            get { return _precomputed; }
        }



        // Programs
        private Program _pTransmittance;
        private Program _pIrradiance1;
        private Program _pInscatter1;
        private Program _pCopyIrradiance;
        private Program _pCopyInscatter1;
        private Program _pPJ;
        private Program _pIrradianceN;
        private Program _pInscatterN;
        private Program _pCopyInscatterN;

        private Program _pAtmoshpere;

        // Frame Buffers
        private FrameBuffer _fPrecompute;
        private FrameBuffer _fAtmosphere;

        // Textures
        private Texture _tTransmittance;
        public Texture tTransmittiance
        {
            get { return _tTransmittance; }
        }

        private Texture _tIrradiance;
        public Texture tIrradiance
        {
            get { return _tIrradiance; }
        }

        private Texture _tInscatter;
        public Texture tInscatter
        {
            get { return _tInscatter; }
        }

        private Texture _tDeltaE;
        public Texture tDeltaE
        {
            get { return _tDeltaE; }
        }

        private Texture _tDeltaSR;
        public Texture tDeltaSR
        {
            get { return _tDeltaSR; }
        }

        private Texture _tDeltaSM;
        public Texture tDeltaSM
        {
            get { return _tDeltaSM; }
        }

        private Texture _tDeltaJ;
        public Texture tDeltaJ
        {
            get { return _tDeltaJ; }
        }


        private Texture _tAtmoshpere;
        public Texture tAtmosphere
        {
            get { return _tAtmoshpere; }
        }




        public fx_AtmosphericScattering(string glsl_effect_path, Resolution full_resolution)
            : base(glsl_effect_path, full_resolution)
        {
            _precomputed = false;
        }

        protected override void load_Programs()
        {
            string[] ats_functions = new string[]
            {
                _path_glsl_effect + "helpers/ats_Functions.include"
            };

            string[] ats_atmposhere_vert_helpers = new string[]
            {
                EngineHelper.path_glsl_common_ubo_cameraSpatials
            };
            string[] ats_atmposhere_frag_helpers = new string[]
            {
                EngineHelper.path_glsl_common_ubo_cameraSpatials,
                EngineHelper.path_glsl_common_ubo_shadowMatrices_Directional,
                _path_glsl_effect + "helpers/ats_Functions.include",
                EngineHelper.path_glsl_common_helper_positionFromDepth,
                EngineHelper.path_glsl_common_helper_lightingFunctions,
                EngineHelper.path_glsl_common_helper_shadowEvaluation
            };

            //------------------------------------------------------
            // Precomputation Programs
            //------------------------------------------------------
            string _path_glsl_precomputation = _path_glsl_effect + "precomputation/";

            _pTransmittance = ProgramLoader.createProgram_PostProcessing(new ShaderFile[]
            {
                new ShaderFile(ShaderType.FragmentShader, _path_glsl_precomputation + "ats_Transmittance.frag", ats_functions)
            });

            _pIrradiance1 = ProgramLoader.createProgram_PostProcessing(new ShaderFile[]
            {
                new ShaderFile(ShaderType.FragmentShader, _path_glsl_precomputation + "ats_Irradiance1.frag", ats_functions)
            });
            _pIrradiance1.addUniform("transmittanceSampler");

            _pInscatter1 = ProgramLoader.createProgram_PostProcessing(new ShaderFile[]
            {
                new ShaderFile(ShaderType.GeometryShader, _path_glsl_precomputation + "ats_Precompute.geom", null),
                new ShaderFile(ShaderType.FragmentShader, _path_glsl_precomputation + "ats_Inscatter1.frag", ats_functions)
            });
            _pInscatter1.addUniform("transmittanceSampler");
            _pInscatter1.addUniform("r");
            _pInscatter1.addUniform("dhdH");
            _pInscatter1.addUniform("layer");

            _pCopyIrradiance = ProgramLoader.createProgram_PostProcessing(new ShaderFile[]
            {
                new ShaderFile(ShaderType.FragmentShader, _path_glsl_precomputation + "ats_CopyIrradiance.frag", ats_functions)
            });
            _pCopyIrradiance.addUniform("k");
            _pCopyIrradiance.addUniform("deltaESampler");

            _pCopyInscatter1 = ProgramLoader.createProgram_PostProcessing(new ShaderFile[]
            {
                new ShaderFile(ShaderType.GeometryShader, _path_glsl_precomputation + "ats_Precompute.geom", null),
                new ShaderFile(ShaderType.FragmentShader, _path_glsl_precomputation + "ats_CopyInscatter1.frag", ats_functions)
            });
            _pCopyInscatter1.addUniform("deltaSRSampler");
            _pCopyInscatter1.addUniform("deltaSMSampler");
            _pCopyInscatter1.addUniform("r");
            _pCopyInscatter1.addUniform("dhdH");
            _pCopyInscatter1.addUniform("layer");

            _pPJ = ProgramLoader.createProgram_PostProcessing(new ShaderFile[]
            {
                new ShaderFile(ShaderType.GeometryShader, _path_glsl_precomputation + "ats_Precompute.geom", null),
                new ShaderFile(ShaderType.FragmentShader, _path_glsl_precomputation + "ats_InscatterS.frag", ats_functions)
            });
            _pPJ.addUniform("first");
            _pPJ.addUniform("transmittanceSampler");
            _pPJ.addUniform("deltaESampler");
            _pPJ.addUniform("deltaSRSampler");
            _pPJ.addUniform("deltaSMSampler");
            _pPJ.addUniform("r");
            _pPJ.addUniform("dhdH");
            _pPJ.addUniform("layer");

            _pIrradianceN = ProgramLoader.createProgram_PostProcessing(new ShaderFile[]
            {
                new ShaderFile(ShaderType.FragmentShader, _path_glsl_precomputation + "ats_IrradianceN.frag", ats_functions)
            });
            _pIrradianceN.addUniform("first");
            _pIrradianceN.addUniform("transmittanceSampler");
            _pIrradianceN.addUniform("deltaESampler");
            _pIrradianceN.addUniform("deltaSRSampler");
            _pIrradianceN.addUniform("deltaSMSampler");

            _pInscatterN = ProgramLoader.createProgram_PostProcessing(new ShaderFile[]
            {
                new ShaderFile(ShaderType.GeometryShader, _path_glsl_precomputation + "ats_Precompute.geom", null),
                new ShaderFile(ShaderType.FragmentShader, _path_glsl_precomputation + "ats_InscatterN.frag", ats_functions)
            });
            _pInscatterN.addUniform("first");
            _pInscatterN.addUniform("transmittanceSampler");
            _pInscatterN.addUniform("deltaJSampler");
            _pInscatterN.addUniform("r");
            _pInscatterN.addUniform("dhdH");
            _pInscatterN.addUniform("layer");

            _pCopyInscatterN = ProgramLoader.createProgram_PostProcessing(new ShaderFile[]
            {
                new ShaderFile(ShaderType.GeometryShader, _path_glsl_precomputation + "ats_Precompute.geom", null),
                new ShaderFile(ShaderType.FragmentShader, _path_glsl_precomputation + "ats_CopyInscatterN.frag", ats_functions)
            });
            _pCopyInscatterN.addUniform("deltaSRSampler");
            _pCopyInscatterN.addUniform("r");
            _pCopyInscatterN.addUniform("dhdH");
            _pCopyInscatterN.addUniform("layer");


            //------------------------------------------------------
            // Atmoshperic Scattering Programs
            //------------------------------------------------------

            _pAtmoshpere = ProgramLoader.createProgram(new ShaderFile[]
            {
                new ShaderFile(ShaderType.VertexShader, _path_glsl_effect + "ats_Atmoshpere.vert", ats_atmposhere_vert_helpers),
                new ShaderFile(ShaderType.FragmentShader, _path_glsl_effect + "ats_Atmoshpere.frag", ats_atmposhere_frag_helpers)
            });
            _pAtmoshpere.addUniform("transmittanceSampler");
            _pAtmoshpere.addUniform("irradianceSampler");
            _pAtmoshpere.addUniform("inscatterSampler");
            _pAtmoshpere.enable_Samplers(4);
            _pAtmoshpere.addUniform("sun_position");

        }

        protected override void load_Buffers()
        {

            //------------------------------------------------------
            // Precomputation Textures
            //------------------------------------------------------

            _tTransmittance = new Texture(TextureTarget.Texture2D,
                TRANSMITTANCE_W, TRANSMITTANCE_H, 0, 
                false, false);
            _tTransmittance.load();

            _tIrradiance = new Texture(TextureTarget.Texture2D,
                SKY_W, SKY_H, 0, 
                false, false);
            _tIrradiance.load();

            _tInscatter = new Texture(TextureTarget.Texture3D,
                RES_MU_S * RES_NU, RES_MU, RES_R, 
                false, false);
            _tInscatter.load();

            _tDeltaE = new Texture(TextureTarget.Texture2D,
                SKY_W, SKY_H, 0,
                false, false);
            _tDeltaE.load();

            _tDeltaSR = new Texture(TextureTarget.Texture3D,
                RES_MU_S * RES_NU, RES_MU, RES_R,
                false, false);
            _tDeltaSR.load();

            _tDeltaSM = new Texture(TextureTarget.Texture3D,
                RES_MU_S * RES_NU, RES_MU, RES_R,
                false, false);
            _tDeltaSM.load();

            _tDeltaJ = new Texture(TextureTarget.Texture3D,
                RES_MU_S * RES_NU, RES_MU, RES_R,
                false, false);
            _tDeltaJ.load();


            _fPrecompute = new FrameBuffer("ATS Precomputation");

            //------------------------------------------------------
            // Atmoshperic Scattering Textures
            //------------------------------------------------------
            _tAtmoshpere = new Texture(TextureTarget.Texture2D,
                _resolution.W, _resolution.H, 0,
                false, false);
            _tAtmoshpere.load();

            _fAtmosphere = new FrameBuffer("ATS Atmoshpere");
            _fAtmosphere.load(new Dictionary<FramebufferAttachment, Texture>()
            {
                { FramebufferAttachment.ColorAttachment0, _tAtmoshpere },
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

        //------------------------------------------------------
        // Precompute
        //------------------------------------------------------
        void setLayer(Program program, int layer)
        {
            double r = layer / (RES_R - 1.0f);
            r = r * r;
            r = Math.Sqrt(Rg * Rg + r * (Rt * Rt - Rg * Rg)) + (layer == 0 ? 0.01f : (layer == RES_R - 1 ? -0.001f : 0.0f));
            double dmin = Rt - r;
            double dmax = Math.Sqrt(r * r - Rg * Rg) + Math.Sqrt(Rt * Rt - Rg * Rg);
            double dminp = r - Rg;
            double dmaxp = Math.Sqrt(r * r - Rg * Rg);
            OGL.Uniform(program.getUniform("r"), (float)r);
            OGL.Uniform(program.getUniform("dhdH"), (float)dmin, (float)dmax, (float)dminp, (float)dmaxp);
            OGL.Uniform(program.getUniform("layer"), layer);
        }

        public void precompute(fx_Quad quad)
        {
            if (_precomputed) return;

            _fPrecompute.bind(DrawBuffersEnum.ColorAttachment0);


            //------------------------------------------------------

            _fPrecompute.bindTexture(FramebufferAttachment.ColorAttachment0, _tTransmittance.id);
            OGL.Viewport(0, 0, TRANSMITTANCE_W, TRANSMITTANCE_H);
            _pTransmittance.bind();
            quad.render();

            //------------------------------------------------------

            _fPrecompute.bindTexture(FramebufferAttachment.ColorAttachment0, _tDeltaE.id);
            OGL.Viewport(0, 0, SKY_W, SKY_H);
            _pIrradiance1.bind();
            _tTransmittance.bind(_pIrradiance1.getUniform("transmittanceSampler"), 0);
            quad.render();

            //------------------------------------------------------

            _fPrecompute.bindTexture(FramebufferAttachment.ColorAttachment0, _tDeltaSR.id);
            _fPrecompute.bindTexture(FramebufferAttachment.ColorAttachment1, _tDeltaSM.id);
            _fPrecompute.bindAttachements(new DrawBuffersEnum[]{
                DrawBuffersEnum.ColorAttachment0,
                DrawBuffersEnum.ColorAttachment1
            });
            OGL.Viewport(0, 0, RES_MU_S * RES_NU, RES_MU);
            _pInscatter1.bind();
            _tTransmittance.bind(_pInscatter1.getUniform("transmittanceSampler"), 0);
            for (int layer = 0; layer < RES_R; layer++)
            {
                setLayer(_pInscatter1, layer);
                quad.render();
            }
            _fPrecompute.bindTexture(FramebufferAttachment.ColorAttachment1, 0);
            _fPrecompute.bindAttachements(DrawBuffersEnum.ColorAttachment0);

            //------------------------------------------------------

            _fPrecompute.bindTexture(FramebufferAttachment.ColorAttachment0, _tIrradiance.id);
            OGL.Viewport(0, 0, SKY_W, SKY_H);
            _pCopyIrradiance.bind();
            OGL.Uniform(_pCopyIrradiance.getUniform("k"), 0f);
            _tDeltaE.bind(_pCopyIrradiance.getUniform("deltaESampler"), 0);
            quad.render();

            //------------------------------------------------------

            _fPrecompute.bindTexture(FramebufferAttachment.ColorAttachment0, _tInscatter.id);
            OGL.Viewport(0, 0, RES_MU_S * RES_NU, RES_MU);
            _pCopyInscatter1.bind();
            _tDeltaSR.bind(_pCopyInscatter1.getUniform("deltaSRSampler"), 0);
            _tDeltaSM.bind(_pCopyInscatter1.getUniform("deltaSMSampler"), 1);
            for (int layer = 0; layer < RES_R; layer++)
            {
                setLayer(_pCopyInscatter1, layer);
                quad.render();
            }

            //------------------------------------------------------

            for (int order = 2; order <= 4; order++)
            {

                //------------------------------------------------------

                _fPrecompute.bindTexture(FramebufferAttachment.ColorAttachment0, _tDeltaJ.id);
                OGL.Viewport(0, 0, RES_MU_S * RES_NU, RES_MU);
                _pPJ.bind();
                OGL.Uniform(_pPJ.getUniform("first"), order == 2 ? 1f : 0f);
                _tTransmittance.bind(_pPJ.getUniform("transmittanceSampler"), 0);
                _tDeltaE.bind(_pPJ.getUniform("deltaESampler"), 1);
                _tDeltaSR.bind(_pPJ.getUniform("deltaSRSampler"), 2);
                _tDeltaSM.bind(_pPJ.getUniform("deltaSMSampler"), 3);
                for (int layer = 0; layer < RES_R; layer++)
                {
                    setLayer(_pPJ, layer);
                    quad.render();
                }

                //------------------------------------------------------

                _fPrecompute.bindTexture(FramebufferAttachment.ColorAttachment0, _tDeltaE.id);
                OGL.Viewport(0, 0, SKY_W, SKY_H);
                _pIrradianceN.bind();
                OGL.Uniform(_pIrradianceN.getUniform("first"), order == 2 ? 1f : 0f);
                _tTransmittance.bind(_pIrradianceN.getUniform("transmittanceSampler"), 0);
                _tDeltaE.bind(_pIrradianceN.getUniform("deltaESampler"), 1);
                _tDeltaSR.bind(_pIrradianceN.getUniform("deltaSRSampler"), 2);
                _tDeltaSM.bind(_pIrradianceN.getUniform("deltaSMSampler"), 3);
                quad.render();

                //------------------------------------------------------

                _fPrecompute.bindTexture(FramebufferAttachment.ColorAttachment0, _tDeltaSR.id);
                OGL.Viewport(0, 0, RES_MU_S * RES_NU, RES_MU);
                _pInscatterN.bind();
                OGL.Uniform(_pInscatterN.getUniform("first"), order == 2 ? 1f : 0f);
                _tTransmittance.bind(_pInscatterN.getUniform("transmittanceSampler"), 0);
                _tDeltaJ.bind(_pInscatterN.getUniform("deltaJSampler"), 1);
                for (int layer = 0; layer < RES_R; layer++)
                {
                    setLayer(_pInscatterN, layer);
                    quad.render();
                }

                //------------------------------------------------------
                //------------------------------------------------------

                OGL.Enable(EnableCap.Blend);
                OGL.BlendEquationSeparate(BlendEquationMode.FuncAdd, BlendEquationMode.FuncAdd);
                OGL.BlendFuncSeparate(BlendingFactorSrc.One, BlendingFactorDest.One, BlendingFactorSrc.One, BlendingFactorDest.One);

                //------------------------------------------------------

                _fPrecompute.bindTexture(FramebufferAttachment.ColorAttachment0, _tIrradiance.id);
                OGL.Viewport(0, 0, SKY_W, SKY_H);
                _pCopyIrradiance.bind();
                OGL.Uniform(_pCopyIrradiance.getUniform("k"), 1f);
                _tDeltaE.bind(_pCopyIrradiance.getUniform("deltaESampler"), 0);
                quad.render();

                //------------------------------------------------------

                _fPrecompute.bindTexture(FramebufferAttachment.ColorAttachment0, _tInscatter.id);
                OGL.Viewport(0, 0, RES_MU_S * RES_NU, RES_MU);
                _pCopyInscatterN.bind();
                _tDeltaSR.bind(_pCopyInscatterN.getUniform("deltaSRSampler"), 0);
                for (int layer = 0; layer < RES_R; layer++)
                {
                    setLayer(_pCopyInscatterN, layer);
                    quad.render();
                }

                //------------------------------------------------------

                OGL.Disable(EnableCap.Blend);

                //------------------------------------------------------
                //------------------------------------------------------
            }


            _precomputed = true;
        }


        //------------------------------------------------------
        // Render
        //------------------------------------------------------

        public void render(
            fx_Quad quad,
            Texture normal_texture,
            Texture diffuse_texture,
            Texture specular_texture,
            Vector3 sun_position,
            Texture shadowDepthTexture)
        {
            //------------------------------------------------------
            // Render Atmoshpere
            //------------------------------------------------------

            _fAtmosphere.bind(DrawBuffersEnum.ColorAttachment0);

            OGL.Viewport(0, 0, _resolution.W, _resolution.H);
            _pAtmoshpere.bind();

            _tTransmittance.bind(_pAtmoshpere.getUniform("transmittanceSampler"), 0);
            _tIrradiance.bind(_pAtmoshpere.getUniform("irradianceSampler"), 1);
            _tInscatter.bind(_pAtmoshpere.getUniform("inscatterSampler"), 2);

            normal_texture.bind(_pAtmoshpere.getSamplerUniform(0), 3);
            diffuse_texture.bind(_pAtmoshpere.getSamplerUniform(1), 4);
            specular_texture.bind(_pAtmoshpere.getSamplerUniform(2), 5);
            shadowDepthTexture.bind(_pAtmoshpere.getSamplerUniform(3), 6);

            sun_position = Vector3.Normalize(sun_position);
            OGL.Uniform(_pAtmoshpere.getUniform("sun_position"), sun_position);

            quad.renderFullQuad();
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NeiraEngine;
using NeiraEngine.Components;
using NeiraEngine.Render;
using NeiraEngine.Render.OpenGL;
using NeiraEngine.World;

namespace NativeOpenGL
{
    public class Main : RenderModuleGL
    {
        private bool _enable_debug_views;
        private bool _take_screenshot;

        // Render FXs
        private List<RenderEffect> _effects;

        private fx_Quad _fxQuad;
        private fx_Test _fxTest;
        private fx_CrossHair _fxCrosshair;
        private fx_Sprite _fxSprite;
        private fx_Special _fxSpecial;
        private fx_Final _fxFinal;
        private fx_gBuffer _fxGBuffer;
        private fx_VXGI _fxVXGI;
        private fx_Shadow _fxShadow;
        private fx_SkyBox _fxSkyBox;
        private fx_HDR _fxHDR;
        private fx_Lens _fxLens;
        private fx_DepthOfField _fxDepthOfField;
        private fx_MotionBlur _fxMotionBlur;
        private fx_AtmosphericScattering _fxAtmosphericScattering;


        public Main(Resolution res) : base(res)
        {
            Debug.logInfo(0, "Hello from the NativeOpenGL");
            _enable_debug_views = false;

            // Render UBOs
            ubo_game_config = new UniformBuffer(BufferStorageFlags.DynamicStorageBit, 0, new EngineHelper.size[]
            {
                EngineHelper.size.vec4,
                EngineHelper.size.f
            });

            ubo_camera = new UniformBuffer(BufferStorageFlags.DynamicStorageBit, 1, new EngineHelper.size[] {
                EngineHelper.size.mat4,
                EngineHelper.size.mat4,
                EngineHelper.size.mat4,
                EngineHelper.size.mat4,
                EngineHelper.size.mat4,
                EngineHelper.size.vec3,
                EngineHelper.size.vec3
            });

            // Render FX List
            _effects = new List<RenderEffect>();

            // Render FXs
            _fxQuad = createEffect<fx_Quad>("common/");
            _fxTest = createEffect<fx_Test>("test/");
            _fxCrosshair = createEffect<fx_CrossHair>("crosshair/");
            _fxSprite = createEffect<fx_Sprite>("sprite/");
            _fxSpecial = createEffect<fx_Special>("special/");
            _fxFinal = createEffect<fx_Final>("final/");
            _fxGBuffer = createEffect<fx_gBuffer>("gBuffer/");
            _fxVXGI = createEffect<fx_VXGI>("vxgi/");
            _fxShadow = createEffect<fx_Shadow>("shadow/");
            _fxSkyBox = createEffect<fx_SkyBox>("skybox/");
            _fxHDR = createEffect<fx_HDR>("hdr/");
            _fxLens = createEffect<fx_Lens>("lens/");
            _fxDepthOfField = createEffect<fx_DepthOfField>("dof/");
            _fxMotionBlur = createEffect<fx_MotionBlur>("motion_blur/");
            _fxAtmosphericScattering = createEffect<fx_AtmosphericScattering>("ats/");
            Load_FX();
        }

        //------------------------------------------------------
        // Loading
        //------------------------------------------------------

        // Factory workers to create effects
        private T createEffect<T>(string resource_folder_name) where T : RenderEffect
        {
            T temp_effect = (T)Activator.CreateInstance(typeof(T), resource_folder_name, resolution);
            _effects.Add(temp_effect);
            return temp_effect;
        }

        private void Load_FX()
        {
            foreach (RenderEffect effect in _effects)
            {
                effect.load();
            }
        }

        //------------------------------------------------------
        // Helpers
        //------------------------------------------------------
        public void handle_MouseState(bool locked)
        {
            _fxCrosshair.enabled = locked;
        }


        public void toggleDebugViews()
        {
            _enable_debug_views = !_enable_debug_views;
        }


        public void toggleWireframe()
        {
            _fxGBuffer.toggleWireframe();
        }


        public void toggleEffect(Type effect_type)
        {
            foreach (RenderEffect effect in _effects)
            {
                if (effect.GetType() == effect_type)
                    effect.toggle();
            }
        }

        public RenderEffect getEffect(Type effect_type)
        {
            foreach (RenderEffect effect in _effects)
            {
                if (effect.GetType() == effect_type)
                    return effect;
            }
            return null;
        }

        //------------------------------------------------------
        // Rendering
        //------------------------------------------------------
        public void Render(Scene scene, SpatialData camera_spatial_data, float current_fps)
        {

            //------------------------------------------------------
            // Pre-Processing
            //------------------------------------------------------
            OGL.Disable(EnableCap.DepthTest);

            _fxAtmosphericScattering.precompute(_fxQuad);

            _fxHDR.calcExposure(_fxFinal.tFinalScene);


            //------------------------------------------------------
            // Scene Processing
            //------------------------------------------------------
            OGL.DepthMask(true);
            OGL.Enable(EnableCap.DepthTest);
            OGL.Enable(EnableCap.CullFace);
            OGL.CullFace(CullFaceMode.Back);


            _fxVXGI.voxelizeScene(scene, camera_spatial_data.position);


            _fxShadow.render(scene, camera_spatial_data);


            _fxVXGI.lightInjection(scene, _fxShadow, camera_spatial_data);


            _fxGBuffer.pass_DeferredShading(scene, _fxShadow);


            _fxSkyBox.render(_fxQuad, _fxGBuffer.fGBuffer, scene.circadian_timer.position);


            //------------------------------------------------------
            // Post-processing
            //------------------------------------------------------
            OGL.Disable(EnableCap.DepthTest);

            _fxVXGI.coneTracing(_fxQuad, _fxGBuffer.tDiffuse_ID, _fxGBuffer.tNormal_Depth, _fxGBuffer.tSpecular, camera_spatial_data);

            _fxAtmosphericScattering.render(_fxQuad, _fxGBuffer.tNormal_Depth, _fxGBuffer.tDiffuse_ID, _fxGBuffer.tSpecular, scene.circadian_timer.position, _fxShadow.tDirectional);

            _fxGBuffer.pass_LightAccumulation(_fxQuad, _fxAtmosphericScattering.tAtmosphere, _fxVXGI.tConeTrace_Diffuse, _fxFinal.fFinalScene);

            _fxDepthOfField.render(_fxQuad, _fxSpecial, _fxGBuffer.tNormal_Depth, _fxFinal.fFinalScene, _fxFinal.tFinalScene);

            _fxHDR.scaleScene(_fxQuad, _fxFinal.fFinalScene, _fxFinal.tFinalScene);

            _fxLens.render(_fxQuad, _fxSpecial, _fxFinal.tFinalScene, _fxFinal.fFinalScene, camera_spatial_data.rotation_matrix);

            _fxMotionBlur.render(_fxQuad, _fxSpecial, _fxFinal.fFinalScene, _fxFinal.tFinalScene, _fxGBuffer.tNormal_Depth, _fxGBuffer.tVelocity, current_fps);


            //------------------------------------------------------
            // Render to Screen
            //------------------------------------------------------
            _fxFinal.render(_fxQuad);


            //------------------------------------------------------
            // Debug Views
            //------------------------------------------------------

            _fxVXGI.rayTracing(_fxQuad, camera_spatial_data);

            //_fxSprite.updateProjection()

            foreach (SpriteComponent sprite in scene.sprites)
            {
                Image image = sprite.GetCurrentFrame();
                _fxSprite.render_Texture(sprite.GetCurrentFrame().texture, sprite.color, sprite.worldObject.spatial.scale.Xy, sprite.worldObject.spatial.position.Xy, sprite.worldObject.spatial.rotation_angles.X, (int)sprite.worldObject.spatial.position.Z);
            }

            if (_enable_debug_views)
            {
                //_fxQuad.render_Texture(_fxDepthOfField.tDOF_Scene, 1f, 0);
                //_fxQuad.render_Texture(_fxMotionBlur.tFinal, 1f, 0);


                //_fxQuad.render_Texture(_fxVXGI.tConeTrace_Diffuse, 0.5f, 1);
                //_fxQuad.render_Texture(_fxVXGI._tVoxelVolume, 0.33f, 1, 150);
                //_fxQuad.render_Texture(_fxAtmosphericScattering.tAtmosphere, 0.25f, 2);
                //_fxQuad.render_Texture(_fxMotionBlur.tVelocity_2, 0.25f, 3);
                //_fxQuad.render_Texture(_fxMotionBlur.tVelocity_1, 0.25f, 2);
                //_fxQuad.render_Texture(_fxShadow.tSpot, 0.25f, 2);
                //_fxQuad.render_Texture(_fxVXGI._tTemp, 0.25f, 1);
                _fxQuad.render_Texture(_fxGBuffer.tDiffuse_ID, 0.25f, 0);


                // CSM Cascades
                //_fxQuad.render_Texture(_fxShadow.tDirectional, 0.25f, 3, 3);
                //_fxQuad.render_Texture(_fxShadow.tDirectional, 0.25f, 2, 2);
                //_fxQuad.render_Texture(_fxShadow.tDirectional, 0.25f, 1, 1);
                //_fxQuad.render_Texture(_fxShadow.tDirectional, 0.25f, 0, 0);
            }

            //------------------------------------------------------
            // Overlays
            //------------------------------------------------------
            _fxCrosshair.render(scene.current_animation_time);

        }
    }
}
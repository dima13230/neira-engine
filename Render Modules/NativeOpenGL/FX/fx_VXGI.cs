﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NeiraEngine;
using NeiraEngine.Render;
using NeiraEngine.Render.OpenGL;
using NeiraEngine.Output;
using NeiraEngine.World;
using NeiraEngine.World.Lights;

namespace NativeOpenGL
{
    public class fx_VXGI : RenderEffect
    {
        // Properties
        private bool _debug_display_voxels = false;
        private int _debug_display_voxels_mip_level = 0;
        private float _vx_volume_dimensions = 256.0f;
        private float _vx_volume_scale = 30.0f;
        private Matrix4 _vx_shift_matrix;

        // Programs
        private Program _pVoxelize;
        private Program _pRayTrace;
        private Program _pConeTrace;
        private Program _pInjection;
        private Program _pMipMap;

        // Frame Buffers
        private FrameBuffer _fConeTrace;

        // Textures
        public Texture _tVoxelVolume { get; private set; }
        private Texture _tVoxelVolume_Diffuse;

        private Texture _tConeTrace_Diffuse;
        public Texture tConeTrace_Diffuse
        {
            get { return _tConeTrace_Diffuse; }
        }


        public Texture _tTemp;


        public fx_VXGI(string glsl_effect_path, Resolution full_resolution)
            : base(glsl_effect_path, full_resolution)
        {
            // Shifts the voxel volume for cone tracing
            float vx_shift_scaler = 2.0f;
            float vx_shift_translation = -_vx_volume_dimensions / vx_shift_scaler;
            _vx_shift_matrix = Matrix4.CreateTranslation(new Vector3(vx_shift_translation)) *
                Matrix4.CreateScale(_vx_volume_scale / _vx_volume_dimensions * vx_shift_scaler);
        }

        protected override void load_Programs()
        {
            string[] geometry_extensions = new string[]
            {
                EngineHelper.path_glsl_common_ext_bindlessTextures
            };
            string[] trace_vert_helpers = new string[]
            {
                EngineHelper.path_glsl_common_ubo_cameraSpatials
            };
            string[] trace_frag_helpers = new string[]
            {
                EngineHelper.path_glsl_common_ubo_cameraSpatials,
                EngineHelper.path_glsl_common_helper_positionFromDepth,
                EngineHelper.path_glsl_common_helper_voxelFunctions
            };
            string[] injection_helpers = new string[]
            {
                EngineHelper.path_glsl_common_helper_positionFromDepth,
                EngineHelper.path_glsl_common_helper_lightingFunctions,
                EngineHelper.path_glsl_common_helper_shadowEvaluation,
                EngineHelper.path_glsl_common_helper_voxelFunctions
            };
            string[] voxelize_helpers = new string[]
            {
                EngineHelper.path_glsl_common_ubo_bindlessTextures_Materials
            };
            voxelize_helpers = voxelize_helpers.Concat(injection_helpers).ToArray();

            string[] injection_ubos = new string[]
            {
                EngineHelper.path_glsl_common_ubo_shadowManifest,
                EngineHelper.path_glsl_common_ubo_shadowMatrices_Spot,
                EngineHelper.path_glsl_common_ubo_shadowMatrices_Point,
                EngineHelper.path_glsl_common_ubo_shadowMatrices_Directional,
            };
            injection_helpers = injection_helpers.Concat(injection_ubos).ToArray();


            // Rendering Geometry into voxel volume
            _pVoxelize = ProgramLoader.createProgram(new ShaderFile[]
            {
                new ShaderFile(ShaderType.VertexShader, _path_glsl_effect + "vxgi_Voxelize.vert", null),
                new ShaderFile(ShaderType.GeometryShader, _path_glsl_effect + "vxgi_Voxelize.geom", null),
                new ShaderFile(ShaderType.FragmentShader, _path_glsl_effect + "vxgi_Voxelize.frag", voxelize_helpers, geometry_extensions)
            });
            _pVoxelize.enable_MeshLoading();
            _pVoxelize.enable_Samplers(2);
            _pVoxelize.addUniform("vx_volume_dimensions");
            _pVoxelize.addUniform("vx_volume_scale");
            _pVoxelize.addUniform("vx_volume_position");
            _pVoxelize.addUniform("vx_projection");


            // Cone Trace through voxel volume
            _pConeTrace = ProgramLoader.createProgram(new ShaderFile[]
            {
                new ShaderFile(ShaderType.VertexShader, _path_glsl_effect + "vxgi_Trace.vert", trace_vert_helpers),
                new ShaderFile(ShaderType.FragmentShader, _path_glsl_effect + "vxgi_ConeTrace.frag", trace_frag_helpers)
            });
            _pConeTrace.enable_Samplers(4);
            _pConeTrace.addUniform("vx_volume_dimensions");
            _pConeTrace.addUniform("vx_volume_scale");
            _pConeTrace.addUniform("vx_volume_position");
            _pConeTrace.addUniform("maxMipLevels");

            // Cone Trace through voxel volume
            _pRayTrace = ProgramLoader.createProgram(new ShaderFile[]
            {
                new ShaderFile(ShaderType.VertexShader, _path_glsl_effect + "vxgi_Trace.vert", trace_vert_helpers),
                new ShaderFile(ShaderType.FragmentShader, _path_glsl_effect + "vxgi_RayTrace.frag", trace_frag_helpers)
            });
            _pRayTrace.enable_Samplers(1);
            _pRayTrace.addUniform("vx_volume_dimensions");
            _pRayTrace.addUniform("vx_inv_view_perspective");
            _pRayTrace.addUniform("displayMipLevel");

            // Light Injection
            _pInjection = ProgramLoader.createProgram(new ShaderFile[]
            {
                new ShaderFile(ShaderType.ComputeShader, _path_glsl_effect + "vxgi_Injection.comp", injection_helpers)
            });
            _pInjection.enable_Samplers(6);
            _pInjection.addUniform("texture_size");
            _pInjection.addUniform("vx_volume_dimensions");
            _pInjection.addUniform("vx_volume_scale");
            _pInjection.addUniform("vx_volume_position");


            // MipMap
            _pMipMap = ProgramLoader.createProgram(new ShaderFile[]
            {
                new ShaderFile(ShaderType.ComputeShader, _path_glsl_effect + "vxgi_MipMap.comp", null)
            });
            _pMipMap.enable_Samplers(2);
            _pMipMap.addUniform("source_mip_level");

        }

        protected override void load_Buffers()
        {
            //------------------------------------------------------
            // Voxel Volumes
            //------------------------------------------------------
            _tVoxelVolume = new Texture(TextureTarget.Texture3D,
                (int)_vx_volume_dimensions, (int)_vx_volume_dimensions, (int)_vx_volume_dimensions,
                true, true,
                PixelInternalFormat.Rgba16f, PixelFormat.Rgba, PixelType.Float,
                TextureMinFilter.LinearMipmapLinear, TextureMagFilter.Linear, TextureWrapMode.Clamp);
            _tVoxelVolume.load();

            _tVoxelVolume_Diffuse = new Texture(TextureTarget.Texture3D,
                (int)_vx_volume_dimensions, (int)_vx_volume_dimensions, (int)_vx_volume_dimensions,
                false, false,
                PixelInternalFormat.Rgba16f, PixelFormat.Rgba, PixelType.Float,
                TextureMinFilter.Linear, TextureMagFilter.Linear, TextureWrapMode.Clamp);
            _tVoxelVolume_Diffuse.load();

            //------------------------------------------------------
            // Cone Traced Lighting
            //------------------------------------------------------
            _tConeTrace_Diffuse = new Texture(TextureTarget.Texture2D,
                _resolution.W, _resolution.H, 0,
                false, false,
                PixelInternalFormat.Rgba16f, PixelFormat.Rgba, PixelType.Float,
                TextureMinFilter.Linear, TextureMagFilter.Linear, TextureWrapMode.Clamp);
            _tConeTrace_Diffuse.load();


            _fConeTrace = new FrameBuffer("VXGI - Cone Trace");
            _fConeTrace.load(new Dictionary<FramebufferAttachment, Texture>()
            {
                { FramebufferAttachment.ColorAttachment0, _tConeTrace_Diffuse }
            });

            _tTemp = new Texture(TextureTarget.Texture2D,
                (int)_vx_volume_dimensions, (int)_vx_volume_dimensions, 0,
                false, false,
                PixelInternalFormat.Rgba16f, PixelFormat.Rgba, PixelType.Float,
                TextureMinFilter.Linear, TextureMagFilter.Linear, TextureWrapMode.Clamp);
            _tTemp.load();
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
        // Helpers
        //------------------------------------------------------

        private Vector3 voxelSnap(Vector3 vector)
        {
            Vector3 temp_vector = vector;
            float scaler = (_vx_volume_dimensions) / (_vx_volume_scale * (float)Math.Pow(2.0f, _tVoxelVolume.getMaxMipMap()/1.0f));
            temp_vector *= scaler;
            temp_vector.X = (float)Math.Floor(temp_vector.X);
            temp_vector.Y = (float)Math.Floor(temp_vector.Y);
            temp_vector.Z = (float)Math.Floor(temp_vector.Z);
            temp_vector /= scaler;

            return temp_vector;
            //return Vector3.Zero;
        }


        private void clearVoxelVolumes()
        {
            _tVoxelVolume.clear();
            _tVoxelVolume_Diffuse.clear();
        }


        private void mipMap()
        {
            //_tVoxelVolume.generateMipMap();

            _pMipMap.bind();

            for (int mip_level = 1; mip_level < _tVoxelVolume.getMaxMipMap(); mip_level++)
            {
                OGL.Uniform(_pMipMap.getUniform("source_mip_level"), mip_level - 1);

                _tVoxelVolume.bind(_pMipMap.getSamplerUniform(0), 0);
                _tVoxelVolume.bindImageUnit(_pMipMap.getSamplerUniform(1), 1, TextureAccess.WriteOnly, mip_level);

                OGL.DispatchCompute(
                    ((_tVoxelVolume.width >> mip_level) + _pMipMap.compute_workgroup_size[0] - 1) / _pMipMap.compute_workgroup_size[0],
                    ((_tVoxelVolume.width >> mip_level) + _pMipMap.compute_workgroup_size[1] - 1) / _pMipMap.compute_workgroup_size[1],
                    ((_tVoxelVolume.width >> mip_level) + _pMipMap.compute_workgroup_size[2] - 1) / _pMipMap.compute_workgroup_size[2]);

                OGL.MemoryBarrier(MemoryBarrierFlags.TextureFetchBarrierBit | MemoryBarrierFlags.ShaderImageAccessBarrierBit);
            }
        }


        //------------------------------------------------------
        // Main Functions
        //------------------------------------------------------


        public void voxelizeScene(Scene scene, Vector3 camera_position)
        {
            if (!_enabled) return;

            clearVoxelVolumes();

            OGL.ColorMask(false, false, false, false);
            OGL.Disable(EnableCap.DepthTest);
            OGL.Disable(EnableCap.CullFace);
            OGL.Disable(EnableCap.DepthClamp);

            OGL.Viewport(0, 0, _tVoxelVolume.width, _tVoxelVolume.height);


            _pVoxelize.bind();



            float radius = _vx_volume_dimensions;
            Matrix4 voxel_projection = Matrix4.CreateOrthographicOffCenter(-radius, radius, -radius, radius, -radius, radius);

            OGL.Uniform(_pVoxelize.getUniform("vx_projection"), false, voxel_projection);


            OGL.Uniform(_pVoxelize.getUniform("vx_volume_dimensions"), _vx_volume_dimensions);
            OGL.Uniform(_pVoxelize.getUniform("vx_volume_scale"), _vx_volume_scale);


            Matrix4 voxel_volume_position = Matrix4.CreateTranslation(voxelSnap(camera_position));
            OGL.Uniform(_pVoxelize.getUniform("vx_volume_position"), false, voxel_volume_position);


            _tVoxelVolume.bindImageUnit(_pVoxelize.getSamplerUniform(0), 0, TextureAccess.WriteOnly);
            _tVoxelVolume_Diffuse.bindImageUnit(_pVoxelize.getSamplerUniform(1), 1, TextureAccess.WriteOnly);


            scene.renderMeshesGL_WithMaterials(BeginMode.Triangles, _pVoxelize);
            //scene.renderLightObjects(BeginMode.Triangles, _pVoxelize);


            OGL.DepthMask(true);
            OGL.ColorMask(true, true, true, true);
            OGL.Enable(EnableCap.DepthTest);
            OGL.Enable(EnableCap.CullFace);
            OGL.Enable(EnableCap.DepthClamp);


            OGL.MemoryBarrier(MemoryBarrierFlags.TextureFetchBarrierBit | MemoryBarrierFlags.ShaderImageAccessBarrierBit);


        }


        public void lightInjection(Scene scene, fx_Shadow shadow, SpatialData camera_spatial)
        {
            if (!_enabled) return;


            int workgroup_size = 4;
            int texture_size = (int)_vx_volume_dimensions * 8;

            _tTemp.clear();

            _pInjection.bind();


            OGL.Uniform(_pInjection.getUniform("texture_size"), shadow.tSpot.dimensions.Xy);

            OGL.Uniform(_pInjection.getUniform("vx_volume_dimensions"), _vx_volume_dimensions);
            OGL.Uniform(_pInjection.getUniform("vx_volume_scale"), _vx_volume_scale);
            OGL.Uniform(_pInjection.getUniform("vx_volume_position"), -voxelSnap(camera_spatial.position));

            _tVoxelVolume.bindImageUnit(_pInjection.getSamplerUniform(0), 0, TextureAccess.ReadWrite);
            _tVoxelVolume_Diffuse.bind(_pInjection.getSamplerUniform(1), 1);

            shadow.tSpot.bind(_pInjection.getSamplerUniform(2), 2);
            shadow.tPoint.bind(_pInjection.getSamplerUniform(3), 3);
            shadow.tDirectional.bind(_pInjection.getSamplerUniform(4), 4);

            _tTemp.bindImageUnit(_pInjection.getSamplerUniform(5), 5, TextureAccess.WriteOnly);


            OGL.DispatchCompute(((int)shadow.tSpot.dimensions.X / workgroup_size), ((int)shadow.tSpot.dimensions.Y / workgroup_size), 1);

            OGL.MemoryBarrier(MemoryBarrierFlags.TextureFetchBarrierBit | MemoryBarrierFlags.ShaderImageAccessBarrierBit);


        }


        public void coneTracing(fx_Quad quad, Texture diffuse_texture, Texture normal_texture, Texture specular_texture, SpatialData camera_spatial)
        {
            if (!_enabled)
            {
                _tConeTrace_Diffuse.clear();
                return;
            }

            mipMap();


            _fConeTrace.bind(DrawBuffersEnum.ColorAttachment0);

            OGL.Viewport(0, 0, _tConeTrace_Diffuse.width, _tConeTrace_Diffuse.height);

            _pConeTrace.bind();

            OGL.Uniform(_pConeTrace.getUniform("vx_volume_dimensions"), _vx_volume_dimensions);
            OGL.Uniform(_pConeTrace.getUniform("vx_volume_scale"), _vx_volume_scale);

            Vector3 vx_position_snapped = -voxelSnap(camera_spatial.position);
            Matrix4 voxel_volume_position = Matrix4.CreateTranslation(vx_position_snapped);
            OGL.Uniform(_pConeTrace.getUniform("vx_volume_position"), vx_position_snapped);

            OGL.Uniform(_pConeTrace.getUniform("maxMipLevels"), _tVoxelVolume.getMaxMipMap());

            normal_texture.bind(_pConeTrace.getSamplerUniform(0), 0);
            specular_texture.bind(_pConeTrace.getSamplerUniform(1), 1);
            diffuse_texture.bind(_pConeTrace.getSamplerUniform(2), 2);

            _tVoxelVolume.bind(_pConeTrace.getSamplerUniform(3), 3);


            quad.renderFullQuad();
        }


        public void rayTracing(fx_Quad quad, SpatialData camera_spatial)
        {
            if (!(_debug_display_voxels && _enabled)) return;

            mipMap();

            OGL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, 0);
            OGL.Clear(ClearBufferMask.ColorBufferBit);

            OGL.Viewport(0, 0, _resolution.W, _resolution.H);


            _pRayTrace.bind();

            OGL.Uniform(_pRayTrace.getUniform("vx_volume_dimensions"), _vx_volume_dimensions);

            Vector3 vx_position_snapped = -voxelSnap(camera_spatial.position);
            Matrix4 voxel_volume_position = Matrix4.CreateTranslation(vx_position_snapped);

            Matrix4 invMVP = Matrix4.Invert(_vx_shift_matrix * voxel_volume_position * camera_spatial.model_view * camera_spatial.perspective);
            OGL.Uniform(_pRayTrace.getUniform("vx_inv_view_perspective"), false, invMVP);

            OGL.Uniform(_pRayTrace.getUniform("displayMipLevel"), _debug_display_voxels_mip_level);


            _tVoxelVolume.bind(_pRayTrace.getSamplerUniform(0), 0);


            quad.renderFullQuad();

        }

    }
}

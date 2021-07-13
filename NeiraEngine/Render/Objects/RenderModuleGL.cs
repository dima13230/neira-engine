using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NeiraEngine.Render;
using ngl = NeiraEngine.Render.OpenGL;
using NeiraEngine.World;
using NeiraEngine.Output;

namespace NeiraEngine.Render
{
    public class RenderModuleGL : RenderModule
    {
        public ngl.UniformBuffer ubo_game_config;

        public ngl.UniformBuffer ubo_camera;

        public Dictionary<string, ngl.UniformBuffer> ModuleUniformBuffers = new Dictionary<string, ngl.UniformBuffer>();

        public RenderModuleGL(Resolution res)
            : base(res)
        {
            ubo_game_config = new ngl.UniformBuffer(ngl.BufferStorageFlags.DynamicStorageBit, 0, new EngineHelper.size[]
            {
                EngineHelper.size.vec4,
                EngineHelper.size.f
            });
            ubo_camera = new ngl.UniformBuffer(ngl.BufferStorageFlags.DynamicStorageBit, 1, new EngineHelper.size[] {
                EngineHelper.size.mat4,
                EngineHelper.size.mat4,
                EngineHelper.size.mat4,
                EngineHelper.size.mat4,
                EngineHelper.size.mat4,
                EngineHelper.size.vec3,
                EngineHelper.size.vec3 });
        }

        //------------------------------------------------------
        // Updating
        //------------------------------------------------------
        public void updateUBO_GameConfig(Vector4 near_far, float target_fps)
        {
            ubo_game_config.update(0, near_far);
            ubo_game_config.update(1, target_fps);
        }

        public void updateUBO_Camera(
            Matrix4 view, Matrix4 perspective, Matrix4 inv_view_perspective,
            Matrix4 previous_view_perspective, Matrix4 inv_previous_view_perspective,
            Vector3 position, Vector3 look)
        {
            ubo_camera.update(0, view);
            ubo_camera.update(1, perspective);
            ubo_camera.update(2, inv_view_perspective);
            ubo_camera.update(3, previous_view_perspective);
            ubo_camera.update(4, inv_previous_view_perspective);
            ubo_camera.update(5, position);
            ubo_camera.update(6, look);
        }
    }
}
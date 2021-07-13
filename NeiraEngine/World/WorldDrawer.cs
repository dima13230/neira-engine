using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NeiraEngine.Render;
using NeiraEngine.World.Model;
using NeiraEngine.World.Lights;

namespace NeiraEngine.World
{
    public static class WorldDrawer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="begin_mode"></param>
        /// <param name="meshes"></param>
        /// <param name="program"></param>
        /// <param name="transformation"></param>
        /// <param name="animation_time"></param>
        /// <param name="draw_mode">0 = basic; 1 = basic + materials; 2 = basic + materials + record previous model matrix</param>
        public static void drawMeshesGL(Render.OpenGL.BeginMode begin_mode, List<UniqueMesh> meshes, Program program, Matrix4 transformation, float animation_time, int draw_mode)
        {
            WorldDrawerGL.drawMeshes(begin_mode, meshes, program, transformation, animation_time, draw_mode);
        }

        public static void drawLightsGL(Render.OpenGL.BeginMode begin_mode, List<Light> lights, Program program, Matrix4 transformation, float animation_time, bool display_light_bounds)
        {
            WorldDrawerGL.drawLights(begin_mode, lights, program, transformation, animation_time, display_light_bounds);
        }

        public static void drawLightBoundsGL(Light light, Program program)
        {
            WorldDrawerGL.drawLightBounds(light, program);
        }
    }
}

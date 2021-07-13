using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;

using OpenTK;
using OpenTK.Graphics.OpenGL;

using NeiraEngine.Components;
using NeiraEngine.Output;
using NeiraEngine.Render.OpenGL;
using NeiraEngine.World;

namespace NeiraEngine.Render
{
    public enum RenderAPI
    {
        OpenGL,
        DirectX,
        Vulkan
    };

    public static class RenderDriver
    {
        public static Resolution resolution;

        public static Assembly Module;

        internal static void Init(Resolution Resolution)
        {
            resolution = Resolution;
            ProgramLoader.Init(ClientConfig.glsl_version);
        }

        internal static void Update()
        {
            Module.GetType($"{Module.GetName().Name}.Main").IsSubclassOf(typeof(RenderModuleGL));

        }
    }
}

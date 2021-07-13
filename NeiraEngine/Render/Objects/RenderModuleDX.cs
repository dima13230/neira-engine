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
    public class RenderModuleDX : RenderModule
    {
        public RenderModuleDX(Resolution res)
            : base(res)
        {

        }
    }
}
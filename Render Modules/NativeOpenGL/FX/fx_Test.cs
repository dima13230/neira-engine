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
    public class fx_Test : RenderEffect
    {

        public fx_Test(string glsl_effect_path, Resolution full_resolution)
            : base(glsl_effect_path, full_resolution)
        { }

        protected override void load_Programs()
        {

        }

        protected override void load_Buffers()
        {

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
            
        }


    }
}

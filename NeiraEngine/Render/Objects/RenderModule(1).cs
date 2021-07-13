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
    public class RenderModule
    {
        // Common for all modules
        public float currentFPS;
        public Scene scene;
        public Resolution resolution;

        public RenderModule()
        {
        }

        public virtual void Load() { }
        public virtual void Unload() { }
        public virtual void Update() { }
        public virtual void Render() { }
    }
}
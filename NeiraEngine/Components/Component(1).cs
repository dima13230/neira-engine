using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Graphics.OpenGL;

using NeiraEngine.Client;
using NeiraEngine.World;
using NeiraEngine.Render;

namespace NeiraEngine.Components
{
    public class Component
    {

        public WorldObject worldObject
        { get; private set; }

        public Scene scene;

        public Component(WorldObject relatedTo)
        {
            worldObject = relatedTo;
            scene = worldObject.parentScene;
        }

        public virtual void Start() { }
        public virtual void Update() { }
        public virtual void Remove() { }
        //public virtual void OnRender(BeginMode beginMode, Program program, float sceneTime) { }
    }
}

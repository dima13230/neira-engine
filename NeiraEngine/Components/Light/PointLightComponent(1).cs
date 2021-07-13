using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;

using NeiraEngine.Client;

using NeiraEngine.World;
using NeiraEngine.World.Model;
using NeiraEngine.World.Lights;

namespace NeiraEngine.Components
{
    class PointLightComponent : Component
    {
        public Vector3 color = new Vector3(1);
        public float intensity = 1;
        public float falloff = 1;
        public bool shadow = true;
        public Mesh light_mesh = Scene.world_loader.pLight_mesh;
        public Matrix4 transformation = Matrix4.Identity;

        public pLight light;

        public PointLightComponent(WorldObject relatedTo) : base(relatedTo)
        { }

        public override void Start()
        {
            light = new pLight(worldObject.id + "_plight", color, intensity, falloff, shadow, light_mesh, transformation, worldObject.parentScene);
            worldObject.parentScene.light_manager.addLight(light);
        }

        public override void Update()
        {
            light.spatial.position = worldObject.spatial.position;
        }

        public override void Remove()
        {
            worldObject.parentScene.light_manager.removeLight(light);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;

using NeiraEngine.World;
using NeiraEngine.World.Model;
using NeiraEngine.World.Lights;

namespace NeiraEngine.Components
{
    class SpotLightComponent : Component
    {
        public Vector3 color = new Vector3(1);
        public float intensity = 1;
        public float falloff = 2;
        public float spot_angle = 40;
        public float spot_blur = MathHelper.DegreesToRadians(70.0f);
        public bool shadow = true;
        public Mesh light_mesh = Scene.world_loader.sLight_mesh;
        public Matrix4 transformation = Matrix4.Identity;

        public sLight light;

        public SpotLightComponent(WorldObject relatedTo) : base(relatedTo)
        { }

        public override void Start()
        {
            light = new sLight(worldObject.id, color, intensity, falloff, spot_angle, spot_blur, shadow, light_mesh, transformation, worldObject.parentScene);
            worldObject.parentScene.light_manager.addLight(light);
        }

        public override void Update()
        {
            light.spatial.transformation = worldObject.spatial.transformation;
        }

        public override void Remove()
        {
            worldObject.parentScene.light_manager.removeLight(light);
        }
    }
}

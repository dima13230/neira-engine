using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;

using NeiraEngine.World;
using NeiraEngine.World.Lights;

namespace NeiraEngine.Components
{
    class DirectionalLightComponent : Component
    {
        public Vector3 position = new Vector3(0);
        public bool shadow = true;
        public float[] cascade_splits = new float[]
                {
                    ClientConfig.near_far.X, 5.0f, 20.0f, 50.0f, 100.0f
                };

        public dLight light;

        public DirectionalLightComponent(WorldObject relatedTo) : base(relatedTo)
        { }

        public override void Start()
        {
            light = new dLight(worldObject.id, shadow, position, cascade_splits, worldObject.parentScene);
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

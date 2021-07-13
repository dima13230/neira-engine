using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BulletSharp;
using BulletSharp.Math;

namespace NeiraEngine.Physics
{
    public static class PhysicsWorld
    {
        public static DiscreteDynamicsWorld world { get; set; }

        public static List<CollisionShape> collision_shapes { get; set; }

        public static List<RigidBodyObject> rigid_body_objects { get; set; }

        public static bool paused { get; set; }

        public static void Init(float gravity, Dispatcher dispatcher, DbvtBroadphase broadphase, SequentialImpulseConstraintSolver solver, CollisionConfiguration collision_config)
        {
            world = new DiscreteDynamicsWorld(dispatcher, broadphase, solver, collision_config);
            world.DispatchInfo.AllowedCcdPenetration = 0.0001f;

            world.Gravity = new Vector3(0, gravity, 0);
            
            collision_shapes = new List<CollisionShape>();
            rigid_body_objects = new List<RigidBodyObject>();

            paused = false;
        }

    }
}
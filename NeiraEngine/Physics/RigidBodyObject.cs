using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BulletSharp;
using BulletSharp.SoftBody;
using BulletSharp.Math;

namespace NeiraEngine.Physics
{
    public class RigidBodyObject : PhysicsObject
    {

        public RigidBodyObject(string id, RigidBody body, Vector3 scale, bool kinematic)
            : base(id, body, scale, kinematic)
        {

        }

    }
}

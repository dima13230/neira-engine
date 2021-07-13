using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BulletSharp;
using BulletSharp.Math;

using NeiraEngine;
using NeiraEngine.Render;
using NeiraEngine.World;
using NeiraEngine.World.Model;
using NeiraEngine.Physics;

namespace NeiraEngine.Components
{
    class RigidBodyComponent : Component
    {
        public float mass = 1;
        public float friction = 1;
        public float restitution = 1;
        public bool dynamic = true;
        public bool kinematic = false;

        MeshComponent meshComponent;

        public RigidBodyComponent(WorldObject relatedTo) : base(relatedTo)
        { }

        public override void Start()
        {
            meshComponent = worldObject.GetComponent<MeshComponent>();

            if (meshComponent != null)
            {
                foreach (UniqueMesh mesh in meshComponent.meshes)
                {
                    CollisionShape shape = PhysicsLoader.loadConvexHull(mesh.mesh, mesh.transformation.ExtractScale());

                    Matrix transformation = mesh.transformation.ClearScale();

                    RigidBodyObject rigid_body_object = PhysicsHelper.createLocalRigidBody(
                        worldObject.id + "_" + mesh.id.Replace('.', '_'),
                        dynamic,
                        kinematic,
                        mass,
                        restitution,
                        friction,
                        transformation,
                        shape,
                        shape.LocalScaling * 2,
                        shape.LocalScaling);
                    mesh.physics_object = rigid_body_object;
                }
            }
        }

        public override void Remove()
        {
            if (meshComponent != null)
            {
                foreach (UniqueMesh mesh in meshComponent.meshes)
                {
                    mesh.physics_object = null;
                }
            }
        }
    }
}

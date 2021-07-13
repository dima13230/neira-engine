﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BulletSharp;
using BulletSharp.SoftBody;
using BulletSharp.Math;

namespace NeiraEngine.Physics
{
    static class PhysicsHelper
    {

        public static RigidBodyObject createLocalRigidBody(
            string id,
            bool dynamic, bool kinematic, 
            float mass, float restitution, float friction,
            Matrix startTransform, 
            CollisionShape shape, Vector3 dimensions, Vector3 scale)
        {

            BulletSharp.Math.Vector3 localInertia = Vector3.Zero;
            if (dynamic)
            {
                shape.CalculateLocalInertia(mass, out localInertia);
            }
            else
            {
                mass = 0.0f;
            }

            DefaultMotionState myMotionState = new DefaultMotionState(startTransform);
            

            RigidBodyConstructionInfo rbInfo = new RigidBodyConstructionInfo(mass, myMotionState, shape, localInertia);
            rbInfo.Restitution = restitution;


            //rbInfo.LinearDamping = 0.8f;
            //rbInfo.AngularDamping = 0.1f;

            //rbInfo.LinearSleepingThreshold = rbInfo.LinearSleepingThreshold * 1000.0f;
            //rbInfo.AngularSleepingThreshold = rbInfo.AngularSleepingThreshold * 1000.0f;

            rbInfo.Friction = friction;
            rbInfo.RollingFriction = 0.01f;


            RigidBody body = new RigidBody(rbInfo);
            rbInfo.Dispose();

            if(kinematic)
            {
                body.ActivationState = ActivationState.DisableDeactivation;
                body.CollisionFlags = CollisionFlags.KinematicObject;
            }


            body.SetSleepingThresholds(0.2f, 0.9f);
            //body.Friction = 1.0f;
            //body.RollingFriction = 1.0f;


            //------------------------------------------------------
            // Create a rigid body object and all to world
            //------------------------------------------------------

            body.UserObject = id;
            RigidBodyObject rigid_body_object = new RigidBodyObject(id, body, scale, kinematic);

            PhysicsWorld.rigid_body_objects.Add(rigid_body_object);
            PhysicsWorld.collision_shapes.Add(shape);
            PhysicsWorld.world.AddRigidBody(body);

            return rigid_body_object;

        }
    }
}

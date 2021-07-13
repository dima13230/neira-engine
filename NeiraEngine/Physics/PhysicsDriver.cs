using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BulletSharp;
using BulletSharp.Math;

namespace NeiraEngine.Physics
{
    public static class PhysicsDriver
    {
        private static CollisionDispatcher _dispatcher;
        private static DbvtBroadphase _broadphase;
        private static SequentialImpulseConstraintSolver _solver;

        private static CollisionConfiguration _collision_config;


        private static float _gravity = -28.91f;

        //------------------------------------------------------
        // Picking Objects / Properties
        //------------------------------------------------------
        private static RigidBody _picked_body;
        private static TypedConstraint _pick_constraint;

        public static bool zoom_picked_distance { get; private set; }

        public static float picking_distance_current { get; private set; }

        public static float picking_distance_original { get; private set; }

        public static float picking_distance_minimum { get; private set; }


        //------------------------------------------------------
        // Character Objects / Properties
        //------------------------------------------------------
        public static PairCachingGhostObject ghostObject;
        public static KinematicCharacterController character;

        public static void Load()
        {
            // Collision configuration contains default setup for memory, collision setup
            _collision_config = new DefaultCollisionConfiguration();
            _dispatcher = new CollisionDispatcher(_collision_config);
            _broadphase = new DbvtBroadphase();
            _solver = new SequentialImpulseConstraintSolver();

            PhysicsWorld.Init(_gravity, _dispatcher, _broadphase, _solver, _collision_config);

            picking_distance_minimum = 10.0f;
        }

        public static void Unload()
        {
            int i;

            // Remove constraints
            for (i = PhysicsWorld.world.NumConstraints - 1; i >= 0; i--)
            {
                TypedConstraint constraint = PhysicsWorld.world.GetConstraint(i);
                PhysicsWorld.world.RemoveConstraint(constraint);
                constraint.Dispose();
            }

            // Remove rigidbodies from the dynamics world and delete them
            for (i = PhysicsWorld.world.NumCollisionObjects - 1; i >= 0; i--)
            {
                CollisionObject obj = PhysicsWorld.world.CollisionObjectArray[i];
                RigidBody body = obj as RigidBody;
                if (body != null && body.MotionState != null)
                {
                    body.MotionState.Dispose();
                }
                PhysicsWorld.world.RemoveCollisionObject(obj);
                obj.Dispose();
            }

            // Delete collision shapes
            foreach (CollisionShape shape in PhysicsWorld.collision_shapes)
                shape.Dispose();
            PhysicsWorld.collision_shapes.Clear();


            // Delete the world
            PhysicsWorld.world.Dispose();
            _broadphase.Dispose();
            if (_dispatcher != null)
            {
                _dispatcher.Dispose();
            }
            _collision_config.Dispose();
        }


        public static void Reset()
        {
            foreach (RigidBodyObject rbo in PhysicsWorld.rigid_body_objects)
            {
                rbo.body.ClearForces();
                rbo.body.AngularVelocity = Vector3.Zero;
                rbo.body.LinearVelocity = Vector3.Zero;

                rbo.body.WorldTransform = rbo.original_transformation;
                rbo.body.MotionState.WorldTransform = rbo.original_transformation;
                rbo.body.Activate();
            }


            OverlappingPairCache pair_cache = PhysicsWorld.world.Broadphase.OverlappingPairCache;
            AlignedBroadphasePairArray pair_array = pair_cache.OverlappingPairArray;
            for(int i = 0; i < pair_array.Count; i++)
            {
                pair_cache.CleanOverlappingPair(pair_array[i], PhysicsWorld.world.Dispatcher);
            }

            _solver.Reset();
            PhysicsWorld.world.ClearForces();
            _broadphase.ResetPool(_dispatcher);

            PhysicsWorld.paused = false;
        }


        internal static void Update(float frame_time, float target_fps, float current_fps)
        {
            if (!PhysicsWorld.paused)
            {
                PhysicsWorld.world.StepSimulation(frame_time, (int)(Math.Max(target_fps / current_fps, 10)));
                //character.UpdateAction(physics_world.world, frame_time);
            }
        }


        public static void Pause()
        {
            PhysicsWorld.paused = !PhysicsWorld.paused;
        }


        //------------------------------------------------------
        // Picking
        //------------------------------------------------------

        public static void pickObject(Vector3 rayFrom, Vector3 rayTo, bool use6Dof)
        {
            BulletSharp.Math.Vector3 from = rayFrom;
            BulletSharp.Math.Vector3 to = rayTo;
            ClosestRayResultCallback rayCallback = new ClosestRayResultCallback(ref from, ref to);
            PhysicsWorld.world.RayTest(rayFrom, rayTo, rayCallback);

            if (rayCallback.HasHit)
            {

                Vector3 pickPos = rayCallback.HitPointWorld;
                RigidBody body = rayCallback.CollisionObject as RigidBody;

                if (body != null)
                {
                    
                    if (!(body.IsStaticObject || body.IsKinematicObject))
                    {
                        _picked_body = body;
                        _picked_body.ActivationState = ActivationState.DisableDeactivation;

                        Vector3 localPivot = Vector3.TransformCoordinate(pickPos, Matrix4.Invert(body.CenterOfMassTransform));


                        if (use6Dof)
                        {
                            Generic6DofConstraint dof6 = new Generic6DofConstraint(body, Matrix.Translation(localPivot), false);
                            dof6.LinearLowerLimit = Vector3.Zero;
                            dof6.LinearUpperLimit = Vector3.Zero;
                            dof6.AngularLowerLimit = Vector3.Zero;
                            dof6.AngularUpperLimit = Vector3.Zero;

                            PhysicsWorld.world.AddConstraint(dof6);
                            _pick_constraint = dof6;

                            dof6.SetParam(ConstraintParam.StopCfm, 0.8f, 0);
                            dof6.SetParam(ConstraintParam.StopCfm, 0.8f, 1);
                            dof6.SetParam(ConstraintParam.StopCfm, 0.8f, 2);
                            dof6.SetParam(ConstraintParam.StopCfm, 0.8f, 3);
                            dof6.SetParam(ConstraintParam.StopCfm, 0.8f, 4);
                            dof6.SetParam(ConstraintParam.StopCfm, 0.8f, 5);

                            dof6.SetParam(ConstraintParam.StopErp, 0.1f, 0);
                            dof6.SetParam(ConstraintParam.StopErp, 0.1f, 1);
                            dof6.SetParam(ConstraintParam.StopErp, 0.1f, 2);
                            dof6.SetParam(ConstraintParam.StopErp, 0.1f, 3);
                            dof6.SetParam(ConstraintParam.StopErp, 0.1f, 4);
                            dof6.SetParam(ConstraintParam.StopErp, 0.1f, 5);
                        }
                        else
                        {
                            Point2PointConstraint p2p = new Point2PointConstraint(body, localPivot);
                            PhysicsWorld.world.AddConstraint(p2p);
                            _pick_constraint = p2p;
                            p2p.Setting.ImpulseClamp = 30;
                            //very weak constraint for picking
                            p2p.Setting.Tau = 0.001f;
                            /*
                            p2p.SetParam(ConstraintParams.Cfm, 0.8f, 0);
                            p2p.SetParam(ConstraintParams.Cfm, 0.8f, 1);
                            p2p.SetParam(ConstraintParams.Cfm, 0.8f, 2);
                            p2p.SetParam(ConstraintParams.Erp, 0.1f, 0);
                            p2p.SetParam(ConstraintParams.Erp, 0.1f, 1);
                            p2p.SetParam(ConstraintParams.Erp, 0.1f, 2);
                            */
                        }

                        picking_distance_original = (pickPos - rayFrom).Length;
                        picking_distance_current = picking_distance_original;
                    }
                }
            }
        }

        public static void moveObject(Vector3 rayFrom, Vector3 rayTo)
        {
            if (_pick_constraint != null)
            {
                Vector3 dir = rayTo - rayFrom;
                dir.Normalize();
                dir *= picking_distance_current;

                if (_pick_constraint.ConstraintType == TypedConstraintType.D6)
                {
                    Generic6DofConstraint pickCon = _pick_constraint as Generic6DofConstraint;

                    //keep it at the same picking distance
                    Matrix tempFrameOffsetA = pickCon.FrameOffsetA;
                    tempFrameOffsetA.Origin = rayFrom + dir;
                    pickCon.SetFrames(tempFrameOffsetA, pickCon.FrameOffsetB);
                }
                else
                {
                    Point2PointConstraint pickCon = _pick_constraint as Point2PointConstraint;

                    //keep it at the same picking distance
                    pickCon.PivotInB = rayFrom + dir;
                }
            }
        }

        public static void releaseObject()
        {
            if (_pick_constraint != null && PhysicsWorld.world != null)
            {
                PhysicsWorld.world.RemoveConstraint(_pick_constraint);
                _pick_constraint.Dispose();
                _pick_constraint = null;
                _picked_body.ForceActivationState(ActivationState.ActiveTag);
                _picked_body.DeactivationTime = 0;
                _picked_body = null;
                zoom_picked_distance = false;
            }
        }

        public static void throwObject(Vector3 direction)
        {
            if (_pick_constraint != null && PhysicsWorld.world != null)
            {
                _picked_body.ApplyCentralImpulse(direction * 100.0f);
            }
            releaseObject();
        }

        public static void zoomPickedObject()
        {
            if (_pick_constraint != null && PhysicsWorld.world != null)
            {
                if (zoom_picked_distance)
                {
                    picking_distance_current = picking_distance_original;
                }
                else
                {
                    picking_distance_current = (picking_distance_original < picking_distance_minimum) ? picking_distance_original : picking_distance_minimum;
                }
                zoom_picked_distance = !zoom_picked_distance;
            }
        }

    }
}

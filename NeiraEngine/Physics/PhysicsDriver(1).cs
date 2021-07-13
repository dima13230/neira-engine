﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BulletSharp;
using BulletSharp.Math;

namespace NeiraEngine.Physics
{
    public class PhysicsDriver
    {
        public PhysicsWorld physics_world { get; private set; }


        private CollisionDispatcher _dispatcher;
        private DbvtBroadphase _broadphase;
        private SequentialImpulseConstraintSolver _solver;


        private CollisionConfiguration _collision_config;


        private float _gravity = -28.91f;

        //------------------------------------------------------
        // Picking Objects / Properties
        //------------------------------------------------------
        private RigidBody _picked_body;
        private TypedConstraint _pick_constraint;


        private bool _zoom_picked_distance;
        public bool zoom_picked_distance
        {
            get { return _zoom_picked_distance; }
        }

        private float _picking_distance_current;
        public float picking_distance_current
        {
            get { return _picking_distance_current; }
        }

        private float _picking_distance_original;
        public float picking_distance_original
        {
            get { return _picking_distance_original; }
        }

        private float _picking_distance_minimum;
        public float picking_distance_minimum
        {
            get { return _picking_distance_minimum; }
        }


        //------------------------------------------------------
        // Character Objects / Properties
        //------------------------------------------------------
        public PairCachingGhostObject ghostObject;
        public KinematicCharacterController character;



        public PhysicsDriver()
        {
            // Collision configuration contains default setup for memory, collision setup
            _collision_config = new DefaultCollisionConfiguration();
            _dispatcher = new CollisionDispatcher(_collision_config);
            _broadphase = new DbvtBroadphase();
            _solver = new SequentialImpulseConstraintSolver();

            physics_world = new PhysicsWorld(_gravity, _dispatcher, _broadphase, _solver, _collision_config);

            _picking_distance_minimum = 10.0f;
        }


        public void load()
        {

        }


        public void unload()
        {
            int i;

            // Remove constraints
            for (i = physics_world.world.NumConstraints - 1; i >= 0; i--)
            {
                TypedConstraint constraint = physics_world.world.GetConstraint(i);
                physics_world.world.RemoveConstraint(constraint);
                constraint.Dispose();
            }

            // Remove rigidbodies from the dynamics world and delete them
            for (i = physics_world.world.NumCollisionObjects - 1; i >= 0; i--)
            {
                CollisionObject obj = physics_world.world.CollisionObjectArray[i];
                RigidBody body = obj as RigidBody;
                if (body != null && body.MotionState != null)
                {
                    body.MotionState.Dispose();
                }
                physics_world.world.RemoveCollisionObject(obj);
                obj.Dispose();
            }

            // Delete collision shapes
            foreach (CollisionShape shape in physics_world.collision_shapes)
                shape.Dispose();
            physics_world.collision_shapes.Clear();


            // Delete the world
            physics_world.world.Dispose();
            _broadphase.Dispose();
            if (_dispatcher != null)
            {
                _dispatcher.Dispose();
            }
            _collision_config.Dispose();
        }


        public void reset()
        {
            foreach (RigidBodyObject rbo in physics_world.rigid_body_objects)
            {
                rbo.body.ClearForces();
                rbo.body.AngularVelocity = Vector3.Zero;
                rbo.body.LinearVelocity = Vector3.Zero;

                rbo.body.WorldTransform = rbo.original_transformation;
                rbo.body.MotionState.WorldTransform = rbo.original_transformation;
                rbo.body.Activate();
            }


            OverlappingPairCache pair_cache = physics_world.world.Broadphase.OverlappingPairCache;
            AlignedBroadphasePairArray pair_array = pair_cache.OverlappingPairArray;
            for(int i = 0; i < pair_array.Count; i++)
            {
                pair_cache.CleanOverlappingPair(pair_array[i], physics_world.world.Dispatcher);
            }

            _solver.Reset();
            physics_world.world.ClearForces();
            _broadphase.ResetPool(_dispatcher);

            physics_world.paused = false;
        }


        public void update(float frame_time, float target_fps, float current_fps)
        {
            if (!physics_world.paused)
            {
                physics_world.world.StepSimulation(frame_time, (int)(Math.Max(target_fps / current_fps, 10)));
                //character.UpdateAction(physics_world.world, frame_time);
            }
        }


        public void pause()
        {
            physics_world.paused = !physics_world.paused;
        }


        //------------------------------------------------------
        // Picking
        //------------------------------------------------------

        public void pickObject(Vector3 rayFrom, Vector3 rayTo, bool use6Dof)
        {
            ClosestRayResultCallback rayCallback = new ClosestRayResultCallback(ref rayFrom, ref rayTo);
            physics_world.world.RayTest(rayFrom, rayTo, rayCallback);

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

                        Vector3 localPivot = Vector3.TransformCoordinate(pickPos, Matrix.Invert(body.CenterOfMassTransform));


                        if (use6Dof)
                        {
                            Generic6DofConstraint dof6 = new Generic6DofConstraint(body, Matrix.Translation(localPivot), false);
                            dof6.LinearLowerLimit = Vector3.Zero;
                            dof6.LinearUpperLimit = Vector3.Zero;
                            dof6.AngularLowerLimit = Vector3.Zero;
                            dof6.AngularUpperLimit = Vector3.Zero;

                            physics_world.world.AddConstraint(dof6);
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
                            physics_world.world.AddConstraint(p2p);
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

                        _picking_distance_original = (pickPos - rayFrom).Length;
                        _picking_distance_current = _picking_distance_original;
                    }
                }
            }
        }

        public void moveObject(Vector3 rayFrom, Vector3 rayTo)
        {
            if (_pick_constraint != null)
            {
               
                Vector3 dir = rayTo - rayFrom;
                dir.Normalize();
                dir *= _picking_distance_current;

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

        public void releaseObject()
        {
            if (_pick_constraint != null && physics_world.world != null)
            {
                physics_world.world.RemoveConstraint(_pick_constraint);
                _pick_constraint.Dispose();
                _pick_constraint = null;
                _picked_body.ForceActivationState(ActivationState.ActiveTag);
                _picked_body.DeactivationTime = 0;
                _picked_body = null;
                _zoom_picked_distance = false;
            }
        }

        public void throwObject(Vector3 direction)
        {
            if (_pick_constraint != null && physics_world.world != null)
            {
                _picked_body.ApplyCentralImpulse(direction * 100.0f);
            }
            releaseObject();
        }

        public void zoomPickedObject()
        {
            if (_pick_constraint != null && physics_world.world != null)
            {
                if (_zoom_picked_distance)
                {
                    _picking_distance_current = _picking_distance_original;
                }
                else
                {
                    _picking_distance_current = (_picking_distance_original < _picking_distance_minimum) ? _picking_distance_original : _picking_distance_minimum;
                }
                _zoom_picked_distance = !_zoom_picked_distance;
            }
        }

    }
}

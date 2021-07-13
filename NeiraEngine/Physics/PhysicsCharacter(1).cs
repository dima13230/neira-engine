using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BulletSharp;
using BulletSharp.Math;

namespace NeiraEngine.Physics
{
    class PhysicsCharacter
    {

        public PairCachingGhostObject ghostObject;
        public KinematicCharacterController character;

        public float size { get; set; }



        public PhysicsCharacter(PhysicsWorld physics_world, Vector3 start_position, float size)
        {
            //AxisSweep3 Broadphase = new AxisSweep3(new Vector3(-1000, -1000, -1000), new Vector3(1000, 1000, 1000));

            this.size = size;

            Matrix start_transformation = Matrix.Translation(start_position);
            ghostObject = new PairCachingGhostObject();
            ghostObject.WorldTransform = start_transformation;
            //ghostObject.DeactivationTime = 0.001f;

            // For character collisions
            physics_world.world.Broadphase.OverlappingPairCache.SetInternalGhostPairCallback(new GhostPairCallback());

            float characterSize = this.size;
            float characterHeight = characterSize;
            float characterWidth = characterSize;
            //_picking_distance_minimum = characterWidth * pickingDistScale;
            ConvexShape capsule = new CapsuleShape(characterWidth, characterHeight);
            //capsule.CalculateLocalInertia(1.0f);


            capsule.Margin = 0.1f;

            //capsule.Margin = characterHeight;
            ghostObject.CollisionShape = capsule;
            ghostObject.CollisionFlags = CollisionFlags.CharacterObject;
            ghostObject.UserObject = "me";
            

            float stepHeight = characterHeight / 2.0f;
            character = new KinematicCharacterController(ghostObject, capsule, stepHeight);
            character.SetJumpSpeed(characterHeight * 8.0f);
            //character.SetMaxJumpHeight(characterHeight * 1.0f);
            //character.SetFallSpeed(-physics_world.world.Gravity.Y * 1.0f);
            character.Gravity = -physics_world.world.Gravity.Y * 1.0f;
            character.MaxSlope = OpenTK.MathHelper.DegreesToRadians(50.0f);
            character.SetUseGhostSweepTest(true);


            physics_world.world.AddCollisionObject(ghostObject, CollisionFilterGroups.CharacterFilter, CollisionFilterGroups.StaticFilter | CollisionFilterGroups.DefaultFilter);
            physics_world.collision_shapes.Add(capsule);
            physics_world.world.AddAction(character);
        }


        public Vector3 getPosition()
        {
            return EngineHelper.bullet2neira(character.GhostObject.WorldTransform).ExtractTranslation() + new Vector3(0.0f, size/1.2f, 0.0f);
        }
    }
}

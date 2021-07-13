using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NeiraEngine.World;
using NeiraEngine.World.Role;
using NeiraEngine.World.View;
using NeiraEngine.Physics;

namespace NeiraEngine.Game
{
    public class Player
    {
        public ControllableWorldObject character { get; private set; }

        public Camera camera { get; private set; }

        private PhysicsCharacter _physics_character;

        private bool _physical;

        public bool enable_flashlight { get; set; }

        public Player()
        {
            _physical = false;
        }


        public void controlAndWatch(Camera camera)
        {
            camera.unfollowCharacter();
            this.camera = camera;
            character = camera;
        }

        public void controlAndWatch(ControllableWorldObject character, Camera camera)
        {
            this.character = character;
            this.camera = camera;
            this.camera.followCharacter(this.character);
        }

        public void getPhysical()
        {
            _physics_character = new PhysicsCharacter(-character.spatial.position, 2.0f);
            _previous_position = character.spatial.position;
            _physical = false;
        }



        //------------------------------------------------------
        // Player Property Controls
        //------------------------------------------------------
        public void togglePhysical()
        {
            _physical = !_physical;
            if(_physical)
            {
                _previous_position = character.spatial.position;
                BulletSharp.Math.Vector3 temp_position = -character.spatial.position;
                _physics_character.character.Warp(ref temp_position);
            }
            else
            {
                character.spatial.position = -_physics_character.getPosition();
                camera.resetSmoothMovement();
            }
        }


        public bool toggleFlashlight()
        {
            enable_flashlight = !enable_flashlight;
            return enable_flashlight;
        }

        //------------------------------------------------------
        // Player Controls
        //------------------------------------------------------
        public void smoothMovement(float smooth_movement_delay)
        {
            if (!_physical) camera.smoothMovement(smooth_movement_delay);
        }


        public void rotate(Vector3 mouse_position_delta, float smooth_factor)
        {
            // Set character angles based on mouse position delta 
            Vector3 temp_angles = character.spatial.rotation_angles + mouse_position_delta;
            temp_angles.X = MathHelper.Clamp(temp_angles.X, -90.0f, 90.0f);
            character.spatial.rotation_angles = temp_angles;

            character.rotate(
                character.spatial.rotation_angles.X,
                character.spatial.rotation_angles.Y,
                character.spatial.rotation_angles.Z,
                smooth_factor
            );
        }


        public void moveForeward()
        {
            character.moveForeward();
        }

        public void moveBackward()
        {
            character.moveBackward();
        }

        public void strafeRight()
        {
            character.strafeRight();
        }

        public void strafeLeft()
        {
            character.strafeLeft();
        }

        public void moveUp()
        {
            if (_physical) _physics_character.character.Jump(); else character.moveUp();
        }

        public void moveDown()
        {
            character.moveDown();
        }

        public void run(bool enable)
        {
            character.running = enable;
        }

        public void sprint(bool enable)
        {
            character.sprinting = enable;
        }

        private Vector3 _previous_position;

        public void updatePhysicalPosition()
        {
            if (_physical)
            {
                BulletSharp.Math.Vector3 walk_direction = -(character.spatial.position - _previous_position);

                _physics_character.character.SetWalkDirection(ref walk_direction);


                character.spatial.position = -_physics_character.getPosition();
                _previous_position = character.spatial.position;
            }
            else
            {
                BulletSharp.Math.Vector3 temp_position = -character.spatial.position;
                _physics_character.character.Warp(ref temp_position);
            }
        }

    }
}

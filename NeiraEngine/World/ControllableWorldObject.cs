﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;


namespace NeiraEngine.World
{
    public class ControllableWorldObject : WorldObject
    {


        protected float _movement_speed_walk;
        public float movement_speed_walk
        {
            get { return _movement_speed_walk; }
            set { _movement_speed_walk = value; }
        }

        protected float _movement_speed_run;
        public float movement_speed_run
        {
            get { return _movement_speed_run; }
            set { _movement_speed_run = value; }
        }

        protected bool _running;
        public bool running
        {
            get { return _running; }
            set { _running = value; }
        }

        protected bool _sprinting;
        public bool sprinting
        {
            get { return _sprinting; }
            set { _sprinting = value; }
        }


        public ControllableWorldObject(string id, SpatialData spatial_data, Scene scene)
            : this(id, spatial_data, scene, 0.02f, 0.2f)
        { }

        public ControllableWorldObject(string id, SpatialData spatial_data, Scene scene, float movement_speed_walk, float movement_speed_run)
            : base(id, spatial_data, scene: scene)
        {
            _movement_speed_walk = movement_speed_walk;
            _movement_speed_run = movement_speed_run;
            _previous_rotation = new Quaternion();
        }




        //------------------------------------------------------
        // Rotation Override
        //------------------------------------------------------
        private Quaternion _previous_rotation;

        public void rotate(float x_angle, float y_angle, float z_angle, float smooth_factor)
        {
            _spatial.rotation_angles = new Vector3(x_angle, y_angle, z_angle);

            Quaternion current_rotation = EngineHelper.createQuaternion(x_angle, y_angle, z_angle);
            current_rotation = Quaternion.Slerp(_previous_rotation, current_rotation, smooth_factor);

            _spatial.rotation_matrix = Matrix4.CreateFromQuaternion(current_rotation);
            _spatial.rotation_matrix = Matrix4.Transpose(_spatial.rotation_matrix);


            _spatial.look = (_spatial.rotation_matrix).Column2.Xyz;
            _spatial.up = (_spatial.rotation_matrix).Column1.Xyz;
            _spatial.strafe = (_spatial.rotation_matrix).Column0.Xyz;

            _previous_rotation = current_rotation;
        }


        //------------------------------------------------------
        // View Based Movement
        //------------------------------------------------------

        private float getMovementSpeed()
        {
            if (_sprinting)
            {
                return _movement_speed_run * 10.0f;
            }
            else if (_running)
            {
                return _movement_speed_run;
            }
            else
            {
                return _movement_speed_walk;
            }
        }

        public void moveForeward()
        {
            moveForeward(getMovementSpeed());
        }

        public void moveBackward()
        {
            moveBackward(getMovementSpeed());
        }

        public void moveUp()
        {
            moveUp(getMovementSpeed());
        }

        public void moveDown()
        {
            moveDown(getMovementSpeed());
        }

        public void strafeRight()
        {
            strafeRight(getMovementSpeed());
        }

        public void strafeLeft()
        {
            strafeLeft(getMovementSpeed());
        }

    }
}

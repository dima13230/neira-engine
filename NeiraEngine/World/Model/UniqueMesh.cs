using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;

using NeiraEngine.Animation;
using NeiraEngine.Physics;
using NeiraEngine.Components;

namespace NeiraEngine.World.Model
{
    public class UniqueMesh
    {
        public string id { get; set; }


        protected Matrix4 _base_transformation;
        public Matrix4 base_transformation
        {
            get { return _base_transformation; }
            set { _base_transformation = value; }
        }



        protected Matrix4 _transformation;
        public Matrix4 transformation
        {
            get { return _transformation; }
            set { _transformation = value; }
        }

        protected Matrix4 _previous_transformation;
        public Matrix4 previous_transformation
        {
            get { return _previous_transformation; }
            set { _previous_transformation = value; }
        }
        public Mesh mesh { get; set; }



        private ObjectAnimator _animator;
        public ObjectAnimator animator
        {
            get { return _animator; }
            set
            {
                animated = true;
                _animator = value;
            }
        }
        public bool animated { get; set; }



        private PhysicsObject _physics_object;
        public PhysicsObject physics_object
        {
            get { return _physics_object; }
            set
            {
                physical = value == null ? false : true;
                _physics_object = value;
            }
        }
        public bool physical { get; set; }

        public UniqueMesh(string id, Mesh mesh, Matrix4 transformation)
        {
            this.id = id;
            this.mesh = mesh;
            _base_transformation = transformation;
            _transformation = _base_transformation;
            _previous_transformation = _base_transformation;
            animated = false;
            physical = false;
        }
    }
}

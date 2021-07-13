using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;

using NeiraEngine.World.Model;
using NeiraEngine.Animation;

namespace NeiraEngine.World.Lights
{
    public class Light : WorldObject
    {

        public const string type_spot = "SPOT";
        public const string type_point = "POINT";
        public const string type_directional = "DIRECTIONAL";

        public int lid { get; set; }
        public int sid { get; set; }
        public bool enabled { get; set; }

        public bool shadowed { get; set; }

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

        public string type { get; set; }

        public Vector3 color { get; set; }

        public float intensity { get; set; }

        public float object_emission { get; set; }

        public float falloff { get; set; }

        public float _spot_angle { get; set; }

        public float _spot_blur { get; set; }

        protected UniqueMesh _unique_mesh;
        public UniqueMesh unique_mesh
        {
            get { return _unique_mesh; }
        }

        protected UniqueMesh _bounding_unique_mesh;
        public UniqueMesh bounding_unique_mesh
        {
            get { return _bounding_unique_mesh; }
        }

        protected Matrix4 _bounds_matrix;
        public Matrix4 bounds_matrix
        {
            get { return _bounds_matrix; }
        }



        public Light(string id, Scene scene, string type, Vector3 color, float intensity, float falloff, bool shadow, Mesh light_mesh, Matrix4 transformation)
            : base(id, new SpatialData(transformation), scene: scene)
        {
            lid = sid = -1;
            enabled = false;
            animated = false;
            this.type = type;
            this.color = color;
            this.intensity = intensity;
            this.falloff = falloff;
            shadowed = shadow;
            _bounds_matrix = Matrix4.Identity;

            object_emission = this.intensity;
        }

    }
}

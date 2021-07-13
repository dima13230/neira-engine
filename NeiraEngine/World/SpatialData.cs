using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiraEngine.World
{
    [Serializable]
    public class SpatialData
    {

        public Vector3 position { get; set; }
        public Vector3 look { get; set; }
        public Vector3 up { get; set; }
        public Vector3 strafe { get; set; }
        public Vector3 scale { get; set; }
        public Vector3 rotation_angles { get; set; }


        public Matrix4 position_matrix => Matrix4.CreateTranslation(position);


        private Matrix4 _rotation_matrix;
        public Matrix4 rotation_matrix
        {
            get { return _rotation_matrix; }
            set
            {
                _rotation_matrix = value;
               
                look = -_rotation_matrix.Row2.Xyz;
                up = -_rotation_matrix.Row1.Xyz;
                strafe = -_rotation_matrix.Row0.Xyz;
            }
        }


        public Matrix4 scale_matrix => Matrix4.CreateScale(scale);



        public Matrix4 transformation
        {
            get
            {
                return (position_matrix * rotation_matrix);
            }
            set
            {
                position = value.ExtractTranslation();
                scale = value.ExtractScale();
                rotation_matrix = Matrix4.CreateFromQuaternion(value.ExtractRotation());
            }
        }


        public Matrix4 model_view
        {
            get
            {
                return transformation;
            }
        }
        public Matrix4 perspective { get; private set; }



        public SpatialData()
            : this(new Vector3(), new Vector3(), new Vector3())
        { }

        public SpatialData(Vector3 position, Vector3 look, Vector3 up)
        {
            this.position = position;
            this.look = look;
            this.up = up;
            scale = new Vector3(1.0f);

            rotation_matrix = Matrix4.LookAt(Vector3.Zero, this.look, this.up);
        }

        public SpatialData(Matrix4 transformation)
        {
            this.transformation = transformation;
        }

        public void setRotationMatrixFromEuler()
        {
            rotation_matrix = EngineHelper.rotate(rotation_angles.X, rotation_angles.Y, rotation_angles.Z);
        }

        public void setPerspective(float fov_degrees, float aspect, Vector2 near_far)
        {
            perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(fov_degrees), aspect, near_far.X, near_far.Y);
        }

        public void setPerspective(float fov_degrees, float aspect, float near, float far)
        {
            perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(fov_degrees), aspect, near, far);
        }

        public void setOrthographic(float left, float right, float bottom, float top, float near, float far)
        {
            perspective = Matrix4.CreateOrthographicOffCenter(left, right, bottom, top, near, far);
        }

        public void setOrthographic(float width, float height, float near, float far)
        {
            perspective = Matrix4.CreateOrthographic(width, height, near, far);
        }
    }
}

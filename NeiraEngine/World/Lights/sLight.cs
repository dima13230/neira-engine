﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;

using NeiraEngine.World.Model;

namespace NeiraEngine.World.Lights
{
    public class sLight : Light
    {

        public Matrix4 shadow_view_matrix
        {
            get
            {
                return Matrix4.CreateTranslation(-_spatial.position) * Matrix4.Transpose(_spatial.rotation_matrix);
            }
        }

        public Matrix4 shadow_perspective_matrix
        {
            get
            {
                return _spatial.perspective;
            }
        }

        public Matrix4 viewray_matrix
        {
            get
            {
                return Matrix4.Invert(shadow_view_matrix.ClearTranslation() * shadow_perspective_matrix);
            }
        }


        public sLight(string id, Vector3 color, float intensity, float falloff, float spot_angle, float spot_blur, bool shadow, Mesh light_mesh, Matrix4 transformation, Scene scene = null)
            : base(id, scene, type_spot, color, intensity, falloff, shadow, light_mesh, transformation)
        {
            _spot_angle = spot_angle / 2.0f;
            _spot_blur = spot_blur;


            // Create Light Object Mesh
            _unique_mesh = new UniqueMesh(id, light_mesh, transformation);

            // Create Light Bounds Mesh
            float spot_depth = falloff / 2.0f;
            float spot_radius = spot_depth * (float)Math.Tan(_spot_angle) * 2.0f;
            Vector3 scaler = new Vector3(
                    spot_radius,
                    spot_radius,
                    spot_depth
                );
            Vector3 shifter = new Vector3(
                    0.0f,
                    0.0f,
                    -scaler.Z
                );

            // Build full transformation
            _bounds_matrix = Matrix4.CreateScale(scaler) * Matrix4.CreateTranslation(shifter);
            transformation = _bounds_matrix * transformation.ClearScale();
            _bounding_unique_mesh = new UniqueMesh(id + "-bounds", light_mesh, transformation);

            _spatial.setPerspective(MathHelper.RadiansToDegrees(_spot_angle * 2), 1.0f, 0.1f, 100.0f);
        }


    }
}

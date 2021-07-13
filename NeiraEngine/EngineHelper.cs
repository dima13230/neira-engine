﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;

using NeiraEngine.World;
using NeiraEngine.World.Model;
using NeiraEngine.Input;

namespace NeiraEngine
{
    public static class EngineHelper
    {

        //------------------------------------------------------
        // Constants
        //------------------------------------------------------
        public enum size : int
        {
            ui = sizeof(uint),
            ui64 = sizeof(ulong),
            i = sizeof(int),
            i64 = sizeof(long),
            f = sizeof(float),
            vec2 = sizeof(float) * 2,
            vec3 = sizeof(float) * 3,
            vec4 = sizeof(float) * 4,
            mat2 = sizeof(float) * 4,
            mat3 = sizeof(float) * 9,
            mat4 = sizeof(float) * 16,
        }

        public static Mesh GetQuadMesh(float width, float height)
        {
            Mesh QuadMesh = new Mesh("quad");
            QuadMesh.vertices.Add(new Mesh.Vertex(new Vector3(0, 0, 0), new Vector3(0, 0, -1), new Vector3(0, 0, -1), new Vector2(0, 0), new Vector4(), new Vector4()));
            QuadMesh.vertices.Add(new Mesh.Vertex(new Vector3(width, 0, 0), new Vector3(0, 0, -1), new Vector3(0, 0, -1), new Vector2(1, 0), new Vector4(), new Vector4()));
            QuadMesh.vertices.Add(new Mesh.Vertex(new Vector3(0, height, 0), new Vector3(0, 0, -1), new Vector3(0, 0, -1), new Vector2(0, 1), new Vector4(), new Vector4()));
            QuadMesh.vertices.Add(new Mesh.Vertex(new Vector3(width, height, 0), new Vector3(0, 0, -1), new Vector3(0, 0, -1), new Vector2(1, 1), new Vector4(), new Vector4()));

            QuadMesh.indices.Add(0);
            QuadMesh.indices.Add(2);
            QuadMesh.indices.Add(1);

            QuadMesh.indices.Add(2);
            QuadMesh.indices.Add(3);
            QuadMesh.indices.Add(1);

            int[] vao = DAE_Loader.loadVAO(QuadMesh);
            QuadMesh.vbo = vao[0];
            QuadMesh.ibo = vao[1];
            QuadMesh.vao = vao[2];

            return QuadMesh;
        }

        // Resource Paths
        public static string path_render_modules { get { return Path.GetFullPath(getPath_ProjectBase() + "render_modules/"); } }
        public static string path_resources_base { get { return Path.GetFullPath(getPath_ProjectBase()); } }
        public static string path_resources_audio { get { return Path.GetFullPath(path_resources_base + "Audio/"); } }
        public static string path_resources_save_data { get { return Path.GetFullPath(path_resources_base + "SaveData/"); } }
        public static string path_resources_models {  get { return Path.GetFullPath(path_resources_base + "Models/"); } }
        public static string path_resources_prefabs { get { return Path.GetFullPath(path_resources_base + "Prefabs/"); } }
        public static string path_resources_scene { get { return Path.GetFullPath(path_resources_base + "Scene/"); } }
        public static string path_resources_textures { get { return Path.GetFullPath(path_resources_base + "Textures/"); } }
        public static string path_resources_textures_static { get { return Path.GetFullPath(path_resources_textures + "_static/"); } }
        public static string path_resources_textures_sprites { get { return Path.GetFullPath(path_resources_textures + "sprites/"); } }
        public static string path_resources_screenshots { get { return Path.GetFullPath(path_resources_base + "Screenshots/"); } }

        // Shader Paths
        public static string path_glsl_base { get { return Path.GetFullPath(getPath_ProjectBase() + "/Shaders/"); } }
        public static string path_glsl_common { get { return "common/"; } }
        public static string path_glsl_common_helpers { get { return path_glsl_common + "helpers/"; } }
        public static string path_glsl_common_ubo { get { return path_glsl_common + "ubo/"; } }

        // Common Shader File Paths
        public static string path_glsl_common_generic_vs { get { return path_glsl_common + "render_Texture2D.vert"; } }
        public static string path_glsl_common_generic_geometry { get { return path_glsl_common + "geometry.vert"; } }

        // Common Shader Helper File Paths
        public static string path_glsl_common_helper_linearDepth { get { return path_glsl_common_helpers + "linearDepth.include"; } }
        public static string path_glsl_common_helper_positionFromDepth { get { return path_glsl_common_helpers + "positionFromDepth.include"; } }
        public static string path_glsl_common_helper_culling { get { return path_glsl_common_helpers + "culling.include"; } }
        public static string path_glsl_common_helper_interpolation { get { return path_glsl_common_helpers + "interpolation.include"; } }
        public static string path_glsl_common_helper_fxaa { get { return path_glsl_common_helpers + "fxaa.include"; } }
        public static string path_glsl_common_helper_lightingFunctions { get { return path_glsl_common_helpers + "lightingFunctions.include"; } }
        public static string path_glsl_common_helper_shadowEvaluation { get { return path_glsl_common_helpers + "shadowEvaluation.include"; } }
        public static string path_glsl_common_helper_shadowMapping { get { return path_glsl_common_helpers + "shadowMapping.include"; } }
        public static string path_glsl_common_helper_voxelFunctions { get { return path_glsl_common_helpers + "voxelFunctions.include"; } }

        // Common UBO File Paths
        public static string path_glsl_common_ubo_gameConfig { get { return path_glsl_common_ubo + "0_gameConfig.ubo"; } }
        public static string path_glsl_common_ubo_cameraSpatials { get { return path_glsl_common_ubo + "1_cameraSpatials.ubo"; } }
        public static string path_glsl_common_ubo_bindlessTextures_Materials { get { return path_glsl_common_ubo + "2_bindlessTextures_Materials.ubo"; } }
        public static string path_glsl_common_ubo_shadowManifest { get { return path_glsl_common_ubo + "3_shadowManifest.ubo"; } }
        public static string path_glsl_common_ubo_shadowMatrices_Spot { get { return path_glsl_common_ubo + "4_shadowMatrices_Spot.ubo"; } }
        public static string path_glsl_common_ubo_shadowMatrices_Point { get { return path_glsl_common_ubo + "5_shadowMatrices_Point.ubo"; } }
        public static string path_glsl_common_ubo_shadowMatrices_Directional { get { return path_glsl_common_ubo + "6_shadowMatrices_Directional.ubo"; } }

        // Common Shader Extensions
        public static string path_glsl_common_ext_bindlessTextures { get { return "#extension GL_ARB_bindless_texture : require"; } }


        //------------------------------------------------------
        // Project Helpers
        //------------------------------------------------------
        public static string getProjectName()
        {
            return System.Reflection.Assembly.GetCallingAssembly().FullName.Split(',')[0];
        }

        private static string getPathTo(string search_path)
        {
            bool path_found = false;
            string cur_search = "../";
            string base_path = "";

            while (!path_found)
            {
                base_path = Path.GetFullPath(cur_search);
                string[] dirs = base_path.Split(Path.DirectorySeparatorChar);
                path_found = dirs[dirs.Length - 2] == search_path;
                cur_search += "../";
            }

            return base_path;
        }

        private static string getPathAfter(string path, string search_path)
        {
            bool path_found = false;
            string cur_search = Path.GetFullPath(path);
            string[] cur_search_split = cur_search.Split(Path.DirectorySeparatorChar).Reverse().ToArray();
            string path_after = "";

            foreach(string path_segment in cur_search_split)
            {
                path_after = $"{path_segment}{Path.DirectorySeparatorChar}{path_after}";
                path_found = path_after.Contains(search_path);
                if (path_found) break;
            }
            if(Path.HasExtension(path)) path_after = path_after.Substring(0, path_after.Length - 1);

            return path_after.Replace(search_path, "");
        }


        public static string getPath_ProjectBase()
        {
            ConfigReader conf = new ConfigReader(AppDomain.CurrentDomain.BaseDirectory + "config.cfg");
            string dir = "";
            if (conf.TryGetString("data_path", ref dir))
            {
                return Path.GetFullPath(dir) + "\\";
            }
            else
            {
                return getPathTo(getProjectName());
            }
        }

        public static string getPath_MaterialTextures(string filepath)
        {
            filepath = filepath.Replace("%20", " ").Replace('/', Path.DirectorySeparatorChar);

            string[] filepath_split = path_resources_textures.Split(Path.DirectorySeparatorChar);
            string resource_textures_directories = $"{filepath_split[filepath_split.Length - 2]}{Path.DirectorySeparatorChar}{filepath_split[filepath_split.Length - 1]}";

            filepath = getPathAfter(filepath, resource_textures_directories);
            filepath = Path.GetFullPath(path_resources_textures + filepath);
            return filepath;
        }


        //------------------------------------------------------
        // Data functions
        //------------------------------------------------------

        public static float lerp(float src0, float src1, float t)
        {
            return src0 + (src1 - src0) * t;
        }

        public static Matrix3 lerp(Matrix3 src0, Matrix3 src1, float t)
        {
            Vector3 row0 = Vector3.Lerp(src0.Row0, src1.Row0, t);
            Vector3 row1 = Vector3.Lerp(src0.Row1, src1.Row1, t);
            Vector3 row2 = Vector3.Lerp(src0.Row2, src1.Row2, t);

            return new Matrix3(row0, row1, row2);
        }

        public static Matrix4 lerp(Matrix4 src0, Matrix4 src1, float t)
        {
            Vector3 temp_translation = Vector3.Lerp(src0.ExtractTranslation(), src1.ExtractTranslation(), t);
            Vector3 temp_scale = Vector3.Lerp(src0.ExtractScale(), src1.ExtractScale(), t);
            Quaternion temp_rotation = Quaternion.Slerp(src0.ExtractRotation(), src1.ExtractRotation(), t);
            
            return createMatrix(temp_translation, temp_rotation, temp_scale);
        }

        public static float slerp(float src0, float src1, float t)
        {
            src0 = Math.Max(src0, 0.000001f);
            return (float)(Math.Pow(src1 / src0, t) * src0);
        }



        public static Matrix4 rotate(float x_angle, float y_angle, float z_angle)
        {
            return Matrix4.CreateFromQuaternion(createQuaternion(x_angle, y_angle, z_angle));
        }



        //------------------------------------------------------
        // Data Creators
        //------------------------------------------------------
        public static Vector2 createRotationVector(float angle)
        {
            float angle_rad = MathHelper.DegreesToRadians(angle);
            return new Vector2((float)Math.Sin(angle_rad), (float)-Math.Cos(angle_rad));
        }

        public static float[] createRotationFloats(float angle)
        {
            float angle_rad = MathHelper.DegreesToRadians(angle);
            return new float[] {
                (float)Math.Sin(angle_rad),
                (float)-Math.Cos(angle_rad)
            };
        }

        public static float[] createArray(Vector4 vector)
        {
            return new float[]
            {
                vector.X,
                vector.Y,
                vector.Z,
                vector.W
            };
        }

        public static float[] createArray(Matrix4 matrix)
        {
            List<float> temp_floats = new List<float>();

            temp_floats.AddRange(createArray(matrix.Column0));
            temp_floats.AddRange(createArray(matrix.Column1));
            temp_floats.AddRange(createArray(matrix.Column2));
            temp_floats.AddRange(createArray(matrix.Column3));

            return temp_floats.ToArray();
        }

        public static float[] createArray(Matrix4[] matrices)
        {
            List<float> temp_floats = new List<float>();

            foreach (Matrix4 m in matrices)
            {
                temp_floats.AddRange(createArray(m));
            }

            return temp_floats.ToArray();
        }


        public static Matrix4 createMatrix(float[] matrix_values)
        {
            if (matrix_values.Length != 16)
            {
                throw new Exception("createMatrix(float[] matrix_values) - matrix_values must have length of 16");
            }

            Matrix4 temp_matrix = new Matrix4(
                matrix_values[0], matrix_values[4], matrix_values[8], matrix_values[12],
                matrix_values[1], matrix_values[5], matrix_values[9], matrix_values[13],
                matrix_values[2], matrix_values[6], matrix_values[10], matrix_values[14],
                matrix_values[3], matrix_values[7], matrix_values[11], matrix_values[15]
            );

            return temp_matrix;
        }

        public static Matrix4 createMatrix(Vector3 translation, Vector3 rotation_euler, Vector3 scale)
        {
            // Scale
            Matrix4 temp_scale = Matrix4.CreateScale(scale);

            // Rotation
            Matrix4 temp_rotation = rotate(rotation_euler.X, rotation_euler.Y, rotation_euler.Z);

            // Translation
            Matrix4 temp_translation = Matrix4.CreateTranslation(translation);


            // Build full tranformation matrix
            return temp_scale * (temp_rotation * temp_translation);
        }

        public static Matrix4 createMatrix(Vector3 translation, Quaternion rotation, Vector3 scale)
        {
            // Scale
            Matrix4 temp_scale = Matrix4.CreateScale(scale);

            // Rotation
            Matrix4 temp_rotation = Matrix4.CreateFromQuaternion(rotation);

            // Translation
            Matrix4 temp_translation = Matrix4.CreateTranslation(translation);


            // Build full tranformation matrix
            return temp_scale * (temp_rotation * temp_translation);
        }

        public static Quaternion createQuaternion(float x_angle, float y_angle, float z_angle)
        {
            Quaternion x_rotation = Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(x_angle));
            Quaternion y_rotation = Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(y_angle));
            Quaternion z_rotation = Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(z_angle));

            Quaternion zyx_rotation = Quaternion.Multiply(Quaternion.Multiply(z_rotation, y_rotation), x_rotation);

            zyx_rotation.Normalize();
            return zyx_rotation;
        }

        public static Matrix4 createRotationMatrix(float x_angle, float y_angle, float z_angle)
        {
            return Matrix4.CreateFromQuaternion(createQuaternion(x_angle, y_angle, z_angle));
        }

        public static Matrix4 createRotationMatrix(Vector3 angles)
        {
            return Matrix4.CreateFromQuaternion(createQuaternion(angles.X, angles.Y, angles.Z));
        }

        //------------------------------------------------------
        // Data Type Converters
        //------------------------------------------------------

        public static Matrix4 bullet2neira(BulletSharp.Math.Matrix matrix)
        {
            return new Matrix4(
                matrix.M11, matrix.M12, matrix.M13, matrix.M14,
                matrix.M21, matrix.M22, matrix.M23, matrix.M24,
                matrix.M31, matrix.M32, matrix.M33, matrix.M34,
                matrix.M41, matrix.M42, matrix.M43, matrix.M44
            );
        }

        public static Vector3 bullet2neira(BulletSharp.Math.Vector3 vector)
        {
            return new Vector3(vector.X, vector.Y, vector.Z);
        }



        //------------------------------------------------------
        // World Object Helpers
        //------------------------------------------------------
        // Rotation Matrix to convert Z-up to Y-up
        public static Matrix4 yup = Matrix4.CreateRotationX((float)(-90.0f * Math.PI / 180.0f));

        public static Matrix4 blender2Neira(Vector3 translation, Vector3 rotation_euler, Vector3 scale)
        {
            // Build full tranformation matrix
            Matrix4 temp_matrix = createMatrix(translation, rotation_euler, scale);

            // Blender defaults to Z-up. Need to convert to Y-up.
            return temp_matrix * yup;
        }

        public static Matrix4 blender2Neira(Matrix4 transformation)
        {
            // Blender defaults to Z-up. Need to convert to Y-up.
            return transformation * yup;
        }



    }
}

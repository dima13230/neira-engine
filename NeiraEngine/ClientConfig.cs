using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using NeiraEngine.Render;
using NeiraEngine.Output;

namespace NeiraEngine
{
    public static class ClientConfig
    {
        public static VSyncMode VSync { get; set; }

        public static string title { get; set; }

        public static string startupLevel { get; set; }

        public static int gl_major_version { get; set; }
        public static int gl_minor_version { get; set; }

        private static int _glsl_version;
        public static int glsl_version
        {
            get
            {
                return int.Parse(gl_major_version + "" + gl_minor_version + "0");
            }
            set { _glsl_version = value; }
        }

        public static string gl_version_string
        {
            get
            {
                return gl_major_version + "." + gl_minor_version;
            }
        }

        //------------------------------------------------------
        // Display
        //------------------------------------------------------

        public static Resolution default_resolution { get; private set; }

        public static bool default_fullscreen { get; private set; }

        public static float fps_target { get; set; }
        public static float fov { get; set; }
        public static float fov_radian
        {
            get
            {
                return MathHelper.DegreesToRadians(fov);
            }
            set
            {
                fov = MathHelper.RadiansToDegrees(value);
            }
        }

        public static Vector2 near_far { get; set; }

        public static Vector2 near_far_projection
        {
            get
            {
                return new Vector2(
                        near_far.Y / (near_far.Y - near_far.X),
                        (near_far.Y * near_far.X) / (near_far.Y - near_far.X)
                    );
            }
        }

        public static Vector4 near_far_full
        {
            get
            {
                return new Vector4(
                        near_far.X,
                        near_far.Y,
                        near_far_projection.X,
                        near_far_projection.Y
                    );
            }
        }

        public static float smooth_mouse_delay { get; set; }

        public static float smooth_keyboard_delay { get; set; }

        public static float default_movement_speed_walk { get; set; }

        public static float default_movement_speed_run { get; set; }

        public static float default_look_sensitivity { get; set; }

        public static void Init(
            string Title,
            string StartupLevel,
            int Gl_major_version, int Gl_minor_version, 
            float target_fps, 
            float Fov, float Near_plane, float Far_plane,
            float Smooth_mouse_delay, float Smooth_keyboard_delay,
            int width, int height, bool fullscreen,
            float movement_speed_walk, float movement_speed_run, float look_sensitivity)
        {
            title = Title;
            startupLevel = StartupLevel;

            gl_major_version = Gl_major_version;
            gl_minor_version = Gl_minor_version;

            fps_target = target_fps;

            fov = Fov;
            near_far = new Vector2(Near_plane, Far_plane);

            smooth_mouse_delay = Smooth_mouse_delay;
            smooth_keyboard_delay = Smooth_keyboard_delay;

            default_resolution = new Resolution(width, height);
            default_fullscreen = fullscreen;


            default_movement_speed_walk = movement_speed_walk;
            default_movement_speed_run = movement_speed_run;
            default_look_sensitivity = look_sensitivity;
        }
    }
}

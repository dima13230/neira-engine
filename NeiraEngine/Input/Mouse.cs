using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Input;
using MB = OpenTK.Input.MouseButton;

using NeiraEngine.World.Role;

namespace NeiraEngine.Input
{
    public static class Mouse
    {
        static bool anyKey, lastAnyKey;

        public delegate void MouseButtonEventHandler(MouseButtonEventArgs state);
        public static event MouseButtonEventHandler ButtonDown;
        public static event MouseButtonEventHandler ButtonUp;
        public static event MouseButtonEventHandler ButtonPress;

        static MouseState mouseState, lastMouseState;
        static MouseState mouseCursorState, lastMouseCursorState;

        public static float sensitivity { get; set; }

        public static bool locked { get; set; }

        public static Dictionary<Enum, bool> buttons { get; set; }

        public static Point position_current { get; set; }
        public static Point position_previous { get; set; }

        public static float wheel { get { return mouseState.WheelPrecise; } }

        public static Vector3 position_delta
        {
            get
            {
                return new Vector3(
                    (-position_current.Y + position_previous.Y) * sensitivity,
                    (-position_current.X + position_previous.X) * sensitivity,
                    0.0f
                );
            }
        }
    

        internal static void Init(float Sensitivity, bool Locked)
        {
            sensitivity = Sensitivity;
            locked = Locked;
            buttons = new Dictionary<Enum, bool>();
            Hide();
        }

        public static bool IsKeyUp(MouseButton key)
        {
            mouseState = OpenTK.Input.Mouse.GetState();

            if (IsButtonUnpress(key))
                return true;

            lastMouseState = mouseState;
            return false;
        }

        public static bool IsKeyDown(MouseButton key)
        {
            mouseState = OpenTK.Input.Mouse.GetState();

            if (IsButtonPress(key))
                return true;

            lastMouseState = mouseState;
            return false;
        }

        public static bool IsButtonPress(MouseButton key)
        {
            return mouseState[(MB)key] && (mouseState[(MB)key] != lastMouseState[(MB)key]);
        }

        public static bool IsButtonUnpress(MouseButton key)
        {
            return !mouseState[(MB)key] && (mouseState[(MB)key] != lastMouseState[(MB)key]);
        }

        internal static void UpdateEvents()
        {
            mouseState = OpenTK.Input.Mouse.GetState();
            mouseCursorState = OpenTK.Input.Mouse.GetCursorState();

            foreach (MouseButton key in (MouseButton[])Enum.GetValues(typeof(MouseButton)))
            {
                ButtonPress(new MouseButtonEventArgs(mouseCursorState.X, mouseCursorState.Y, key, true));
                if (IsButtonPress(key))
                    ButtonDown(new MouseButtonEventArgs(mouseCursorState.X, mouseCursorState.Y, key, true));

            }

            foreach (MouseButton key in (MouseButton[])Enum.GetValues(typeof(MouseButton)))
            {
                if (KeyUpSinceLast(key))
                {
                    ButtonUp(new MouseButtonEventArgs(mouseCursorState.X, mouseCursorState.Y, key, false));
                }
            }
            lastMouseState = mouseState;
            lastMouseCursorState = mouseCursorState;
            lastAnyKey = anyKey;
        }

        static bool KeyUpSinceLast(MouseButton key)
        {
            if (lastMouseState.IsButtonDown((MB)key) && mouseState.IsButtonUp((MB)key))
                return true;
            return false;
        }

        public static void Hide()
        {
            if (locked)
            {
                System.Windows.Forms.Cursor.Hide();
            }
            else
            {
                System.Windows.Forms.Cursor.Show();
            }
        }

        public static void toggleLock()
        {
            locked = !locked;
            Hide();
        }

    }
}

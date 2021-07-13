using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Input;

using NeiraEngine.World.View;
using NeiraEngine.World.Role;

namespace NeiraEngine.Input
{
    public static class Keyboard
    {
        static KeyboardState keyboardState, lastKeyboardState;

        static bool anyKey, lastAnyKey;

        public delegate void KeyboardEventHandler(KeyboardKeyEventArgs state);
        public static event KeyboardEventHandler KeyDown;
        public static event KeyboardEventHandler KeyUp;
        public static event KeyboardEventHandler KeyPress;

        public static bool IsKeyUp(Key key)
        {
            keyboardState = OpenTK.Input.Keyboard.GetState();

            if (IsKeyUnpress(key))
                return true;

            lastKeyboardState = keyboardState;
            return false;
        }

        public static bool IsKeyDown(Key key)
        {
            keyboardState = OpenTK.Input.Keyboard.GetState();

            if (IsKeyPress(key))
                return true;

            lastKeyboardState = keyboardState;
            return false;
        }

        static bool IsKeyPress(Key key)
        {
            return keyboardState[(OpenTK.Input.Key)key] && (keyboardState[(OpenTK.Input.Key)key] != lastKeyboardState[(OpenTK.Input.Key)key]);
        }

        static bool IsKeyUnpress(Key key)
        {
            return !keyboardState[(OpenTK.Input.Key)key] && (keyboardState[(OpenTK.Input.Key)key] != lastKeyboardState[(OpenTK.Input.Key)key]);
        }

        internal static void UpdateEvents()
        {
            keyboardState = OpenTK.Input.Keyboard.GetState();
            anyKey = keyboardState.IsAnyKeyDown;

            foreach (Key key in (Key[])Enum.GetValues(typeof(Key)))
            {
                if (keyboardState[(OpenTK.Input.Key)key])
                {
                    KeyPress(new KeyboardKeyEventArgs(key));
                    if (anyKey != lastAnyKey)
                        KeyDown(new KeyboardKeyEventArgs(key));
                }
            }

            foreach (Key key in (Key[])Enum.GetValues(typeof(Key)))
            {
                if(KeyUpSinceLast(key))
                    KeyUp(new KeyboardKeyEventArgs(key));
            }

            lastKeyboardState = keyboardState;
            lastAnyKey = anyKey;
        }

        static bool KeyUpSinceLast(Key key)
        {
            if (lastKeyboardState.IsKeyDown((OpenTK.Input.Key)key) && keyboardState.IsKeyUp((OpenTK.Input.Key)key))
                return true;
            return false;
        }

        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
        public static void turnOffCapLock()
        {
            if (Control.IsKeyLocked(Keys.CapsLock))
            {
                const int KEYEVENTF_EXTENDEDKEY = 0x1;
                const int KEYEVENTF_KEYUP = 0x2;

                keybd_event(0x14, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
                keybd_event(0x14, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, (UIntPtr)0);
            }
        }

    }
}

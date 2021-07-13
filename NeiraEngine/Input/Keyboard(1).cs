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
    public class Keyboard : InputDevice
    {

        public bool repeat { get; set; }
        public Dictionary<Enum, bool> keys { get; set; }


        public Keyboard()
            : this(false)
        { }

        public Keyboard(bool key_repeat)
        {
            repeat = key_repeat;
            keys = new Dictionary<Enum, bool>();
        }


        public void keyUp(KeyboardKeyEventArgs e)
        {
            keys[e.Key] = false;
        }


        public void keyDown(KeyboardKeyEventArgs e)
        {
            keys[e.Key] = true;

            switch (e.Key)
            {
                
            }
        }


        public bool getKeyPress(Key key)
        {
            return getInput(key, keys);
        }


        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
        public void turnOffCapLock()
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

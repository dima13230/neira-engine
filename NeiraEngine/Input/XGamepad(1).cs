using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NeiraEngine;

using SharpDX.XInput;

namespace NeiraEngine.Input
{
    public class XGamepad : InputDevice
    {
        Controller controller;

        State state
        {
            get
            {
                if (active)
                    return controller.GetState();
                else
                    return new State();
            }
        }

        public bool active
        {
            get
            {
                return controller != null && controller.IsConnected;
            }
        }


        public Vector2 LeftThumb;


        public XGamepad(UserIndex userIndex)
        {
            controller = new Controller(userIndex);
        }

        public float getLeftThumbX()
        {
            if (active)
            {
                return state.Gamepad.LeftThumbX;
            }
            else
                return 0;
        }

        public float getLeftThumbY()
        {
            if (active)
            {
                return state.Gamepad.LeftThumbY;
            }
            else
                return 0;
        }

        public float getRightThumbX()
        {
            if (active)
            {
                return state.Gamepad.RightThumbX;
            }
            else
                return 0;
        }

        public float getRightThumbY()
        {
            if (active)
            {
                return state.Gamepad.RightThumbY;
            }
            else
                return 0;
        }

        public int getLeftTrigger()
        {
            if (active)
            {
                return state.Gamepad.LeftTrigger;
            }
            else
                return 0;
        }

        
    }
}

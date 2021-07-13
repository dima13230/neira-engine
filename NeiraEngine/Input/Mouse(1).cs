using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Input;

using NeiraEngine.World.Role;

namespace NeiraEngine.Input
{
    public class Mouse : InputDevice
    {

        private float _sensitivity;
        public float sensitivity
        {
            get { return _sensitivity; }
            set { _sensitivity = value; }
        }


        private bool _locked;
        public bool locked
        {
            get { return _locked; }
            set { _locked = value; }
        }


        private Dictionary<Enum, bool> _buttons;
        public Dictionary<Enum, bool> buttons
        {
            get { return _buttons; }
            set { _buttons = value; }
        }
        public Point position_current { get; set; }
        public Point position_previous { get; set; }

        public Vector3 position_delta
        {
            get
            {
                return new Vector3(
                    (-position_current.Y + position_previous.Y) * _sensitivity,
                    (-position_current.X + position_previous.X) * _sensitivity,
                    0.0f
                );
            }
        }
    

        public Mouse(float sensitivity, bool locked)
        {
            _sensitivity = sensitivity;
            _locked = locked;
            _buttons = new Dictionary<Enum, bool>();
            hide();
        }


        public bool getButtonPress(MouseButton mouse_button)
        {
            return getInput(mouse_button, _buttons);
        }

        public void buttonUp(MouseButtonEventArgs e)
        {
            _buttons[e.Button] = false;
        }

        public void buttonDown(MouseButtonEventArgs e)
        {
            _buttons[e.Button] = true;
        }

        public void wheel(MouseWheelEventArgs e)
        {
            
        }


        public void hide()
        {
            if (_locked)
            {
                System.Windows.Forms.Cursor.Hide();
            }
            else
            {
                System.Windows.Forms.Cursor.Show();
            }
        }

        public void toggleLock()
        {
            _locked = !_locked;
            hide();
        }

    }
}

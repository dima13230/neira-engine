using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

using Cgen.Audio;

using NeiraEngine;
using NeiraEngine.Client;
using NeiraEngine.Components;
using NeiraEngine.World;

namespace NeiraEngine.Run
{
    public class GameLogic : EngineDriver
    {
        Thread splashThread;
        SplashScreen splash;
        public GameLogic(Client.Client _game) : base(_game)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            splashThread = new Thread(makeSplash);
            splashThread.Start();
        }

        void makeSplash()
        {
            Application.Run(new SplashScreen());
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            splashThread.Abort();
        }

        //------------------------------------------------------
        // Helpers
        //------------------------------------------------------
        private Vector3 getWorldSpaceRay()
        {
            float x = 2.0f * client.mouse.position_current.X / client.display.resolution.W - 1.0f;
            float y = 1.0f - (2.0f * client.mouse.position_current.Y) / client.display.resolution.H;
            float z = 1.0f;
            Vector3 mouse_ray = new Vector3(x, y, z);
            Vector4 ray_clip = new Vector4(mouse_ray.X, mouse_ray.Y, -1.0f, 1.0f);
            Vector4 ray_eye = Vector4.Transform(ray_clip, Matrix4.Invert(client.player.camera.spatial.perspective));
            ray_eye = new Vector4(ray_eye.X, ray_eye.Y, -1.0f, 0.0f);

            return Vector3.Normalize(Vector4.Transform(ray_eye, Matrix4.Invert(client.player.camera.spatial.model_view)).Xyz);
        }

        private Vector3[] getPickingVectors()
        {
            Vector3 ray_world = getWorldSpaceRay();
            Vector3 start = -client.player.camera.spatial.position;

            ray_world = Vector3.TransformPosition(ray_world * client.config.near_far.Y, Matrix4.CreateTranslation(start));

            return new Vector3[]{
                start,
                ray_world
            };
        }

        //------------------------------------------------------
        // Input
        //------------------------------------------------------

        private void centerMouse()
        {
            if (Focused)
            {
                if (client.mouse.locked)
                {
                    Point windowCenter = new Point(
                    (Width) / 2,
                    (Height) / 2);

                    System.Windows.Forms.Cursor.Position = PointToScreen(windowCenter);
                }
                client.mouse.position_previous = PointToClient(System.Windows.Forms.Cursor.Position);
            }
        }

        protected override void KeyboardKeyDown(KeyboardKeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.F:
                    client.scene.toggleFlashlight(client.player.toggleFlashlight());
                    break;
                case Key.O:
                    Thread.Sleep(2000);
                    break;
                case Key.P:
                    _physics_driver.pause();
                    client.scene.pauseAnimation();
                    break;
                case Key.R:
                    _physics_driver.reset();
                    client.scene.resetAnimation();
                    break;
                case Key.X:
                    _physics_driver.throwObject(EngineHelper.otk2bullet(getWorldSpaceRay()));
                    break;
                case Key.Z:
                    _physics_driver.zoomPickedObject();
                    break;
                case Key.Tab:
                    client.player.togglePhysical();
                    break;
                case Key.CapsLock:
                    client.mouse.toggleLock();
                    if (client.mouse.locked)
                    {
                        centerMouse();
                    }
                    break;
                case Key.F6:
                    _debug_window.toggleTimers();
                    break;
                case Key.F7:
                    _render_driver.toggleDebugViews();
                    break;
                case Key.F8:
                    _render_driver.toggleWireframe();
                    break;
                case Key.F12:
                    _render_driver.triggerScreenshot();
                    break;
                case Key.Number7:
                case Key.Keypad7:
                    _render_driver.toggleEffect(typeof(Render.FX.fx_VXGI));
                    break;
                case Key.Escape:
                    Exit();
                    break;
            }
        }

        protected override void KeyboardBuffer()
        {
            //------------------------------------------------------
            // Smooth Camera Movement
            //------------------------------------------------------
            client.player.smoothMovement(client.config.smooth_keyboard_delay);


            //------------------------------------------------------
            // Player Movement
            //------------------------------------------------------

            // Running
            client.player.run(client.keyboard.getKeyPress(Key.ShiftLeft));

            // Sprinting
            client.player.sprint(client.keyboard.getKeyPress(Key.AltLeft));


            if (client.keyboard.getKeyPress(Key.W))
            {
                //Console.WriteLine("Forward");
                client.player.moveForeward();
            }

            if (client.keyboard.getKeyPress(Key.S))
            {
                //Console.WriteLine("Backward");
                client.player.moveBackward();
            }

            if (client.keyboard.getKeyPress(Key.A))
            {
                //Console.WriteLine("Left");
                client.player.strafeLeft();
            }

            if (client.keyboard.getKeyPress(Key.D))
            {
                //Console.WriteLine("Right");
                client.player.strafeRight();
            }

            if (client.keyboard.getKeyPress(Key.Space))
            {
                //Console.WriteLine("Jump");
                client.player.moveUp();
            }

            if (client.keyboard.getKeyPress(Key.ControlLeft))
            {
                //Console.WriteLine("Crouch");
                client.player.moveDown();
            }

            // Update Physics Character Position
            client.player.updatePhysicalPosition();
        }

        protected override void MouseButtonUp(MouseButtonEventArgs e)
        {
            base.MouseButtonUp(e);
            switch (e.Button)
            {
                case MouseButton.Left:
                    _physics_driver.releaseObject();
                    break;
            }
        }

        protected override void MouseButtonDown(MouseButtonEventArgs e)
        {
            base.MouseButtonDown(e);
            switch (e.Button)
            {
                case MouseButton.Left:
                    Vector3[] picking_vectors = getPickingVectors();
                    _physics_driver.pickObject(EngineHelper.otk2bullet(picking_vectors[0]), EngineHelper.otk2bullet(picking_vectors[1]), !client.keyboard.getKeyPress(Key.AltLeft));
                    break;
                case MouseButton.Middle:
                    _render_driver.triggerScreenshot();
                    break;
            }
        }

        protected override void MouseScroll(MouseWheelEventArgs e)
        {
            client.scene.circadian_timer.time += e.Delta / 10.0f;
        }

        protected override void MouseButtonBuffer()
        {
            // Zoom camera
            client.player.camera.zoom(client.mouse.getButtonPress(MouseButton.Right), _fps);
        }

        protected override void MouseMoveBuffer()
        {
            base.MouseMoveBuffer();
            // Don't move game if we are using the tweak bar or window isn't focused
            if (!_debug_window.handle_MouseMove(client.mouse.position_current) && Focused)
            {
                // Rotate main character from mouse movement
                client.player.rotate(
                    client.mouse.position_delta,
                    client.config.smooth_mouse_delay
                );

                // Move Picked Object
                Vector3[] picking_vectors = getPickingVectors();
                _physics_driver.moveObject(EngineHelper.otk2bullet(picking_vectors[0]), EngineHelper.otk2bullet(picking_vectors[1]));
            }


            // Recenter
            centerMouse();
        }

    }
}

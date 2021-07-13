using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;

using Cgen.Audio;

using NeiraEngine;
using NeiraEngine.Input;
using NeiraEngine.Physics;
using NeiraEngine.Render;
using NeiraEngine.Components;
using NeiraEngine.World;

namespace NeiraEngine.Run
{
    public class GameLogic : EngineDriver
    {
        Thread splashThread;
        SplashScreen splash;
        public GameLogic() : base()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            splashThread = new Thread(makeSplash);
            splashThread.Start();
            update += Update;
        }

        void makeSplash()
        {
            //Application.Run(new SplashScreen());
        }

        public override void OnLoad()
        {
            base.OnLoad();
            splashThread.Abort();
        }

        //------------------------------------------------------
        // Helpers
        //------------------------------------------------------
        private Vector3 getWorldSpaceRay()
        {
            float x = 2.0f * Mouse.position_current.X / Client.display.resolution.W - 1.0f;
            float y = 1.0f - (2.0f * Mouse.position_current.Y) / Client.display.resolution.H;
            float z = 1.0f;
            Vector3 mouse_ray = new Vector3(x, y, z);
            Vector4 ray_clip = new Vector4(mouse_ray.X, mouse_ray.Y, -1.0f, 1.0f);
            Vector4 ray_eye = Vector4.Transform(ray_clip, Matrix4.Invert(Client.player.camera.spatial.perspective));
            ray_eye = new Vector4(ray_eye.X, ray_eye.Y, -1.0f, 0.0f);

            return Vector3.Normalize(Vector4.Transform(ray_eye, Matrix4.Invert(Client.player.camera.spatial.model_view)).Xyz);
        }

        private Vector3[] getPickingVectors()
        {
            Vector3 ray_world = getWorldSpaceRay();
            Vector3 start = -Client.player.camera.spatial.position;

            ray_world = Vector3.TransformPosition(ray_world * ClientConfig.near_far.Y, Matrix4.CreateTranslation(start));

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
                if (Mouse.locked)
                {
                    Point windowCenter = new Point(
                    (Bounds.W) / 2,
                    (Bounds.H) / 2);

                    //Cursor.Position = PointToScreen(windowCenter);
                }
                //Mouse.position_previous = PointToClient(Cursor.Position);
            }
        }

        protected override void KeyboardKeyDown(KeyboardKeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.F:
                    Client.scene.toggleFlashlight(Client.player.toggleFlashlight());
                    break;
                case Key.O:
                    Thread.Sleep(2000);
                    break;
                case Key.P:
                    PhysicsDriver.Pause();
                    Client.scene.pauseAnimation();
                    break;
                case Key.R:
                    PhysicsDriver.Reset();
                    Client.scene.resetAnimation();
                    break;
                case Key.X:
                    PhysicsDriver.throwObject(getWorldSpaceRay());
                    break;
                case Key.Z:
                    PhysicsDriver.zoomPickedObject();
                    break;
                case Key.Tab:
                    Client.player.togglePhysical();
                    break;
                case Key.CapsLock:
                    Mouse.toggleLock();
                    if (Mouse.locked)
                    {
                        centerMouse();
                    }
                    break;
                case Key.F6:
                    debug_window.toggleTimers();
                    break;
                case Key.F7:
                    //RenderDriver.toggleDebugViews();
                    break;
                case Key.F8:
                    //RenderDriver.toggleWireframe();
                    break;
                case Key.F12:
                    //_render_driver.triggerScreenshot();
                    break;
                case Key.Escape:
                    //Exit();
                    break;
            }
        }

        protected void KeyboardBuffer()
        {
            
        }

        protected override void MouseButtonUp(MouseButtonEventArgs e)
        {
            base.MouseButtonUp(e);
            switch (e.Button)
            {
                case MouseButton.Left:
                    PhysicsDriver.releaseObject();
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
                    PhysicsDriver.pickObject(picking_vectors[0], picking_vectors[1], !Keyboard.IsKeyDown(Key.AltLeft));
                    break;
            }
        }

        protected override void MouseScroll(MouseWheelEventArgs e)
        {
            Client.scene.circadian_timer.time += e.Delta / 10.0f;
        }

        protected override void MouseButtonBuffer()
        {
            // Zoom camera
            Client.player.camera.zoom(Mouse.IsButtonPress(MouseButton.Right), RenderDriver.fps);
        }

        protected override void MouseMoveBuffer()
        {
            base.MouseMoveBuffer();
            // Don't move game if we are using the tweak bar or window isn't focused
            if (!debug_window.handle_MouseMove(Mouse.position_current) && Focused)
            {
                // Rotate main character from mouse movement
                Client.player.rotate(
                    Mouse.position_delta,
                    ClientConfig.smooth_mouse_delay
                );

                // Move Picked Object
                Vector3[] picking_vectors = getPickingVectors();
                PhysicsDriver.moveObject(picking_vectors[0], picking_vectors[1]);
            }


            // Recenter
            centerMouse();
        }

        public void Update(object sender, FrameEventArgs e)
        {
            //------------------------------------------------------
            // Smooth Camera Movement
            //------------------------------------------------------
            Client.player.smoothMovement(ClientConfig.smooth_keyboard_delay);


            //------------------------------------------------------
            // Player Movement
            //------------------------------------------------------

            // Running
            Client.player.run(Keyboard.IsKeyDown(Key.ShiftLeft));

            // Sprinting
            Client.player.sprint(Keyboard.IsKeyDown(Key.AltLeft));


            if (Keyboard.IsKeyDown(Key.W))
            {
                //Console.WriteLine("Forward");
                Client.player.moveForeward();
            }

            if (Keyboard.IsKeyDown(Key.S))
            {
                //Console.WriteLine("Backward");
                Client.player.moveBackward();
            }

            if (Keyboard.IsKeyDown(Key.A))
            {
                //Console.WriteLine("Left");
                Client.player.strafeLeft();
            }

            if (Keyboard.IsKeyDown(Key.D))
            {
                //Console.WriteLine("Right");
                Client.player.strafeRight();
            }

            if (Keyboard.IsKeyDown(Key.Space))
            {
                //Console.WriteLine("Jump");
                Client.player.moveUp();
            }

            if (Keyboard.IsKeyDown(Key.ControlLeft))
            {
                //Console.WriteLine("Crouch");
                Client.player.moveDown();
            }

            // Update Physics Character Position
            Client.player.updatePhysicalPosition();
        }

    }
}

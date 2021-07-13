using System;
using System.Drawing;
using System.Threading;
using System.Runtime.InteropServices;

using System.Reflection;

using OpenTK;
//using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using SharpDX.XInput;

using Cgen.Audio;

using NeiraEngine;
using NeiraEngine.Input;
using NeiraEngine.Output;
using NeiraEngine.Render;
using NeiraEngine.World;
using NeiraEngine.Physics;

using Keyboard = NeiraEngine.Input.Keyboard;
using Mouse = NeiraEngine.Input.Mouse;

namespace NeiraEngine
{
    public class EngineDriver: IDisposable
    {

        public static EngineDriver current;

        // Engine Objects
        private static bool sleeping;

        public static bool Focused;

        public static Resolution Bounds;

        public event EventHandler<FrameEventArgs> update;

        // Debug Objects
        protected static DebugWindow debug_window;

        public EngineDriver()
        {
            Client.Load();
            current = this;
            sleeping = false;

            // Display Settings
            ClientConfig.VSync = Render.VSyncMode.On;

            // Register Input Devices
            Keyboard.KeyUp += keyboard_KeyUp;
            Keyboard.KeyDown += keyboard_KeyDown;
            Keyboard.KeyPress += keyboard_KeyPress;

            Mouse.ButtonUp += mouse_ButtonUp;
            Mouse.ButtonDown += mouse_ButtonDown;
            //Mouse.WheelChanged += mouse_Wheel;

            //Keyboard.KeyRepeat = client.keyboard.repeat;
            update += UpdateFrame;
        }


        // Event Handling
        public void GlGameWindow_UpdateFrame(object sender, OpenTK.FrameEventArgs e)
        {
            update(sender, new FrameEventArgs(e.Time));
        }

        //------------------------------------------------------
        // Input Handling
        //------------------------------------------------------

        // Process input per frame
        private void inputBuffer()
        {
            mouse_ButtonBuffer();
            mouse_MoveBuffer();
        }

        // Keyboard

        protected void keyboard_KeyPress(KeyboardKeyEventArgs state)
        {
            debug_window.handle_Keyboard(state);
        }

        protected void keyboard_KeyUp(KeyboardKeyEventArgs state)
        {
            KeyboardKeyUp(state);
        }

        protected void keyboard_KeyDown(KeyboardKeyEventArgs state)
        {
            KeyboardKeyDown(state);
        }

        protected virtual void KeyboardKeyDown(KeyboardKeyEventArgs e) { }
        protected virtual void KeyboardKeyUp(KeyboardKeyEventArgs e) { }

        // Mouse

        protected void mouse_ButtonUp(MouseButtonEventArgs e)
        {
            if(debug_window.handle_MouseClick(e))
            {
                return;
            }

            MouseButtonUp(e);
        }

        protected void mouse_ButtonDown(MouseButtonEventArgs e)
        {
            if (debug_window.handle_MouseClick(e))
            {
                return;
            }

            MouseButtonDown(e);
        }

        protected void mouse_Wheel(MouseWheelEventArgs e)
        {
            if (debug_window.handle_MouseWheel(e))
            {
                return;
            }

            MouseScroll(e);

        }

        private void mouse_ButtonBuffer()
        {
            MouseButtonBuffer();
        }

        private void mouse_MoveBuffer()
        {
            // Calculate mouse current position
            Mouse.position_current = (Mouse.locked || Mouse.IsButtonPress(MouseButton.Left)) ? Mouse.position_current : Mouse.position_previous;

            MouseMoveBuffer();
        }

        protected virtual void MouseButtonUp(MouseButtonEventArgs e) { }
        protected virtual void MouseButtonDown(MouseButtonEventArgs e) { }
        protected virtual void MouseScroll(MouseWheelEventArgs e) { }
        protected virtual void MouseButtonBuffer() { }
        protected virtual void MouseMoveBuffer() { }

        //------------------------------------------------------
        // Game Loop Handling
        //------------------------------------------------------

        public virtual void OnLoad()
        {
            // Create Engine Objects
            RenderDriver.Init(Client.display.resolution);

            debug_window = new DebugWindow();
            PhysicsDriver.Load();

            // Load Sound System
            SoundSystem.Instance.Initialize();

            // Load Objects
            Client.Init();

            debug_window.load();

            Scene.world_loader.loadLevel(ClientConfig.startupLevel, Client.scene);

            foreach (WorldObject worldObject in Client.scene.worldObjects)
                worldObject.Start(Client.scene.staticMode);

            Render.Forms.FormHandler.Run();
        }

        internal void UpdateFrame(object sender, FrameEventArgs e)
        {
            PhysicsDriver.Update((float)e.Time, ClientConfig.fps_target, RenderDriver.fps);

            SoundSystem.Instance.Update(e.Time, Client.player.character.spatial.position, Client.player.character.spatial.look, Client.player.character.spatial.up);

            foreach (WorldObject obj in Client.scene.worldObjects)
                obj.Update(Client.scene.staticMode);

        }

        protected void OnUnload(EventArgs e)
        {
            Client.Unload();
            debug_window.unload();
            PhysicsDriver.Unload();

            Debug.logInfo(0, "\n\nExiting...", "\n\n");
            Thread.Sleep(500);
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}

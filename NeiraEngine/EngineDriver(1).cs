using System;
using System.Drawing;
using System.Threading;
using System.Runtime.InteropServices;

using System.Reflection;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

using SharpDX.XInput;

using Cgen.Audio;

using NeiraEngine.Output;
using NeiraEngine.Render;
using NeiraEngine.World;
using NeiraEngine.Physics;

namespace NeiraEngine
{
    public class EngineDriver : GameWindow
    {
        public static EngineDriver currentDriver;

        // Engine Objects
        public float _fps;
        private bool _sleeping;

        // Physics Objects
        public PhysicsDriver _physics_driver;

        // Game Objects
        public Client client;

        // Debug Objects
        protected DebugWindow _debug_window;

        public EngineDriver(Client _game) :
            base(_game.display.resolution.W, _game.display.resolution.H,
                new GraphicsMode(new ColorFormat(64), 64, 64, 1),
                _game.title,
                _game.display.fullscreen ? GameWindowFlags.Fullscreen : GameWindowFlags.Default,
                DisplayDevice.Default,
                ClientConfig.gl_major_version, ClientConfig.gl_minor_version,
                GraphicsContextFlags.Default)
        {
            currentDriver = this;
            client = _game;

            _sleeping = false;

            // Display Settings
            VSync = VSyncMode.On;

            // Register Input Devices
            Keyboard.KeyUp += keyboard_KeyUp;
            Keyboard.KeyDown += keyboard_KeyDown;

            Mouse.ButtonUp += mouse_ButtonUp;
            Mouse.ButtonDown += mouse_ButtonDown;
            Mouse.WheelChanged += mouse_Wheel;

            Keyboard.KeyRepeat = client.keyboard.repeat;
        }

        //------------------------------------------------------
        // Input Handling
        //------------------------------------------------------

        // Process input per frame
        private void inputBuffer()
        {
            KeyboardBuffer();
            mouse_ButtonBuffer();
            mouse_MoveBuffer();
        }

        // Keyboard

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            _debug_window.handle_Keyboard(e);
        }

        protected void keyboard_KeyUp(object sender, KeyboardKeyEventArgs e)
        {
            client.keyboard.keyUp(e);
            KeyboardKeyUp(e);
        }

        protected void keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {

            client.keyboard.keyDown(e);
            KeyboardKeyDown(e);
        }

        protected virtual void KeyboardKeyDown(KeyboardKeyEventArgs e) { }
        protected virtual void KeyboardKeyUp(KeyboardKeyEventArgs e) { }
        protected virtual void KeyboardBuffer() { }

        // Mouse

        protected void mouse_ButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(_debug_window.handle_MouseClick(e))
            {
                return;
            }

            client.mouse.buttonUp(e);
            MouseButtonUp(e);
        }

        protected void mouse_ButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_debug_window.handle_MouseClick(e))
            {
                return;
            }

            client.mouse.buttonDown(e);
            MouseButtonDown(e);
        }

        protected void mouse_Wheel(object sender, MouseWheelEventArgs e)
        {
            if (_debug_window.handle_MouseWheel(e))
            {
                return;
            }

            client.mouse.wheel(e);

            MouseScroll(e);

        }

        private void mouse_ButtonBuffer()
        {
            MouseButtonBuffer();
        }

        private void mouse_MoveBuffer()
        {
            // Calculate mouse current position
            client.mouse.position_current = (client.mouse.locked || client.mouse.getButtonPress(MouseButton.Left)) ? PointToClient(System.Windows.Forms.Cursor.Position) : client.mouse.position_previous;

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

        protected override void OnResize(EventArgs e)
        {
            //_game.display.resolution.W = this.Width;
            //_game.display.resolution.H = this.Height;
            GL.Viewport(0, 0, Width, Height);
            _debug_window.resize(ClientSize);
        }

        protected override void OnLoad(EventArgs e)
        {
            Visible = false;
            // Create Engine Objects
            RenderDriver.Init(client.display.resolution);

            _debug_window = new DebugWindow();
            _physics_driver = new PhysicsDriver();

            // Load Sound System
            SoundSystem.Instance.Initialize();

            // Load Objects
            client.load(_physics_driver.physics_world);
            _debug_window.load();

            SoundSystem.Instance.Initialize();

            Scene.world_loader.loadLevel(ClientConfig.startupLevel, client.scene);

            foreach (WorldObject worldObject in client.scene.worldObjects)
                worldObject.Start(client.scene.staticMode);

            Visible = true;
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            _physics_driver.update((float)e.Time, ClientConfig.fps_target, _fps);

            foreach (WorldObject obj in client.scene.worldObjects)
                    obj.Update(client.scene.staticMode);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            _fps = (float)(1.0d / e.Time);

            inputBuffer();

            // Update Scene and its objects
            client.scene.update(client.player.camera.spatial);

            // Update Dynamic UBOs
            RenderDriver.updateUBO_Camera(
                client.player.camera.spatial.transformation,
                client.player.camera.spatial.perspective,
                Matrix4.Invert(client.player.camera.spatial.model_view.ClearTranslation() * client.player.camera.spatial.perspective),
                client.player.camera.previous_view_perspective,
                Matrix4.Invert(client.player.camera.previous_view_matrix.ClearTranslation() * client.player.camera.previous_perspective_matrix),
                client.player.camera.spatial.position,
                client.player.camera.spatial.look);
            _render_driver.handle_MouseState(client.mouse.locked);


            // Set camera's previous MVP matrix
            client.player.camera.previous_view_matrix = client.player.camera.spatial.model_view;
            client.player.camera.previous_perspective_matrix = client.player.camera.spatial.perspective;

            _render_driver.render(client.scene, client.player.camera.spatial, _fps);

            SoundSystem.Instance.Update(e.Time, client.player.character.spatial.position, client.player.character.spatial.look, client.player.character.spatial.up);


            //_debug_window.render(_fps);

            Debug.logGLError();
            SwapBuffers();

        }

        protected override void OnUnload(EventArgs e)
        {
            base.Dispose();

            client.unload();
            _debug_window.unload();
            _physics_driver.unload();

            Debug.logInfo(0, "\n\nExiting...", "\n\n");
            Thread.Sleep(500);
        }
    }
}

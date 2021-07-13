using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if WIND
using System.Windows.Forms;

namespace NeiraEngine.Render.Forms
{
    internal static class FormHandler
    {
        //static GLForm glf;
        //DXForm dxf;
        //VKForm vkf;

        static OpenTK.GameWindow glGameWindow;

        internal static void Init(RenderAPI api)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            switch (RenderDriver.recognizedAPI)
            {
                case RenderAPI.OpenGL:
                    glGameWindow = new OpenTK.GameWindow(ClientConfig.default_resolution.W, ClientConfig.default_resolution.H, OpenTK.Graphics.GraphicsMode.Default, ClientConfig.title, ClientConfig.default_fullscreen ? OpenTK.GameWindowFlags.Fullscreen : OpenTK.GameWindowFlags.Default, OpenTK.DisplayDevice.Default, ClientConfig.gl_major_version, ClientConfig.gl_minor_version, OpenTK.Graphics.GraphicsContextFlags.Default);
                    glGameWindow.UpdateFrame += EngineDriver.current.GlGameWindow_UpdateFrame;
                    glGameWindow.RenderFrame += RenderDriver.RenderGL;
                    EngineDriver.Focused = glGameWindow.Focused;
                    EngineDriver.Bounds = new Resolution(glGameWindow.Width, glGameWindow.Height);
                    break;
            }
        }

        internal static void Run()
        {
            switch (RenderDriver.recognizedAPI)
            {
                case RenderAPI.OpenGL:
                    glGameWindow.Run();
                    break;
            }
        }
    }
}
#endif
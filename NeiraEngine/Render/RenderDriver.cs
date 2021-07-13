using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cgen.Audio;

using System.Reflection;

using OpenTK;
//using OpenTK.Graphics.OpenGL;

using NeiraEngine.Components;
using NeiraEngine.Output;
using NeiraEngine.Render.OpenGL;
using NeiraEngine.World;

namespace NeiraEngine.Render
{
    public enum RenderAPI
    {
        OpenGL,
        DirectX,
        Vulkan
    };

    public static class RenderDriver
    {
        static dynamic typeObject;
        public static Resolution resolution;

        public static Assembly module;

        public static RenderAPI recognizedAPI;

        public static float fps;

        internal static void Init(Resolution Resolution)
        {
            resolution = Resolution;
            ProgramLoader.Init(ClientConfig.glsl_version);
            Console.WriteLine("Loading " + EngineHelper.path_render_modules + "NativeOpenGL.dll");
            module = Assembly.LoadFile(EngineHelper.path_render_modules + "NativeOpenGL.dll");

            Type mainType = module.GetType($"{module.GetName().Name}.Main", true);
            bool isModule = mainType.IsSubclassOf(typeof(RenderModuleDX)) || mainType.IsSubclassOf(typeof(RenderModuleGL)) || mainType.IsSubclassOf(typeof(RenderModuleVK));

            if (!isModule) throw new Exception("Requested to load something unknown as render module! ( Or it wasn't specified if it's module of OpenGL, DirectX, Vulkan? Specify it as RenderModuleGL, RenderModuleDX or RenderModuleVK )");

            if (mainType.IsSubclassOf(typeof(RenderModuleGL)))
            {
                recognizedAPI = RenderAPI.OpenGL;
            }

            Forms.FormHandler.Init(recognizedAPI);
            switch (recognizedAPI) {
                case RenderAPI.OpenGL:

                    LoadDefaultGL();

                    typeObject = Activator.CreateInstance(mainType, resolution);
                    typeObject.updateUBO_GameConfig(ClientConfig.near_far_full, ClientConfig.fps_target);
                    break;
            }
        }

        internal static void Update()
        {
        }

        private static void LoadDefaultGL()
        {
            // Default OpenGL Setup
            OGL.ClearColor(Color.Black);
            OGL.Enable(EnableCap.DepthTest);
            OGL.DepthMask(true);
            OGL.DepthFunc(DepthFunction.Lequal);
            OGL.DepthRange(0.0f, 1.0f);
            OGL.Enable(EnableCap.DepthClamp);

            OGL.Enable(EnableCap.CullFace);
            OGL.CullFace(CullFaceMode.Back);
            OGL.FrontFace(FrontFaceDirection.Ccw);

            OGL.Enable(EnableCap.TextureCubeMapSeamless);
        }

        internal static void RenderGL(object sender, OpenTK.FrameEventArgs e)
        {
            fps = (float)(1.0d / e.Time);

            // Update Scene and its objects
            Client.scene.Update(Client.player.camera.spatial);

            // Update Dynamic UBOs
            /*mainType.GetMethod("updateUBO_Camera").Invoke(null, new object[] {
                Client.player.camera.spatial.transformation,
                Client.player.camera.spatial.perspective,
                Matrix4.Invert(Client.player.camera.spatial.model_view.ClearTranslation() * Client.player.camera.spatial.perspective),
                Client.player.camera.previous_view_perspective,
                Matrix4.Invert(Client.player.camera.previous_view_matrix.ClearTranslation() * Client.player.camera.previous_perspective_matrix),
                Client.player.camera.spatial.position,
                Client.player.camera.spatial.look });*/

            Matrix4 spatcur = Matrix4.Invert(Client.player.camera.spatial.model_view.ClearTranslation() * Client.player.camera.spatial.perspective);
            Matrix4 spatprev = Matrix4.Invert(Client.player.camera.previous_view_matrix.ClearTranslation() * Client.player.camera.previous_perspective_matrix);

            typeObject.updateUBO_Camera(Client.player.camera.spatial.transformation,
                Client.player.camera.spatial.perspective,
                spatcur,
                Client.player.camera.previous_view_perspective,
                spatprev,
                Client.player.camera.spatial.position,
                Client.player.camera.spatial.look);

            // Set camera's previous MVP matrix
            Client.player.camera.previous_view_matrix = Client.player.camera.spatial.model_view;
            Client.player.camera.previous_perspective_matrix = Client.player.camera.spatial.perspective;

            //_debug_window.render(_fps);

            Debug.logGLError();

        }
    }
}
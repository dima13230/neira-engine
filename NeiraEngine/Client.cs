using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using NeiraEngine.Serialization;
using NeiraEngine.Output;
using NeiraEngine.Input;
using NeiraEngine.World;
using NeiraEngine.World.View;
using NeiraEngine.World.Role;
using NeiraEngine.Game;
using NeiraEngine.Physics;

namespace NeiraEngine
{
    public static class Client
    {
        public static string title { get; private set; }
        public static string startupLevel { get; private set; }
        public static string loadedRenderModule { get; private set; }
        public static Serializer serializer { get; private set; }
        public static Display display { get; private set; }
        public static Scene scene { get; private set; }
        public static Player player { get; private set; }

        private static Camera _camera_1;
        private static PlayableCharacter _character_1;


        internal static void Load()
        {
            title = ClientConfig.title;
            startupLevel = ClientConfig.startupLevel;
            serializer = new Serializer(EngineHelper.path_resources_save_data);
            display = new Display(title, ClientConfig.default_resolution, ClientConfig.default_fullscreen);      
            scene = new Scene(EngineHelper.path_resources_scene);

            player = new Player();


            _camera_1 = new Camera("camera_1", scene, ClientConfig.fov, display.resolution.aspect, ClientConfig.near_far);
            _character_1 = new PlayableCharacter(
                    "Player",
                    (SpatialData)serializer.Load("player_spatial.dat") ?? new SpatialData(new Vector3(), new Vector3(0.0f, 0.0f, -1.0f), new Vector3(0.0f, 10.0f, 0.0f)),
                    ClientConfig.default_movement_speed_walk,
                    ClientConfig.default_movement_speed_run
            );

            Mouse.Init(ClientConfig.default_look_sensitivity, true);
        }

        internal static void Init()
        {
            player.controlAndWatch(_character_1, _camera_1);
            player.getPhysical();

            scene.Load();
            scene.toggleFlashlight(player.enable_flashlight);
        }

        public static void Unload()
        {
            serializer.Save(player.character.spatial, "player_spatial.dat");
            Keyboard.turnOffCapLock();
        }

    }
}
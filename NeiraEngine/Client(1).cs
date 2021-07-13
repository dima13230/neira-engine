using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;

using NeiraEngine.Serialization;
using NeiraEngine.Output;
using NeiraEngine.Input;
using NeiraEngine.World;
using NeiraEngine.World.View;
using NeiraEngine.World.Role;
using NeiraEngine.Game;

namespace NeiraEngine
{
    public class Client
    {
        public string title { get; }
        public string startupLevel { get; }
        public string loadedRenderModule { get; }
        public Serializer serializer { get; }
        public Display display { get; }
        public Scene scene { get; }
        public Keyboard keyboard { get; }
        public Mouse mouse { get; }
        public Player player { get; }


        private Camera _camera_1;
        private PlayableCharacter _character_1;


        public Client()
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

            keyboard = new Keyboard();
            mouse = new Mouse(ClientConfig.default_look_sensitivity, true);
        }


        public void load(Physics.PhysicsWorld physics_world)
        {
            player.controlAndWatch(_character_1, _camera_1);
            player.getPhysical(physics_world);

            scene.load(physics_world);
            scene.toggleFlashlight(player.enable_flashlight);
        }


        public void unload()
        {
            serializer.Save(player.character.spatial, "player_spatial.dat");
            keyboard.turnOffCapLock();
        }

    }
}

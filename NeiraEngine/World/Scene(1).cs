using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NeiraEngine;
using NeiraEngine.Render;
using NeiraEngine.Render.OpenGL;
using NeiraEngine.Animation;
using NeiraEngine.Physics;
using NeiraEngine.World.Model;
using NeiraEngine.World.Lights;

using NeiraEngine.Components;

namespace NeiraEngine.World
{
    public class Scene
    {

        public bool staticMode;

        private string _path_scene;

        public static WorldLoader world_loader { get; private set; }

        public MaterialManager _material_manager { get; private set; }
        public LightManager light_manager { get; private set; }
        public Timer animation_timer { get; }
        public CircadianTimer circadian_timer { get; private set; }
        public float current_animation_time { get; private set; } = 0.0f;
        public List<UniqueMesh> meshes { get; set; }
        public List<SpriteComponent> sprites { get; set; }

        public List<WorldObject> worldObjects { get; private set; } = new List<WorldObject>();

        public List<Light> lights
        {
            get { return light_manager.lights_enabled; }
        }
        public Light flashlight { get; private set; }
        public dLight sun { get; set; }

        public float startTime = 20;
        public float timeSpeed = 0.1f;

        //------------------------------------------------------
        // Constructor
        //------------------------------------------------------

        public Scene(string path_scene)
        {
            _path_scene = path_scene;

            meshes = new List<UniqueMesh>();
            sprites = new List<SpriteComponent>();

            animation_timer = new Timer();
            circadian_timer = new CircadianTimer(startTime, timeSpeed);
        }


        //------------------------------------------------------
        // Flashlight
        //------------------------------------------------------

        public void toggleFlashlight(bool enabled)
        {
            flashlight.enabled = enabled;
        }

        private void load_Flashlight()
        {
            // Load Flashlight
            flashlight = new sLight(
                "flashlight",
                new Vector3(1.0f), 2.0f, 40.0f, MathHelper.DegreesToRadians(70.0f), 0.1f,
                true,
                world_loader.sLight_mesh, Matrix4.Identity,
                this);

            light_manager.addLight(flashlight);
            toggleFlashlight(true);
        }

        private void load_SunLight()
        {
            sun = new dLight(
                "sun",
                true,
                circadian_timer.position,
                new float[]
                {
                    ClientConfig.near_far.X, 5.0f, 20.0f, 50.0f, 100.0f
                },
                this);

            light_manager.addLight(sun);
        }

        //------------------------------------------------------
        // Load Scene
        //------------------------------------------------------

        public void load(PhysicsWorld physics_world)
        {
            _material_manager = new MaterialManager();
            light_manager = new LightManager();

            world_loader = new WorldLoader(_path_scene, "light_objects", physics_world, _material_manager);

            load_Flashlight();
            load_SunLight();

            animation_timer.start();
            circadian_timer.start();
        }

        public void postLoad()
        {
            if(startTime != circadian_timer.time) circadian_timer.time = startTime;
            if (timeSpeed != circadian_timer.speed) circadian_timer.speed = timeSpeed;
        }


        //------------------------------------------------------
        // Update Scene
        //------------------------------------------------------
        private void update_Sun(SpatialData camera_spatial)
        {
            sun.spatial.position = circadian_timer.position;
            sun.update_Cascades(camera_spatial, Vector3.Normalize(circadian_timer.position));
        }

        private void update_Flashlight(SpatialData camera_spatial)
        {
            // Flashlight stuff
            flashlight.unique_mesh.transformation = Matrix4.Invert(camera_spatial.transformation);
            flashlight.bounding_unique_mesh.transformation = flashlight.bounding_unique_mesh.base_transformation * flashlight.unique_mesh.transformation;
            flashlight.spatial.position = -camera_spatial.position;
            flashlight.spatial.rotation_matrix = Matrix4.Transpose(camera_spatial.rotation_matrix);
        }

        public void update(SpatialData camera_spatial)
        {
            current_animation_time = animation_timer.seconds;

            update_Sun(camera_spatial);
            update_Flashlight(camera_spatial);

            light_manager.update(-camera_spatial.position, current_animation_time);
        }

        //------------------------------------------------------
        // Render Scene
        //------------------------------------------------------

        public void pauseAnimation()
        {
            animation_timer.pause();
            circadian_timer.pause();
        }

        public void resetAnimation()
        {
            animation_timer.restart();
            circadian_timer.restart();
        }


        public void renderGL(BeginMode begin_mode, Program program)
        {
            renderMeshesGL(begin_mode, program);
            foreach (WorldObject worldObject in worldObjects)
                worldObject.RenderGL(begin_mode, program, current_animation_time);
            renderLightObjectsGL(begin_mode, program);
        }

        public void renderMeshesGL(BeginMode begin_mode, Program program)
        {
            WorldDrawer.drawMeshesGL(begin_mode, meshes, program, Matrix4.Identity, current_animation_time, 10);
        }

        public void renderMeshesGL_WithMaterials(BeginMode begin_mode, Program program)
        {
            WorldDrawer.drawMeshesGL(begin_mode, meshes, program, Matrix4.Identity, current_animation_time, 1);
        }

        public void renderMeshesGL_Basic(BeginMode begin_mode, Program program)
        {
            WorldDrawer.drawMeshesGL(begin_mode, meshes, program, Matrix4.Identity, current_animation_time, 0);
        }

        public void renderLightObjectsGL(BeginMode begin_mode, Program program)
        {
            WorldDrawer.drawLightsGL(begin_mode, lights, program, Matrix4.Identity, current_animation_time, false);
        }


        //------------------------------------------------------
        // Objects Management
        //------------------------------------------------------

        public WorldObject CreateObject(string id, SpatialData spatial, WorldObject parent = null)
        {
            WorldObject newObject = new WorldObject(id, spatial, scene: this);
            if (parent != null) newObject.parentObject = parent;
            worldObjects.Add(newObject);
            return worldObjects[worldObjects.Count-1];
        }

        public WorldObject CreateObject(string id, WorldObject prototype, WorldObject parent = null)
        {
            WorldObject newObject = new WorldObject(id,prototype: prototype,scene: this);
            if (parent != null) newObject.parentObject = parent;
            worldObjects.Add(newObject);
            newObject.Start(staticMode);
            return newObject;
        }

        public WorldObject CreateObject(string id, WorldObject prototype, SpatialData spatial, WorldObject parent = null)
        {
            WorldObject newObject = new WorldObject(id, prototype, spatial, scene: this);
            if (parent != null) newObject.parentObject = parent;
            worldObjects.Add(newObject);
            newObject.Start(staticMode);
            return newObject;
        }

        public void RemoveObject(string id)
        {
            foreach (WorldObject obj in worldObjects)
                if (obj.id == id)
                {
                    obj.Remove();
                    worldObjects.Remove(obj);
                }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Globalization;

using OpenTK;

using NeiraEngine.Physics;
using NeiraEngine.World.Model;
using NeiraEngine.World.Lights;
using NeiraEngine.Components;
using NeiraEngine.Input;

namespace NeiraEngine.World
{
    public class WorldLoader
    {


        private string _path_scene;
        public Mesh sLight_mesh { get; }
        public Mesh pLight_mesh { get; }

        private MaterialManager _material_manager;

        public WorldLoader(string path_scene, string light_objects_filename,MaterialManager material_manager)
        {
            // Fill Base Paths
            _path_scene = path_scene;

            // Assign a material manager
            _material_manager = material_manager;

            // Load standard light object meshes
            Dictionary<string, UniqueMesh> light_objects = new Dictionary<string, UniqueMesh>();
            Dictionary<string, LightLoader.LightLoaderExtras> garbage_extras = new Dictionary<string, LightLoader.LightLoaderExtras>();
            try
            {
                DAE_Loader.load(
                    _path_scene + light_objects_filename + "/" + light_objects_filename + ".dae",
                    _material_manager,
                    out light_objects,
                    out garbage_extras);
                sLight_mesh = light_objects["sLight"].mesh;
                pLight_mesh = light_objects["pLight"].mesh;
                light_objects.Clear();
                garbage_extras.Clear();
            }
            catch (Exception e)
            {
                Debug.logError("[ ERROR ] Loading World File: " + light_objects_filename, e.Message);
            }
        }

        private string[] createFilePaths(string filename)
        {
            string mesh_filename = _path_scene + filename + "/" + filename + ".dae";
            string physics_filename = _path_scene + filename + "/" + filename + ".physics";
            string lights_filename = _path_scene + filename + "/" + filename + ".lights";
            string level_filename = _path_scene + filename + "/" + filename + ".nengine";

            return new string[]
            {
                mesh_filename,
                physics_filename,
                lights_filename,
                level_filename
            };
        }

        public void createWorld(string filename, out List<UniqueMesh> meshes, out List<Light> lights)
        {
            Debug.logInfo(1, "Loading Static World", filename);

            // Build filenames
            string[] filepaths = createFilePaths(filename);
            string mesh_filename = filepaths[0];
            string physics_filename = filepaths[1];
            string lights_filename = filepaths[2];


            Dictionary<string, UniqueMesh> temp_meshes;
            Dictionary<string, LightLoader.LightLoaderExtras> light_extras;

            DAE_Loader.load(
                mesh_filename,
                _material_manager,
                out temp_meshes,
                out light_extras);
            lights = LightLoader.load(lights_filename, light_extras, sLight_mesh, pLight_mesh);
            PhysicsLoader.load(physics_filename, temp_meshes);
            meshes = temp_meshes.Values.ToList();

            temp_meshes.Clear();
            light_extras.Clear();

            Debug.logInfo(1, "", "");
        }


        public void addWorldToScene(string[] filenames, List<UniqueMesh> meshes, LightManager light_manager)
        {
            List<UniqueMesh> temp_meshes;
            List<Light> temp_lights;

            foreach (string filename in filenames)
            {
                try
                {
                    createWorld(filename, out temp_meshes, out temp_lights);

                    meshes.AddRange(temp_meshes);
                    light_manager.addLight(temp_lights);

                    temp_meshes.Clear();
                    temp_lights.Clear();
                }
                catch (Exception e)
                {
                    Debug.logError("[ ERROR ] Loading Static World File: " + filename, e.Message);
                }
            }
        }

        public void loadLevel(string filename, Scene scene)
        {
            string[] paths = createFilePaths(filename);

            try
            {
                Debug.logInfo(1, "Loading level...", filename);

                if (File.Exists(paths[0]))
                    addWorldToScene(new string[] { filename }, scene.meshes, scene.light_manager);

                if (File.Exists(paths[3]))
                {
                    List<string> scene_settings = new List<string>();

                    WorldObject currentObject = null;
                    Component currentComponent = null;

                    bool scene_settings_mode = false;

                    foreach (string line in File.ReadAllLines(paths[3]))
                    {
                        if (line.Length != 0 && !line.StartsWith("//"))
                        {
                            if (!scene_settings_mode)
                            {
                                string single_value = line.Substring(4);
                                string[] multi_value = line.Substring(4).Split(' ');

                                switch (line.Substring(0, 4))
                                {
                                    case "glob":
                                        scene_settings_mode = true;
                                        break;
                                    case "obj ":
                                        currentObject = scene.CreateObject(single_value, new SpatialData(Matrix4.Identity));
                                        currentComponent = null;
                                        break;
                                    case "pos ":
                                        currentObject.spatial.position = new Vector3(float.Parse(multi_value[0], CultureInfo.InvariantCulture), float.Parse(multi_value[1], CultureInfo.InvariantCulture), float.Parse(multi_value[2], CultureInfo.InvariantCulture));
                                        break;
                                    case "rot ":
                                        currentObject.spatial.rotation_angles = new Vector3(float.Parse(multi_value[0], CultureInfo.InvariantCulture), float.Parse(multi_value[1], CultureInfo.InvariantCulture), float.Parse(multi_value[2], CultureInfo.InvariantCulture));
                                        currentObject.spatial.setRotationMatrixFromEuler();
                                        break;
                                    case "scl ":
                                        currentObject.spatial.scale = new Vector3(float.Parse(multi_value[0], CultureInfo.InvariantCulture), float.Parse(multi_value[1], CultureInfo.InvariantCulture), float.Parse(multi_value[2], CultureInfo.InvariantCulture));
                                        break;
                                    case "com ":
                                        if (currentObject != null)
                                        {
                                            currentComponent = (Component)currentObject.AddComponent(single_value);
                                        }
                                        else
                                        {
                                            Debug.logError("[ ERROR ] Couldn't add component! " + single_value, "No any WorldObject to attach to!");
                                        }
                                        break;
                                    case "par ":
                                        if (currentComponent != null)
                                        {
                                            FieldInfo field = currentComponent.GetType().GetField(multi_value[0]);
                                            Type ftype = field.FieldType;
                                            dynamic value = null;
                                            if (ftype == typeof(string))
                                            {
                                                value = multi_value[1];
                                            }
                                            else if (ftype == typeof(float))
                                            {
                                                value = float.Parse(multi_value[1], CultureInfo.InvariantCulture);
                                            }
                                            else if (ftype == typeof(bool))
                                            {
                                                value = bool.Parse(multi_value[1]);
                                            }
                                            else if (ftype == typeof(Vector3))
                                            {
                                                value = new Vector3(float.Parse(multi_value[1], CultureInfo.InvariantCulture), float.Parse(multi_value[2], CultureInfo.InvariantCulture), float.Parse(multi_value[3], CultureInfo.InvariantCulture));
                                            }
                                            else if (ftype == typeof(Vector2))
                                            {
                                                value = new Vector2(float.Parse(multi_value[1], CultureInfo.InvariantCulture), float.Parse(multi_value[2]));
                                            }

                                            if (value != null) field.SetValue(currentComponent, value);
                                        }
                                        else
                                        {
                                            Debug.logError("[ ERROR ] Couldn't set parameter! " + multi_value[0], "No any Component to set in!");
                                        }
                                        break;
                                    case "cld ":
                                        if (multi_value.Length > 1)
                                        {
                                            if (multi_value[0] == "pfb")
                                            {
                                                WorldObject pfb = PrefabManager.GetPrefab(multi_value[1]);
                                                currentObject = scene.CreateObject(multi_value[1], pfb, new SpatialData(Matrix4.Identity), currentObject);
                                            }
                                        }
                                        else
                                            currentObject = scene.CreateObject(single_value, new SpatialData(Matrix4.Identity), currentObject);
                                        break;
                                    case "ecld":
                                        if (currentObject.parentObject != null) currentObject = currentObject.parentObject;
                                        break;
                                    case "pfb ":
                                        currentObject = scene.CreateObject(single_value, PrefabManager.GetPrefab(multi_value[0]));
                                        currentComponent = null;
                                        break;
                                }
                            }
                            else
                            {
                                if (line == "eglob")
                                {
                                    ConfigReader scene_settings_reader = new ConfigReader(scene_settings.ToArray());

                                    foreach (KeyValuePair<string, string[]> pair in scene_settings_reader.values)
                                    {
                                        FieldInfo field = scene.GetType().GetField(pair.Key);
                                        Type ftype = field.FieldType;
                                        dynamic value = null;
                                        if (ftype == typeof(string))
                                        {
                                            value = pair.Value[0];
                                        }
                                        else if (ftype == typeof(float))
                                        {
                                            value = float.Parse(pair.Value[0], CultureInfo.InvariantCulture);
                                        }
                                        else if (ftype == typeof(bool))
                                        {
                                            value = bool.Parse(pair.Value[0]);
                                        }
                                        else if (ftype == typeof(Vector3))
                                        {
                                            value = new Vector3(float.Parse(pair.Value[0], CultureInfo.InvariantCulture), float.Parse(pair.Value[1], CultureInfo.InvariantCulture), float.Parse(pair.Value[2], CultureInfo.InvariantCulture));
                                        }
                                        else if (ftype == typeof(Vector2))
                                        {
                                            value = new Vector2(float.Parse(pair.Value[0], CultureInfo.InvariantCulture), float.Parse(pair.Value[1]));
                                        }

                                        if (value != null) field.SetValue(scene, value);
                                    }

                                    scene_settings_mode = false;
                                }
                                else
                                {
                                    scene_settings.Add(line);
                                }
                            }
                        }
                    }
                }
                scene.postLoad();
            }
            catch (Exception e)
            {
                Debug.logError("[ ERROR ] Loading World File: " + filename, e.ToString());
            }
        }

    }
}
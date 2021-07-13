using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using System.Reflection;

using NeiraEngine.World;
using NeiraEngine.Components;
using OpenTK;

namespace NeiraEngine.World
{
    public static class PrefabManager
    {
        static List<WorldObject> prefabs = new List<WorldObject>();

        public static WorldObject GetPrefab(string name)
        {
            foreach(WorldObject prefab in prefabs)
            {
                if (prefab.id == name)
                    return prefab;
            }
            return Load(name);
        }

        public static WorldObject Load(string name)
        {
            WorldObject worldObject = new WorldObject(name.Replace("/","."), new SpatialData(Matrix4.Identity));
            WorldObject originalObject = worldObject;

            Debug.logInfo(1, "Loading prefab...", name);

            string filename = EngineHelper.path_resources_prefabs + name + ".pfb";
            Console.WriteLine(filename);
            try
            {
                Component currentComponent = null;

                foreach (string line in File.ReadAllLines(filename))
                {
                    if (line.Length != 0)
                    {
                        string single_value = line.Substring(4);
                        string[] multi_value = line.Substring(4).Split(' ');

                        switch (line.Substring(0, 4))
                        {
                            case "pos ":
                                worldObject.spatial.position = new Vector3(float.Parse(multi_value[0], CultureInfo.InvariantCulture), float.Parse(multi_value[1], CultureInfo.InvariantCulture), float.Parse(multi_value[2], CultureInfo.InvariantCulture));
                                break;
                            case "rot ":
                                worldObject.spatial.rotation_angles = new Vector3(float.Parse(multi_value[0], CultureInfo.InvariantCulture), float.Parse(multi_value[1], CultureInfo.InvariantCulture), float.Parse(multi_value[2], CultureInfo.InvariantCulture));
                                worldObject.spatial.setRotationMatrixFromEuler();
                                break;
                            case "scl ":
                                worldObject.spatial.scale = new Vector3(float.Parse(multi_value[0], CultureInfo.InvariantCulture), float.Parse(multi_value[1], CultureInfo.InvariantCulture), float.Parse(multi_value[2], CultureInfo.InvariantCulture));
                                break;
                            case "com ":
                                currentComponent = (Component)worldObject.AddComponent(single_value);
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
                                        value = multi_value[1] == "1" ? true : false;
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
                                        WorldObject pfb = GetPrefab(multi_value[1]);
                                        worldObject = new WorldObject(multi_value[1], pfb, new SpatialData(Matrix4.Identity), worldObject);
                                    }
                                }
                                else
                                    worldObject = new WorldObject(single_value, new SpatialData(Matrix4.Identity), worldObject);
                                break;
                            case "ecld":
                                if (worldObject.parentObject != null) worldObject = worldObject.parentObject;
                                break;
                            case "pfb ":
                                worldObject = new WorldObject(single_value,parent: GetPrefab(multi_value[0]));
                                currentComponent = null;
                                break;
                        }
                    }
                }

                return originalObject;
            }
            catch (Exception e)
            {
                Debug.logError("[ ERROR ] Loading Prefab: " + filename, e.ToString());
                return null;
            }
        }
    }
}

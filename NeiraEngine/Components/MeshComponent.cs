using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL;

using System.IO;

using NeiraEngine.Render;
using NeiraEngine.World;
using NeiraEngine.World.Model;

namespace NeiraEngine.Components
{
    class MeshComponent : Component
    {

        public string filename = "";

        public MaterialManager materialManager;

        public List<UniqueMesh> meshes;

        public Vector3 overagePosition
        {
            get
            {
                Vector3 pos = new Vector3();
                foreach (UniqueMesh mesh in meshes)
                    pos += mesh.transformation.ExtractTranslation();
                pos /= meshes.Count;
                return pos;
            }
        }

        public Vector3 overagePhysicsPosition
        {
            get
            {
                Vector3 pos = new Vector3();
                foreach (UniqueMesh mesh in meshes)
                    pos += ((Matrix4)mesh.physics_object.body.WorldTransform).ExtractTranslation();
                pos /= meshes.Count;
                return pos;
            }
        }

        public MeshComponent(WorldObject relatedTo) : base(relatedTo)
        { }

        public override void Start()
        {
            Debug.logInfo(1, "Requested loading of mesh", filename);
            Dictionary<string, UniqueMesh> meshD;
            Dictionary<string, LightLoader.LightLoaderExtras> lle;

            materialManager = scene._material_manager;
            string fname = EngineHelper.path_resources_models + filename;

            if (!Path.HasExtension(fname))
                fname += ".dae";

            if (File.Exists(fname))
            {
                DAE_Loader.load(fname, materialManager, out meshD, out lle);
                meshes = meshD.Values.ToList();
                scene.meshes.AddRange(meshes);
            }

            SpatialData spatial = worldObject.spatial;
            foreach (UniqueMesh mesh in meshes)
            {
                mesh.transformation = mesh.base_transformation * worldObject.spatial.transformation + worldObject.spatial.scale_matrix;
            }
        }

        public override void Update()
        {
            SpatialData spatial = worldObject.spatial;
            foreach (UniqueMesh mesh in meshes)
                mesh.transformation = mesh.base_transformation * worldObject.spatial.transformation + worldObject.spatial.scale_matrix;
            worldObject.spatial.position = overagePhysicsPosition;
        }

        public override void Remove()
        {
            worldObject.parentScene.meshes.RemoveAll(mesh => meshes.Contains(mesh));
        }
    }
}

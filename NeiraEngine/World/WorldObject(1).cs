using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NeiraEngine.Components;
using NeiraEngine.Render;

using NeiraEngine.Client;

namespace NeiraEngine.World
{
    public class WorldObject
    {

        public Scene parentScene
        {
            get;
            private set;
        }

        public bool initiallyStatic { get; private set; }

        public WorldObject parentObject;
        public List<WorldObject> childs = new List<WorldObject>();

        protected List<Component> components = new List<Component>();

        protected string _id;
        public string id
        {
            get { return _id; }
            set { _id = value; }
        }


        protected SpatialData _spatial;
        public SpatialData spatial
        {
            get { return _spatial; }
            set { _spatial = value; }
        }


        public Vector3 globalPosition {
            get
            {
                Vector3 pos = spatial.position;
                if(parentObject != null)
                    pos += parentObject.globalPosition;
                return pos;
            }
        }

        public Matrix4 globalRotationMatrix
        {
            get
            {
                Matrix4 mat = spatial.rotation_matrix;
                if( parentObject != null )
                    mat += parentObject.globalRotationMatrix;
                return mat;
            }
        }

        public WorldObject(string id, WorldObject parent = null, Scene scene = null)
            : this (id, new SpatialData(Matrix4.Identity), scene: scene)
        {
            if (parent != null) { parentObject = parent; parentScene = parent.parentScene; }
            if (scene != null) parentScene = scene;
        }

        public WorldObject(string id, SpatialData spatial, WorldObject parent = null, Scene scene = null)
        {
            _id = id;
            _spatial = spatial;
            if (parent != null) { parentObject = parent; parentScene = parent.parentScene; }
            if (scene != null) parentScene = scene;
        }

        public WorldObject(string id, WorldObject prototype, WorldObject parent = null, Scene scene = null)
        {
            _id = id;
            foreach (Component component in prototype.components)
            {
                components.Add(component);
            }
            if (parent != null) { parentObject = parent; parentScene = parent.parentScene; }
            if (scene != null) parentScene = scene;
        }

        public WorldObject(string id, WorldObject prototype, SpatialData customSpatial, WorldObject parent = null, Scene scene = null)
        {
            _id = id;
            _spatial = customSpatial;
            foreach (Component component in prototype.components)
            {
                components.Add(component);
            }
            if (parent != null) { parentObject = parent; parentScene = parent.parentScene; }
            if (scene != null) parentScene = scene;
        }

        public T AddComponent<T>() where T : Component
        {
            T com = (T)Activator.CreateInstance(typeof(T), new object[] { this });
            components.Add(com);
            return com;
        }

        public object AddComponent(string name)
        {
            Type type = Type.GetType(name);
            object obj = Activator.CreateInstance(type,new object[] { this });
            components.Add((Component)obj);
            return Convert.ChangeType(obj, obj.GetType());
        }

        public T GetComponent<T>() where T : Component
        {
            foreach (Component com in components)
                if (com.GetType() == typeof(T))
                    return (T)com;
            return null;
        }

        public Component[] GetComponents()
        {
            return components.ToArray();
        }

        public void RemoveComponent<T>() where T : Component
        {
            foreach (Component com in components)
                if (com.GetType() == typeof(T))
                {
                    com.Remove();
                    components.Remove(com);
                }
        }

        public void Start(bool staticMode)
        {
            initiallyStatic = staticMode ? true : false;
            foreach (Component component in components)
                if (!staticMode || (staticMode && (component.GetType() == typeof(MeshComponent) || component.GetType() == typeof(DirectionalLightComponent) || component.GetType() == typeof(PointLightComponent) || component.GetType() == typeof(SpotLightComponent))))
                    component.Start();
        }

        public void Update(bool staticMode)
        {
            foreach (Component component in components)
                if (!staticMode || (staticMode && (component.GetType() == typeof(MeshComponent) || component.GetType() == typeof(DirectionalLightComponent) || component.GetType() == typeof(PointLightComponent) || component.GetType() == typeof(SpotLightComponent))))
                    component.Update();

            if(initiallyStatic && !staticMode)
            {
                Start(false);
            }
        }

        public void Remove()
        {
            foreach (Component component in components)
                component.Remove();
        }

        public void RenderGL(Render.OpenGL.BeginMode beginMode, Program program, float sceneTime)
        {
            //foreach (Component component in components)
            //    component.OnRender(beginMode, program, sceneTime);
        }

        //------------------------------------------------------
        // Rotation
        //------------------------------------------------------

        public virtual void rotate(float x_angle, float y_angle, float z_angle)
        {
            _spatial.rotation_angles = new Vector3(x_angle, y_angle, z_angle);

            Quaternion x_rotation = Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(x_angle));
            Quaternion y_rotation = Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(y_angle));
            Quaternion z_rotation = Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(z_angle));

            Quaternion zyx_rotation = Quaternion.Multiply(Quaternion.Multiply(z_rotation, y_rotation), x_rotation);

            zyx_rotation.Normalize();
            _spatial.rotation_matrix = Matrix4.CreateFromQuaternion(zyx_rotation);

        }

        //------------------------------------------------------
        // View Based Movement
        //------------------------------------------------------

        public void moveForeward(float speed)
        {
            _spatial.position += _spatial.look * speed;
        }

        public void moveBackward(float speed)
        {
            _spatial.position -= _spatial.look * speed;
        }

        public void moveUp(float speed)
        {
            _spatial.position -= _spatial.up * speed;
        }

        public void moveDown(float speed)
        {
            _spatial.position += _spatial.up * speed;
        }

        public void strafeRight(float speed)
        {
            _spatial.position -= _spatial.strafe * speed;
        }

        public void strafeLeft(float speed)
        {
            _spatial.position += _spatial.strafe * speed;
        }

    }
}

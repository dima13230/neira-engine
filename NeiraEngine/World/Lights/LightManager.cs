﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;

using NeiraEngine.Render.OpenGL;

namespace NeiraEngine.World.Lights
{
    public class LightManager
    {
        private int _max_shadows_spot = 4;
        private int _max_shadows_point = 2;
        private int _max_shadows_directional = 1;

        private int _light_count;


        private UniformBuffer _ubo_shadow_manifest;
        private UniformBuffer _ubo_shadow_spot;
        private UniformBuffer _ubo_shadow_point;
        private UniformBuffer _ubo_shadow_directional;


        private Dictionary<int, Light> _lights;
        public Dictionary<int, Light> lights
        {
            get { return _lights; }
        }

        public List<Light> lights_enabled { get; private set; }

        public List<Light> lights_shadowed { get; private set; }

        // Manifest of shadowed lights that are shadowed in game
        // 0 - Spot
        // 1 - Point
        // 2 - Directional
        public List<Vector4> lights_shadowed_manifest { get; private set; }


        public Light[] lights_shadowed_spot
        {
            get
            {
                return lights_shadowed.Where(l => l.type == Light.type_spot).ToArray();
            }
        }
        public Light[] lights_shadowed_point
        {
            get
            {
                return lights_shadowed.Where(l => l.type == Light.type_point).ToArray();
            }
        }
        public Light[] lights_shadowed_directional
        {
            get
            {
                return lights_shadowed.Where(l => l.type == Light.type_directional).ToArray();
            }
        }



        public LightManager()
        {
            _light_count = 0;
            _lights = new Dictionary<int, Light>();
            lights_enabled = new List<Light>();
            lights_shadowed = new List<Light>();
            lights_shadowed_manifest = new List<Vector4>();

            //vec4 info - (light type, id of this type, layer id, --)
            _ubo_shadow_manifest = new UniformBuffer(BufferStorageFlags.DynamicStorageBit, 3, new EngineHelper.size[] {
                EngineHelper.size.vec4
            }, 32);

            //mat4 view
            //mat4 perspective
            //mat4 viewray
            //vec4 - vec3 light_position / float falloff
            //vec4 - vec3 light_color / float intensity
            //vec4 - vec3 light_direction
            //vec4 - float spot_angle / float spot_blur
            _ubo_shadow_spot = new UniformBuffer(BufferStorageFlags.DynamicStorageBit, 4, new EngineHelper.size[] {
                EngineHelper.size.mat4,
                EngineHelper.size.mat4,
                EngineHelper.size.mat4,
                EngineHelper.size.vec4,
                EngineHelper.size.vec4,
                EngineHelper.size.vec4,
                EngineHelper.size.vec4,
            }, 32);

            //mat4 view[6]
            //mat4 perspective
            //vec4 - vec3 light_position / float falloff
            //vec4 - vec3 light_color / float intensity
            _ubo_shadow_point = new UniformBuffer(BufferStorageFlags.DynamicStorageBit, 5, new EngineHelper.size[] {
                EngineHelper.size.mat4,
                EngineHelper.size.mat4,
                EngineHelper.size.mat4,
                EngineHelper.size.mat4,
                EngineHelper.size.mat4,
                EngineHelper.size.mat4,
                EngineHelper.size.mat4,
                EngineHelper.size.mat4,
                EngineHelper.size.mat4,
                EngineHelper.size.mat4,
                EngineHelper.size.mat4,
                EngineHelper.size.mat4,
                EngineHelper.size.mat4,
                EngineHelper.size.vec4,
                EngineHelper.size.vec4,
            }, 32);

            //mat4 view[4]
            //mat4 perspective[4]
            //vec4 light_position
            _ubo_shadow_directional = new UniformBuffer(BufferStorageFlags.DynamicStorageBit, 6, new EngineHelper.size[]
            {
                EngineHelper.size.mat4,
                EngineHelper.size.mat4,
                EngineHelper.size.mat4,
                EngineHelper.size.mat4,
                EngineHelper.size.mat4,
                EngineHelper.size.mat4,
                EngineHelper.size.mat4,
                EngineHelper.size.mat4,
                EngineHelper.size.vec4,
            }, 1);
        }

        //------------------------------------------------------
        // Build Lists
        //------------------------------------------------------

        public void addLight(Light light)
        {
            light.lid = _light_count;
            light.enabled = true;
            _lights.Add(light.lid, light);
            _light_count++;

            if (light.enabled)
            {
                lights_enabled.Add(light);
                if(light.shadowed)
                {
                    lights_shadowed.Add(light);
                }
            }
        }

        public void addLight(List<Light> lights)
        {
            foreach(Light light in lights)
            {
                addLight(light);
            }
        }

        public void removeLight(Light light)
        {
            bool found = false;
            for (int i = 0; i < _lights.Count; i++)
            {
                if (light == _lights[i])
                {
                    _lights.Remove(i);
                    found = true;
                }
                if (found)
                    _lights[i].lid--;
            }

            if (found)
            {
                lights_enabled.Remove(light);
                lights_shadowed.Remove(light);
                _light_count--;
            }
        }

        public void removeLight(List<Light> lights)
        {
            foreach (Light light in lights)
            {
                bool found = false;
                for (int i = 0; i < _lights.Count; i++)
                {
                    if (light == _lights[i])
                    {
                        _lights.Remove(i);
                        found = true;
                    }
                    if (found)
                        _lights[i].lid--;
                }

                if (found)
                {
                    lights_enabled.Remove(light);
                    lights_shadowed.Remove(light);
                    _light_count--;
                }
            }
        }

        //------------------------------------------------------
        // Updating
        //------------------------------------------------------
        private void updateLists(Vector3 camera_position)
        {
            lights_enabled = _lights.Select(kp => kp.Value).Where(light => light.enabled).ToList();
            // Sort shadow casters by closest to the camera
            lights_shadowed = lights_enabled.Where(light => light.shadowed).OrderBy(light => (light.spatial.position - camera_position).Length).ToList();
            lights_shadowed.ForEach(light => light.sid = -1);
            lights_shadowed_manifest.Clear();
        }

        private void updateUBO_Shadow_Spot(sLight light, int shadow_id)
        {
            int ubo_index = shadow_id * 7;

            _ubo_shadow_spot.update(ubo_index, light.shadow_view_matrix);
            _ubo_shadow_spot.update(ubo_index + 1, light.shadow_perspective_matrix);
            _ubo_shadow_spot.update(ubo_index + 2, light.viewray_matrix);
            _ubo_shadow_spot.update(ubo_index + 3, new Vector4(light.spatial.position, light.falloff));
            _ubo_shadow_spot.update(ubo_index + 4, new Vector4(light.color, light.intensity));
            _ubo_shadow_spot.update(ubo_index + 5, new Vector4(light.spatial.look, 0.0f));
            _ubo_shadow_spot.update(ubo_index + 6, new Vector4(light._spot_angle, light._spot_blur, 0.0f, 0.0f));

            light.sid = shadow_id;
        }

        private void updateUBO_Shadow_Point(pLight light, int shadow_id)
        {
            int ubo_index = shadow_id * 15;

            for(int i = 0; i < 6; i++)
            {
                _ubo_shadow_point.update(ubo_index + i, light.shadow_view_matrices[i]);
            }
            _ubo_shadow_point.update(ubo_index + 6, light.spatial.perspective);
            for (int i = 0; i < 6; i++)
            {
                _ubo_shadow_point.update(ubo_index + i + 7, light.viewray_matrices[i]);
            }
            _ubo_shadow_point.update(ubo_index + 13, new Vector4(light.spatial.position, light.falloff));
            _ubo_shadow_point.update(ubo_index + 14, new Vector4(light.color, light.intensity));

            light.sid = shadow_id;
        }

        private void updateUBO_Shadow_Directional(dLight light, int shadow_id)
        {
            int ubo_index = shadow_id * 9;

            for (int i = 0; i < 4; i++)
            {
                _ubo_shadow_directional.update(ubo_index + i, light.shadow_view_matrices[i]);
                _ubo_shadow_directional.update(ubo_index + i + 4, light.shadow_ortho_matrices[i]);
            }
            _ubo_shadow_directional.update(ubo_index + 8, light.spatial.position);

            light.sid = shadow_id;
        }

        private void updateUBO_Shadow()
        {
            int num_shadows_spot = 0;
            int num_shadows_point = 0;
            int num_shadows_directional = 0;

            foreach (Light light in lights_shadowed)
            {
                switch(light.type)
                {
                    case Light.type_spot:
                        if (num_shadows_spot >= _max_shadows_spot) break;
                        updateUBO_Shadow_Spot((sLight)light, num_shadows_spot);
                        lights_shadowed_manifest.Add(new Vector4(0, num_shadows_spot, 0, 0));
                        num_shadows_spot++;
                        break;
                    case Light.type_point:
                        if (num_shadows_point >= _max_shadows_point) break;
                        updateUBO_Shadow_Point((pLight)light, num_shadows_point);
                        lights_shadowed_manifest.Add(new Vector4(1, num_shadows_point, 0, 0));
                        lights_shadowed_manifest.Add(new Vector4(1, num_shadows_point, 1, 0));
                        lights_shadowed_manifest.Add(new Vector4(1, num_shadows_point, 2, 0));
                        lights_shadowed_manifest.Add(new Vector4(1, num_shadows_point, 3, 0));
                        lights_shadowed_manifest.Add(new Vector4(1, num_shadows_point, 4, 0));
                        lights_shadowed_manifest.Add(new Vector4(1, num_shadows_point, 5, 0));
                        num_shadows_point++;
                        break;
                    case Light.type_directional:
                        if (num_shadows_directional >= _max_shadows_directional) break;
                        updateUBO_Shadow_Directional((dLight)light, num_shadows_directional);
                        lights_shadowed_manifest.Add(new Vector4(2, num_shadows_directional, 0, 0));
                        num_shadows_directional++;
                        break;
                }
            }

            int shadow_manifest_length = lights_shadowed_manifest.Count;
            for (int i = 0; i < 32; i ++)
            {
                Vector4 temp_manifest_entry = new Vector4(-1);
                if (i < shadow_manifest_length) temp_manifest_entry = lights_shadowed_manifest[i];
                _ubo_shadow_manifest.update(i, temp_manifest_entry);
            }
        }

        private void animateLights(float animation_time)
        {
            foreach(Light light in lights_enabled)
            {
                if (light.animated)
                {
                    light.unique_mesh.transformation = light.animator.getKeyFrame(animation_time, -1);
                    light.spatial.transformation = light.unique_mesh.transformation;

                    Matrix4 transformation = light.bounds_matrix * light.unique_mesh.transformation.ClearScale();
                    light.bounding_unique_mesh.transformation = transformation;
                }
            }
        }

        public void update(Vector3 camera_position, float animation_time)
        {
            animateLights(animation_time);
            updateLists(camera_position);
            updateUBO_Shadow();
        }
    }
}

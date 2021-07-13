﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NeiraEngine;
using NeiraEngine.Render;

namespace NativeOpenGL
{
    public abstract class RenderEffect
    {

        protected bool _enabled;
        public bool enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        protected string _path_glsl_effect;

        protected string _path_static_textures;

        protected Resolution _resolution;
        protected Resolution _resolution_half;

        public RenderEffect(string resource_folder_name,  Resolution full_resolution)
        {
            _enabled = true;
            _path_glsl_effect = resource_folder_name;
            _path_static_textures = resource_folder_name;
            _resolution = full_resolution;
            _resolution_half = new Resolution(_resolution.W * 0.5f, _resolution.H * 0.5f);
        }



        protected abstract void load_Programs();

        protected abstract void load_Buffers();

        public abstract void load();

        public abstract void unload();

        public abstract void reload();

        public void toggle()
        {
            _enabled = !_enabled;
        }
    }
}

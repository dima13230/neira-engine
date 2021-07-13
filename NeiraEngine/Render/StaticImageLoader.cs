using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiraEngine.Render
{
    public static class StaticImageLoader
    {

        public static Image createImage(string static_texture_path, TextureTarget texture_target, TextureWrapMode wrap_mode, bool use_srgb = true)
        {
            Image temp_image = new Image(EngineHelper.path_resources_textures_static + static_texture_path, use_srgb, texture_target, wrap_mode);
            temp_image.load();
            return temp_image;
        }

        public static Image createImage(string[] files, TextureTarget texture_target, TextureWrapMode wrap_mode, bool use_srgb)
        {
            List<string> filepaths = new List<string>();
            foreach (string file in files)
            {
                filepaths.Add(EngineHelper.path_resources_textures_static + file);
            }
            Image temp_image = new Image(filepaths.ToArray(), use_srgb, texture_target, wrap_mode, true, true);
            temp_image.load();

            filepaths.Clear();
            return temp_image;
        }

    }
}

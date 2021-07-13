using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Graphics.OpenGL;

namespace NeiraEngine.Render
{
    public class Image
    {
        private struct ImageData
        {
            public Bitmap bitmap;
            public System.Drawing.Imaging.BitmapData bitmap_data;

            public ImageData(Bitmap bitmap, System.Drawing.Imaging.BitmapData bitmap_data)
            {
                this.bitmap = bitmap;
                this.bitmap_data = bitmap_data;
            }
        }

        private List<ImageData> _image_data_list;

        public Texture texture { get; }

        //------------------------------------------------------
        // Constructor
        //------------------------------------------------------
        public Image(string filename, bool use_srgb)
            : this(new string[] { filename }, use_srgb, TextureTarget.Texture2D, TextureWrapMode.Repeat, true, true)
        { }

        public Image(string filename, bool use_srgb, TextureTarget texture_target = TextureTarget.Texture2D, TextureWrapMode wrap_mode = TextureWrapMode.Repeat)
            : this(new string[] { filename }, use_srgb, texture_target, wrap_mode, true, true)
        { }

        public Image(Bitmap bitmap, bool use_srgb)
            : this(new Bitmap[] { bitmap }, use_srgb, TextureTarget.Texture2D, TextureWrapMode.Repeat, true, true)
        { }

        public Image(Bitmap bitmap, bool use_srgb, TextureTarget texture_target = TextureTarget.Texture2D, TextureWrapMode wrap_mode = TextureWrapMode.Repeat)
            : this(new Bitmap[] { bitmap }, use_srgb, texture_target, wrap_mode, true, true)
        { }

        public Image(string[] filenames, bool use_srgb, TextureTarget texture_target, TextureWrapMode wrap_mode, bool enable_mipmap, bool enable_aniso)
        {
            _image_data_list = new List<ImageData>();

            PixelInternalFormat pif = PixelInternalFormat.Rgba;
            PixelFormat pf = PixelFormat.Bgra;
            PixelType pt = PixelType.UnsignedByte;

            int texture_width = 0;
            int texture_height = 0;

            foreach (string file in filenames)
            {
                string filename = file.Replace("%20", " ");
                if (File.Exists(filename))
                {
                    try
                    {
                        // Create a new image data struct and load bitmap data into it
                        ImageData image_data = new ImageData();

                        image_data.bitmap = new Bitmap(filename);

                        texture_width = image_data.bitmap.Width;
                        texture_height = image_data.bitmap.Height;

                        image_data.bitmap_data = image_data.bitmap.LockBits(
                            new Rectangle(0, 0, texture_width, texture_height),
                            System.Drawing.Imaging.ImageLockMode.ReadOnly,
                            image_data.bitmap.PixelFormat);


                        // Add image data to list so we can load it into textures later
                        _image_data_list.Add(image_data);

                        // Get the texture format from bitmap pixel format
                        getImageFormat(image_data.bitmap.PixelFormat, ref pif, ref pf, ref pt);
                    }
                    catch (Exception e)
                    {
                        Debug.logError("[ ERROR ] Unable to load Image: ", filename + "\n" + e.Message);
                    }
                }
                else
                {
                    Debug.logError("Texture Not Found", filename);
                }
            }

            if (use_srgb) pif = PixelInternalFormat.Srgb;

            // Load new texture
            texture = new Texture(
                texture_target,
                texture_width, texture_height, filenames.Length,
                enable_mipmap, enable_aniso,
                pif, pf, pt,
                TextureMinFilter.Linear, TextureMagFilter.Linear, wrap_mode);
        }

        public Image(Bitmap[] bitmaps, bool use_srgb, TextureTarget texture_target, TextureWrapMode wrap_mode, bool enable_mipmap, bool enable_aniso)
        {
            _image_data_list = new List<ImageData>();

            PixelInternalFormat pif = PixelInternalFormat.Rgba;
            PixelFormat pf = PixelFormat.Bgra;
            PixelType pt = PixelType.UnsignedByte;

            int texture_width = 0;
            int texture_height = 0;

            foreach (Bitmap bitmap in bitmaps)
            {
                try
                {
                    // Create a new image data struct and load bitmap data into it
                    ImageData image_data = new ImageData();

                    image_data.bitmap = bitmap;

                    texture_width = image_data.bitmap.Width;
                    texture_height = image_data.bitmap.Height;

                    image_data.bitmap_data = image_data.bitmap.LockBits(
                        new Rectangle(0, 0, texture_width, texture_height),
                        System.Drawing.Imaging.ImageLockMode.ReadOnly,
                        image_data.bitmap.PixelFormat);


                    // Add image data to list so we can load it into textures later
                    _image_data_list.Add(image_data);

                    // Get the texture format from bitmap pixel format
                    getImageFormat(image_data.bitmap.PixelFormat, ref pif, ref pf, ref pt);
                }
                catch (Exception e)
                {
                    Debug.logError("[ ERROR ] Unable to load Image! ", e.Message);
                }
            }
            if (use_srgb) pif = PixelInternalFormat.Srgb;

            // Load new texture
            texture = new Texture(
                texture_target,
                texture_width, texture_height, bitmaps.Length,
                    enable_mipmap, enable_aniso,
                pif, pf, pt,
                TextureMinFilter.Linear, TextureMagFilter.Linear, wrap_mode);
        }

        //------------------------------------------------------
        // Helpers
        //------------------------------------------------------

        private void getImageFormat(System.Drawing.Imaging.PixelFormat bitmap_format, ref PixelInternalFormat pif, ref PixelFormat pf, ref PixelType pt)
        {
            switch (bitmap_format)
            {
                case System.Drawing.Imaging.PixelFormat.Format8bppIndexed: // misses glColorTable setup
                    pif = PixelInternalFormat.Rgb8;
                    pf = PixelFormat.ColorIndex;
                    pt = PixelType.Bitmap;
                    break;
                case System.Drawing.Imaging.PixelFormat.Format16bppArgb1555:
                case System.Drawing.Imaging.PixelFormat.Format16bppRgb555: // does not work
                    pif = PixelInternalFormat.Rgb5A1;
                    pf = PixelFormat.Bgr;
                    pt = PixelType.UnsignedShort5551Ext;
                    break;
                //case System.Drawing.Imaging.PixelFormat.Format16bppRgb565:
                //    pif = OpenTK.Graphics.OpenGL.PixelInternalFormat.R5G6B5IccSgix;
                //    pf = OpenTK.Graphics.OpenGL.PixelFormat.R5G6B5IccSgix;
                //    pt = OpenTK.Graphics.OpenGL.PixelType.UnsignedByte;
                //    break;
                case System.Drawing.Imaging.PixelFormat.Format24bppRgb: // works
                    pif = PixelInternalFormat.Rgb8;
                    pf = PixelFormat.Bgr;
                    pt = PixelType.UnsignedByte;
                    break;
                case System.Drawing.Imaging.PixelFormat.Format32bppRgb: // has alpha too? wtf?
                case System.Drawing.Imaging.PixelFormat.Canonical:
                case System.Drawing.Imaging.PixelFormat.Format32bppArgb: // works
                    pif = PixelInternalFormat.Rgba;
                    pf = PixelFormat.Bgra;
                    pt = PixelType.UnsignedByte;
                    break;
                default:
                    throw new Exception("Unsupported Pixel Format: " + bitmap_format.ToString());
            }
        }


        //------------------------------------------------------
        // Loading
        //------------------------------------------------------

        public void load()
        {
            if(_image_data_list.Count == 1)
            {
                // Load texture into GL memory
                texture.load(_image_data_list[0].bitmap_data.Scan0);

                // Dispose of bitmap in System memory
                _image_data_list[0].bitmap.UnlockBits(_image_data_list[0].bitmap_data);
                _image_data_list[0].bitmap.Dispose();
            }
            else
            {
                texture.load(_image_data_list.Select(d => d.bitmap_data.Scan0).ToArray());
                foreach(ImageData data in _image_data_list)
                {
                    data.bitmap.UnlockBits(data.bitmap_data);
                    data.bitmap.Dispose();
                }
            }

            _image_data_list.Clear();
        }

        public long loadBindless()
        {
            load();
            texture.loadBindless();
            texture.makeResident();
            return texture.handle;
        }


        //------------------------------------------------------
        // Binding
        //------------------------------------------------------

        public void bind(int texture_uniform, int index)
        {
            texture.bind(texture_uniform, index);
        }

        public void makeResident()
        {
            texture.makeResident();
        }

        public void makeNonResident()
        {
            texture.makeNonResident();
        }

    }
}

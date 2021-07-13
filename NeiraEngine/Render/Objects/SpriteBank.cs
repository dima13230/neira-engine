using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Media.Imaging;

using System.IO;

using OpenTK.Graphics.OpenGL;

namespace NeiraEngine.Render
{
    public class SpriteBank
    {
        public List<Image> Images = new List<Image>();

        public SpriteBank(string filename)
        {
            if (File.Exists(filename))
            {
                if (Path.GetExtension(filename) == ".gif")
                {
                    FileStream stream = new FileStream(filename, FileMode.Open);
                    stream.Lock(0, stream.Length);
                    GifBitmapDecoder gdec = new GifBitmapDecoder(stream, BitmapCreateOptions.None, BitmapCacheOption.Default);
                    foreach (BitmapFrame frame in gdec.Frames)
                    {
                        Bitmap bmap = GetBitmap(frame);
                        Image image = new Image(new Bitmap[] { bmap }, false, TextureTarget.Texture2D, TextureWrapMode.ClampToEdge, true, true);
                        image.load();
                        Images.Add(image);
                    }
                    stream.Unlock(0, stream.Length);
                    stream.Dispose();
                }
                else
                {
                    Image image = new Image(new string[] { filename }, false, TextureTarget.Texture2D, TextureWrapMode.ClampToEdge, true, true);
                    image.load();
                    Images.Add(image);
                }
            }
        }


        // Helper converter
        Bitmap GetBitmap(BitmapFrame source)
        {
            Bitmap bmp = new Bitmap(
              source.PixelWidth,
              source.PixelHeight,
              System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            BitmapData data = bmp.LockBits(
              new Rectangle(System.Drawing.Point.Empty, bmp.Size),
              ImageLockMode.WriteOnly,
              System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

            source.CopyPixels(
              Int32Rect.Empty,
              data.Scan0,
              data.Height * data.Stride,
              data.Stride);
            bmp.UnlockBits(data);
            return bmp;
        }
    }
}
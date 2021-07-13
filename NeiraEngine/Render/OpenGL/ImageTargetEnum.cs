using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiraEngine.Render.OpenGL
{
    public enum ImageTarget
    {
        //
        // Сводка:
        //     Original was GL_TEXTURE_1D = 0x0DE0
        Texture1D = 3552,
        //
        // Сводка:
        //     Original was GL_TEXTURE_2D = 0x0DE1
        Texture2D = 3553,
        //
        // Сводка:
        //     Original was GL_TEXTURE_3D = 0x806F
        Texture3D = 32879,
        //
        // Сводка:
        //     Original was GL_TEXTURE_RECTANGLE = 0x84F5
        TextureRectangle = 34037,
        //
        // Сводка:
        //     Original was GL_TEXTURE_CUBE_MAP = 0x8513
        TextureCubeMap = 34067,
        //
        // Сводка:
        //     Original was GL_TEXTURE_1D_ARRAY = 0x8C18
        Texture1DArray = 35864,
        //
        // Сводка:
        //     Original was GL_TEXTURE_2D_ARRAY = 0x8C1A
        Texture2DArray = 35866,
        //
        // Сводка:
        //     Original was GL_TEXTURE_BUFFER = 0x8C2A
        TextureBuffer = 35882,
        //
        // Сводка:
        //     Original was GL_RENDERBUFFER = 0x8D41
        Renderbuffer = 36161,
        //
        // Сводка:
        //     Original was GL_TEXTURE_CUBE_MAP_ARRAY = 0x9009
        TextureCubeMapArray = 36873,
        //
        // Сводка:
        //     Original was GL_TEXTURE_2D_MULTISAMPLE = 0x9100
        Texture2DMultisample = 37120,
        //
        // Сводка:
        //     Original was GL_TEXTURE_2D_MULTISAMPLE_ARRAY = 0x9102
        Texture2DMultisampleArray = 37122
    }
}

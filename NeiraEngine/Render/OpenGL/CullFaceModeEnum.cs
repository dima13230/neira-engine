using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiraEngine.Render.OpenGL
{
    public enum CullFaceMode
    {
        //
        // Сводка:
        //     Original was GL_FRONT = 0x0404
        Front = 1028,
        //
        // Сводка:
        //     Original was GL_BACK = 0x0405
        Back = 1029,
        //
        // Сводка:
        //     Original was GL_FRONT_AND_BACK = 0x0408
        FrontAndBack = 1032
    }
}

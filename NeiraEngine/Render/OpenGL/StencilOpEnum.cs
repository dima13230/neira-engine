using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiraEngine.Render.OpenGL
{
    public enum StencilOp
    {
        //
        // Сводка:
        //     Original was GL_ZERO = 0
        Zero = 0,
        //
        // Сводка:
        //     Original was GL_INVERT = 0x150A
        Invert = 5386,
        //
        // Сводка:
        //     Original was GL_KEEP = 0x1E00
        Keep = 7680,
        //
        // Сводка:
        //     Original was GL_REPLACE = 0x1E01
        Replace = 7681,
        //
        // Сводка:
        //     Original was GL_INCR = 0x1E02
        Incr = 7682,
        //
        // Сводка:
        //     Original was GL_DECR = 0x1E03
        Decr = 7683,
        //
        // Сводка:
        //     Original was GL_INCR_WRAP = 0x8507
        IncrWrap = 34055,
        //
        // Сводка:
        //     Original was GL_DECR_WRAP = 0x8508
        DecrWrap = 34056
    }
}

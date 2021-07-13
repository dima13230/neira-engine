using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiraEngine.Render.OpenGL
{
    public enum BlendEquationMode
    {
        //
        // Сводка:
        //     Original was GL_FUNC_ADD = 0x8006
        FuncAdd = 32774,
        //
        // Сводка:
        //     Original was GL_MIN = 0x8007
        Min = 32775,
        //
        // Сводка:
        //     Original was GL_MAX = 0x8008
        Max = 32776,
        //
        // Сводка:
        //     Original was GL_FUNC_SUBTRACT = 0x800A
        FuncSubtract = 32778,
        //
        // Сводка:
        //     Original was GL_FUNC_REVERSE_SUBTRACT = 0x800B
        FuncReverseSubtract = 32779
    }
}

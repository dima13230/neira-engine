using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiraEngine.Render.OpenGL
{
    public enum StencilFunction
    {
        //
        // Сводка:
        //     Original was GL_NEVER = 0x0200
        Never = 512,
        //
        // Сводка:
        //     Original was GL_LESS = 0x0201
        Less = 513,
        //
        // Сводка:
        //     Original was GL_EQUAL = 0x0202
        Equal = 514,
        //
        // Сводка:
        //     Original was GL_LEQUAL = 0x0203
        Lequal = 515,
        //
        // Сводка:
        //     Original was GL_GREATER = 0x0204
        Greater = 516,
        //
        // Сводка:
        //     Original was GL_NOTEQUAL = 0x0205
        Notequal = 517,
        //
        // Сводка:
        //     Original was GL_GEQUAL = 0x0206
        Gequal = 518,
        //
        // Сводка:
        //     Original was GL_ALWAYS = 0x0207
        Always = 519
    }
}

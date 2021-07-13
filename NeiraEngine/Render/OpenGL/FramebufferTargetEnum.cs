using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiraEngine.Render.OpenGL
{
    public enum FramebufferTarget
    {
        //
        // Сводка:
        //     Original was GL_READ_FRAMEBUFFER = 0x8CA8
        ReadFramebuffer = 36008,
        //
        // Сводка:
        //     Original was GL_DRAW_FRAMEBUFFER = 0x8CA9
        DrawFramebuffer = 36009,
        //
        // Сводка:
        //     Original was GL_FRAMEBUFFER = 0x8D40
        Framebuffer = 36160,
        //
        // Сводка:
        //     Original was GL_FRAMEBUFFER_EXT = 0x8D40
        FramebufferExt = 36160
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiraEngine.Render.OpenGL
{
    public enum BlendingFactor
    {
        //
        // Сводка:
        //     Original was GL_ZERO = 0
        Zero = 0,
        //
        // Сводка:
        //     Original was GL_ONE = 1
        One = 1,
        //
        // Сводка:
        //     Original was GL_SRC_COLOR = 0x0300
        SrcColor = 768,
        //
        // Сводка:
        //     Original was GL_ONE_MINUS_SRC_COLOR = 0x0301
        OneMinusSrcColor = 769,
        //
        // Сводка:
        //     Original was GL_SRC_ALPHA = 0x0302
        SrcAlpha = 770,
        //
        // Сводка:
        //     Original was GL_ONE_MINUS_SRC_ALPHA = 0x0303
        OneMinusSrcAlpha = 771,
        //
        // Сводка:
        //     Original was GL_DST_ALPHA = 0x0304
        DstAlpha = 772,
        //
        // Сводка:
        //     Original was GL_ONE_MINUS_DST_ALPHA = 0x0305
        OneMinusDstAlpha = 773,
        //
        // Сводка:
        //     Original was GL_DST_COLOR = 0x0306
        DstColor = 774,
        //
        // Сводка:
        //     Original was GL_ONE_MINUS_DST_COLOR = 0x0307
        OneMinusDstColor = 775,
        //
        // Сводка:
        //     Original was GL_SRC_ALPHA_SATURATE = 0x0308
        SrcAlphaSaturate = 776,
        //
        // Сводка:
        //     Original was GL_CONSTANT_COLOR = 0x8001
        ConstantColor = 32769,
        //
        // Сводка:
        //     Original was GL_ONE_MINUS_CONSTANT_COLOR = 0x8002
        OneMinusConstantColor = 32770,
        //
        // Сводка:
        //     Original was GL_CONSTANT_ALPHA = 0x8003
        ConstantAlpha = 32771,
        //
        // Сводка:
        //     Original was GL_ONE_MINUS_CONSTANT_ALPHA = 0x8004
        OneMinusConstantAlpha = 32772,
        //
        // Сводка:
        //     Original was GL_SRC1_ALPHA = 0x8589
        Src1Alpha = 34185,
        //
        // Сводка:
        //     Original was GL_SRC1_COLOR = 0x88F9
        Src1Color = 35065
    }
}

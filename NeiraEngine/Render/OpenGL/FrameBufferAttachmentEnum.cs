using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiraEngine.Render.OpenGL
{
    public enum FramebufferAttachment
    {
        //
        // Сводка:
        //     Original was GL_FRONT_LEFT = 0x0400
        FrontLeft = 1024,
        //
        // Сводка:
        //     Original was GL_FRONT_RIGHT = 0x0401
        FrontRight = 1025,
        //
        // Сводка:
        //     Original was GL_BACK_LEFT = 0x0402
        BackLeft = 1026,
        //
        // Сводка:
        //     Original was GL_BACK_RIGHT = 0x0403
        BackRight = 1027,
        //
        // Сводка:
        //     Original was GL_AUX0 = 0x0409
        Aux0 = 1033,
        //
        // Сводка:
        //     Original was GL_AUX1 = 0x040A
        Aux1 = 1034,
        //
        // Сводка:
        //     Original was GL_AUX2 = 0x040B
        Aux2 = 1035,
        //
        // Сводка:
        //     Original was GL_AUX3 = 0x040C
        Aux3 = 1036,
        //
        // Сводка:
        //     Original was GL_COLOR = 0x1800
        Color = 6144,
        //
        // Сводка:
        //     Original was GL_DEPTH = 0x1801
        Depth = 6145,
        //
        // Сводка:
        //     Original was GL_STENCIL = 0x1802
        Stencil = 6146,
        //
        // Сводка:
        //     Original was GL_DEPTH_STENCIL_ATTACHMENT = 0x821A
        DepthStencilAttachment = 33306,
        //
        // Сводка:
        //     Original was GL_COLOR_ATTACHMENT0 = 0x8CE0
        ColorAttachment0 = 36064,
        //
        // Сводка:
        //     Original was GL_COLOR_ATTACHMENT0_EXT = 0x8CE0
        ColorAttachment0Ext = 36064,
        //
        // Сводка:
        //     Original was GL_COLOR_ATTACHMENT1 = 0x8CE1
        ColorAttachment1 = 36065,
        //
        // Сводка:
        //     Original was GL_COLOR_ATTACHMENT1_EXT = 0x8CE1
        ColorAttachment1Ext = 36065,
        //
        // Сводка:
        //     Original was GL_COLOR_ATTACHMENT2 = 0x8CE2
        ColorAttachment2 = 36066,
        //
        // Сводка:
        //     Original was GL_COLOR_ATTACHMENT2_EXT = 0x8CE2
        ColorAttachment2Ext = 36066,
        //
        // Сводка:
        //     Original was GL_COLOR_ATTACHMENT3 = 0x8CE3
        ColorAttachment3 = 36067,
        //
        // Сводка:
        //     Original was GL_COLOR_ATTACHMENT3_EXT = 0x8CE3
        ColorAttachment3Ext = 36067,
        //
        // Сводка:
        //     Original was GL_COLOR_ATTACHMENT4 = 0x8CE4
        ColorAttachment4 = 36068,
        //
        // Сводка:
        //     Original was GL_COLOR_ATTACHMENT4_EXT = 0x8CE4
        ColorAttachment4Ext = 36068,
        //
        // Сводка:
        //     Original was GL_COLOR_ATTACHMENT5 = 0x8CE5
        ColorAttachment5 = 36069,
        //
        // Сводка:
        //     Original was GL_COLOR_ATTACHMENT5_EXT = 0x8CE5
        ColorAttachment5Ext = 36069,
        //
        // Сводка:
        //     Original was GL_COLOR_ATTACHMENT6 = 0x8CE6
        ColorAttachment6 = 36070,
        //
        // Сводка:
        //     Original was GL_COLOR_ATTACHMENT6_EXT = 0x8CE6
        ColorAttachment6Ext = 36070,
        //
        // Сводка:
        //     Original was GL_COLOR_ATTACHMENT7 = 0x8CE7
        ColorAttachment7 = 36071,
        //
        // Сводка:
        //     Original was GL_COLOR_ATTACHMENT7_EXT = 0x8CE7
        ColorAttachment7Ext = 36071,
        //
        // Сводка:
        //     Original was GL_COLOR_ATTACHMENT8 = 0x8CE8
        ColorAttachment8 = 36072,
        //
        // Сводка:
        //     Original was GL_COLOR_ATTACHMENT8_EXT = 0x8CE8
        ColorAttachment8Ext = 36072,
        //
        // Сводка:
        //     Original was GL_COLOR_ATTACHMENT9 = 0x8CE9
        ColorAttachment9 = 36073,
        //
        // Сводка:
        //     Original was GL_COLOR_ATTACHMENT9_EXT = 0x8CE9
        ColorAttachment9Ext = 36073,
        //
        // Сводка:
        //     Original was GL_COLOR_ATTACHMENT10 = 0x8CEA
        ColorAttachment10 = 36074,
        //
        // Сводка:
        //     Original was GL_COLOR_ATTACHMENT10_EXT = 0x8CEA
        ColorAttachment10Ext = 36074,
        //
        // Сводка:
        //     Original was GL_COLOR_ATTACHMENT11 = 0x8CEB
        ColorAttachment11 = 36075,
        //
        // Сводка:
        //     Original was GL_COLOR_ATTACHMENT11_EXT = 0x8CEB
        ColorAttachment11Ext = 36075,
        //
        // Сводка:
        //     Original was GL_COLOR_ATTACHMENT12 = 0x8CEC
        ColorAttachment12 = 36076,
        //
        // Сводка:
        //     Original was GL_COLOR_ATTACHMENT12_EXT = 0x8CEC
        ColorAttachment12Ext = 36076,
        //
        // Сводка:
        //     Original was GL_COLOR_ATTACHMENT13 = 0x8CED
        ColorAttachment13 = 36077,
        //
        // Сводка:
        //     Original was GL_COLOR_ATTACHMENT13_EXT = 0x8CED
        ColorAttachment13Ext = 36077,
        //
        // Сводка:
        //     Original was GL_COLOR_ATTACHMENT14 = 0x8CEE
        ColorAttachment14 = 36078,
        //
        // Сводка:
        //     Original was GL_COLOR_ATTACHMENT14_EXT = 0x8CEE
        ColorAttachment14Ext = 36078,
        //
        // Сводка:
        //     Original was GL_COLOR_ATTACHMENT15 = 0x8CEF
        ColorAttachment15 = 36079,
        //
        // Сводка:
        //     Original was GL_COLOR_ATTACHMENT15_EXT = 0x8CEF
        ColorAttachment15Ext = 36079,
        //
        // Сводка:
        //     Original was GL_DEPTH_ATTACHMENT = 0x8D00
        DepthAttachment = 36096,
        //
        // Сводка:
        //     Original was GL_DEPTH_ATTACHMENT_EXT = 0x8D00
        DepthAttachmentExt = 36096,
        //
        // Сводка:
        //     Original was GL_STENCIL_ATTACHMENT = 0x8D20
        StencilAttachment = 36128,
        //
        // Сводка:
        //     Original was GL_STENCIL_ATTACHMENT_EXT = 0x8D20
        StencilAttachmentExt = 36128
    }
}

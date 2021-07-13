using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiraEngine.Render.OpenGL
{
    public enum MemoryBarrierFlags
    {
        //
        // Сводка:
        //     Original was GL_ALL_BARRIER_BITS = 0xFFFFFFFF
        AllBarrierBits = -1,
        //
        // Сводка:
        //     Original was GL_VERTEX_ATTRIB_ARRAY_BARRIER_BIT = 0x00000001
        VertexAttribArrayBarrierBit = 1,
        //
        // Сводка:
        //     Original was GL_ELEMENT_ARRAY_BARRIER_BIT = 0x00000002
        ElementArrayBarrierBit = 2,
        //
        // Сводка:
        //     Original was GL_UNIFORM_BARRIER_BIT = 0x00000004
        UniformBarrierBit = 4,
        //
        // Сводка:
        //     Original was GL_TEXTURE_FETCH_BARRIER_BIT = 0x00000008
        TextureFetchBarrierBit = 8,
        //
        // Сводка:
        //     Original was GL_SHADER_IMAGE_ACCESS_BARRIER_BIT = 0x00000020
        ShaderImageAccessBarrierBit = 32,
        //
        // Сводка:
        //     Original was GL_COMMAND_BARRIER_BIT = 0x00000040
        CommandBarrierBit = 64,
        //
        // Сводка:
        //     Original was GL_PIXEL_BUFFER_BARRIER_BIT = 0x00000080
        PixelBufferBarrierBit = 128,
        //
        // Сводка:
        //     Original was GL_TEXTURE_UPDATE_BARRIER_BIT = 0x00000100
        TextureUpdateBarrierBit = 256,
        //
        // Сводка:
        //     Original was GL_BUFFER_UPDATE_BARRIER_BIT = 0x00000200
        BufferUpdateBarrierBit = 512,
        //
        // Сводка:
        //     Original was GL_FRAMEBUFFER_BARRIER_BIT = 0x00000400
        FramebufferBarrierBit = 1024,
        //
        // Сводка:
        //     Original was GL_TRANSFORM_FEEDBACK_BARRIER_BIT = 0x00000800
        TransformFeedbackBarrierBit = 2048,
        //
        // Сводка:
        //     Original was GL_ATOMIC_COUNTER_BARRIER_BIT = 0x00001000
        AtomicCounterBarrierBit = 4096,
        //
        // Сводка:
        //     Original was GL_SHADER_STORAGE_BARRIER_BIT = 0x00002000
        ShaderStorageBarrierBit = 8192,
        //
        // Сводка:
        //     Original was GL_CLIENT_MAPPED_BUFFER_BARRIER_BIT = 0x00004000
        ClientMappedBufferBarrierBit = 16384,
        //
        // Сводка:
        //     Original was GL_QUERY_BUFFER_BARRIER_BIT = 0x00008000
        QueryBufferBarrierBit = 32768
    }
}

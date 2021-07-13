using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiraEngine.Render.OpenGL
{
    public enum BufferRangeTarget
    {
        //
        // Сводка:
        //     Original was GL_UNIFORM_BUFFER = 0x8A11
        UniformBuffer = 35345,
        //
        // Сводка:
        //     Original was GL_TRANSFORM_FEEDBACK_BUFFER = 0x8C8E
        TransformFeedbackBuffer = 35982,
        //
        // Сводка:
        //     Original was GL_SHADER_STORAGE_BUFFER = 0x90D2
        ShaderStorageBuffer = 37074,
        //
        // Сводка:
        //     Original was GL_ATOMIC_COUNTER_BUFFER = 0x92C0
        AtomicCounterBuffer = 37568
    }
}

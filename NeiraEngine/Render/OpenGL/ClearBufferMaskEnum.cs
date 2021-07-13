using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiraEngine.Render.OpenGL
{
    [Flags]
    public enum ClearBufferMask
    {
        None = 0,
        DepthBufferBit = 256,
        AccumBufferBit = 512,
        StencilBufferBit = 1024,
        ColorBufferBit = 16384,
        CoverageBufferBitNv = 32768
    }
}

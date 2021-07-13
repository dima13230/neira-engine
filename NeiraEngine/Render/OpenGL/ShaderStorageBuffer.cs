using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Graphics.OpenGL;

namespace NeiraEngine.Render.OpenGL
{
    public class ShaderStorageBuffer
    {

        private int _id;
        public int id
        {
            get { return _id; }
        }

        private EngineHelper.size[] _ssbo_stack;

        public ShaderStorageBuffer(EngineHelper.size[] ssbo_stack)
        {
            _id = 0;

            // Calculate total SSBO byte size based on ssbo_stack items
            _ssbo_stack = ssbo_stack;
            int size = 0;
            foreach (EngineHelper.size e in _ssbo_stack)
            {
                size += (int)e;
            }

            // Create Uniform Buffer
            GL.GenBuffers(1, out _id);
            GL.BindBuffer(OpenTK.Graphics.OpenGL.BufferTarget.ShaderStorageBuffer, _id);
            GL.BufferData(OpenTK.Graphics.OpenGL.BufferTarget.ShaderStorageBuffer, size, (IntPtr)0, OpenTK.Graphics.OpenGL.BufferUsageHint.DynamicCopy);

            //Bind uniform buffer to binding index since the block size is set and ubo is created
            GL.BindBuffer(OpenTK.Graphics.OpenGL.BufferTarget.ShaderStorageBuffer, 0);
        }


        public void bind()
        {
            GL.BindBuffer(OpenTK.Graphics.OpenGL.BufferTarget.ShaderStorageBuffer, _id);
        }

        public void bind(int index)
        {
            GL.BindBufferBase((OpenTK.Graphics.OpenGL.BufferRangeTarget)BufferRangeTarget.ShaderStorageBuffer, index, _id);
        }

        public void unbind()
        {
            GL.BindBuffer(OpenTK.Graphics.OpenGL.BufferTarget.ShaderStorageBuffer, 0);
        }

        public void unbind(int index)
        {
            GL.BindBufferBase((OpenTK.Graphics.OpenGL.BufferRangeTarget)BufferRangeTarget.ShaderStorageBuffer, index, 0);
        }

    }
}

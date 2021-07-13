using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Gl = OpenTK.Graphics.OpenGL.GL;
using gl = OpenTK.Graphics.OpenGL;

using NeiraEngine.Output;

namespace NeiraEngine.Render.OpenGL
{
    public class OGL
    {

        #region Bind Buffer
        public static void BindBuffer(BufferTarget target, int buffer)
        {
            Gl.BindBuffer((gl.BufferTarget)target, buffer);
        }

        public static void BindBuffer(BufferTarget target, uint buffer)
        {
            Gl.BindBuffer((gl.BufferTarget)target, buffer);
        }
        #endregion

        public static void BindBufferBase(BufferRangeTarget target, int index, int buffer)
        {
            Gl.BindBufferBase((gl.BufferRangeTarget)target, index, buffer);
        }

        public static void BindBufferRange(BufferRangeTarget target, int index, int buffer, IntPtr offset, IntPtr size)
        {
            Gl.BindBufferRange((gl.BufferRangeTarget)target, index, buffer, offset, size);
        }

        #region Bind Framebuffer
        public static void BindFramebuffer(FramebufferTarget target, int framebuffer)
        {
            Gl.BindFramebuffer((gl.FramebufferTarget)target, framebuffer);
        }

        public static void BindFramebuffer(FramebufferTarget target, uint framebuffer)
        {
            Gl.BindFramebuffer((gl.FramebufferTarget)target, framebuffer);
        }
        #endregion

        #region Bind Vertex Array
        public static void BindVertexArray(int array)
        {
            Gl.BindVertexArray(array);
        }

        public static void BindVertexArray(uint array)
        {
            Gl.BindVertexArray(array);
        }
        #endregion

        public static void BlendEquation(BlendEquationMode mode)
        {
            Gl.BlendEquation((gl.BlendEquationMode)mode);
        }

        public static void BlendEquationSeparate(BlendEquationMode modeRGB, BlendEquationMode modeAlpha)
        {
            Gl.BlendEquationSeparate((gl.BlendEquationMode)modeRGB, (gl.BlendEquationMode)modeAlpha);
        }

        public static void BlendFunc(BlendingFactor src, BlendingFactor dest)
        {
            Gl.BlendFunc((gl.BlendingFactor)src, (gl.BlendingFactor)dest);
        }

        public static void BlendFuncSeparate(BlendingFactorSrc sfactorRGB, BlendingFactorDest dfactorRGB, BlendingFactorSrc sfactorAlpha, BlendingFactorDest dfactorAlpha)
        {
            Gl.BlendFuncSeparate((gl.BlendingFactorSrc)sfactorRGB, (gl.BlendingFactorDest)dfactorRGB, (gl.BlendingFactorSrc)sfactorAlpha, (gl.BlendingFactorDest)dfactorAlpha);
        }

        #region Buffer Data
        public static void BufferData(BufferTarget target, int size, IntPtr data, BufferUsageHint usage)
        {
            Gl.BufferData((gl.BufferTarget)target, size, data, (gl.BufferUsageHint)usage);
        }

        public static void BufferData(BufferTarget target, IntPtr size, float[] data, BufferUsageHint usage)
        {
            Gl.BufferData((gl.BufferTarget)target, size, data, (gl.BufferUsageHint)usage);
        }

        public static void BufferData(BufferTarget target, IntPtr size, IntPtr data, BufferUsageHint usage)
        {
            Gl.BufferData((gl.BufferTarget)target, size, data, (gl.BufferUsageHint)usage);
        }

        public static void BufferData<T>(BufferTarget target, int size, ref T data, BufferUsageHint usage) where T : struct
        {
            Gl.BufferData((gl.BufferTarget)target, size, ref data, (gl.BufferUsageHint)usage);
        }

        public static void BufferData<T>(BufferTarget target, IntPtr size, ref T data, BufferUsageHint usage) where T: struct
        {
            Gl.BufferData((gl.BufferTarget)target, size, ref data, (gl.BufferUsageHint)usage);
        }
        #endregion

        public static void Clear(ClearBufferMask mask)
        {
            Gl.Clear((gl.ClearBufferMask)mask);
        }

        public static void ClearColor(Color color)
        {
            Gl.ClearColor(color);
        }

        #region Color Mask
        public static void ColorMask(bool r, bool g, bool b, bool a)
        {
            Gl.ColorMask(r, g, b, a);
        }

        public static void ColorMask(int index, bool r, bool g, bool b, bool a)
        {
            Gl.ColorMask(index, r, g, b, a);
        }
        #endregion

        public static void CopyImageSubData(int srcId, ImageTarget srcTarget, int srcLevel, int srcX, int srcY, int srcZ, int distName, ImageTarget distTarget, int distLevel, int distX, int distY, int distZ, int distWidth, int distHeight, int srcDepth)
        {
            Gl.CopyImageSubData(srcId, (gl.ImageTarget)srcTarget, srcLevel, srcX, srcY, srcZ, distName, (gl.ImageTarget)distTarget, distLevel, distX, distY, distZ, distWidth, distHeight, srcDepth);
        }

        public static void CullFace(CullFaceMode mode)
        {
            Gl.CullFace((gl.CullFaceMode)mode);
        }

        public static void DepthFunc(DepthFunction func)
        {
            Gl.DepthFunc((gl.DepthFunction)func);
        }

        public static void DepthMask(bool flag)
        {
            Gl.DepthMask(flag);
        }

        public static void DepthRange(float near, float far)
        {
            Gl.DepthRange(near, far);
        }

        public static void DrawArrays(PrimitiveType mode, int first, int count)
        {
            Gl.DrawArrays((gl.PrimitiveType)mode, first, count);
        }

        public static void DrawArraysInstanced(PrimitiveType mode, int first, int count, int instancecount)
        {
            Gl.DrawArraysInstanced((gl.PrimitiveType)mode, first, count, instancecount);
        }

        public static void DrawArraysIndirect(PrimitiveType type, IntPtr indirect)
        {
            Gl.DrawArraysIndirect((gl.PrimitiveType)type, indirect);
        }

        public static void FrontFace(FrontFaceDirection mode)
        {
            Gl.FrontFace((gl.FrontFaceDirection)mode);
        }

        #region Dispatch Compute
        public static void DispatchCompute(int x, int y, int z)
        {
            Gl.DispatchCompute(x, y, z);
        }

        public static void DispatchCompute(uint x, uint y, uint z)
        {
            Gl.DispatchCompute(x, y, z);
        }
        #endregion

        public static void Enable(EnableCap cap)
        {
            Gl.Enable((gl.EnableCap)cap);
        }

        public static void EnableVertexAttribArray(int index)
        {
            Gl.EnableVertexAttribArray(index);
        }

        public static void Disable(EnableCap cap)
        {
            Gl.Disable((gl.EnableCap)cap);
        }

        public static void Finish()
        {
            Gl.Finish();
        }

        #region Gen Buffers
        public static void GenBuffers(int n, out int buffers)
        {
            Gl.GenBuffers(n, out buffers);
        }

        public static void GenBuffers(int n, int[] buffers)
        {
            Gl.GenBuffers(n, buffers);
        }

        public static void GenBuffers(int n, out uint buffers)
        {
            Gl.GenBuffers(n, out buffers);
        }

        public static void GenBuffers(int n, uint[] buffers)
        {
            Gl.GenBuffers(n, buffers);
        }
        #endregion

        #region Gen Vertex Arrays
        public static void GenVertexArrays(int n, out int vao)
        {
            Gl.GenVertexArrays(n, out vao);
        }

        public static void GenVertexArrays(int n, int[] vao)
        {
            Gl.GenVertexArrays(n, vao);
        }

        public static void GenVertexArrays(int n, out uint vao)
        {
            Gl.GenVertexArrays(n, out vao);
        }

        public static void GenVertexArrays(int n, uint[] vao)
        {
            Gl.GenVertexArrays(n, vao);
        }
        #endregion

        public static void GetBufferSubData<T>(BufferTarget target, IntPtr offset, int size, ref T data) where T: struct
        {
            Gl.GetBufferSubData((gl.BufferTarget)target, offset, size, ref data);
        }

        public static void MemoryBarrier(MemoryBarrierFlags flag)
        {
            Gl.MemoryBarrier((gl.MemoryBarrierFlags)flag);
        }

        public static void ReadPixels(int x, int y, int width, int height, PixelFormat format, PixelType type, IntPtr pixels)
        {
            Gl.ReadPixels(x, y, width, height, (gl.PixelFormat)format, (gl.PixelType)type, pixels);
        }

        #region Uniform
        public static void Uniform(int uniformID, float value)
        {
            Gl.Uniform1(uniformID, value);
        }
        public static void Uniform(int uniformID, int value)
        {
            Gl.Uniform1(uniformID, value);
        }
        public static void Uniform(int uniformID, double value)
        {
            Gl.Uniform1(uniformID, value);
        }
        public static void Uniform(int uniformID, uint value)
        {
            Gl.Uniform1(uniformID, value);
        }


        public static void Uniform(int uniformID, Vector2 value)
        {
            OpenTK.Vector2 v = value;
            Gl.Uniform2(uniformID, ref v);
        }
        public static void Uniform(int uniformID, int v0, int v1)
        {
            Gl.Uniform2(uniformID, v0, v1);
        }
        public static void Uniform(int uniformID, float v0, float v1)
        {
            Gl.Uniform2(uniformID, v0, v1);
        }
        public static void Uniform(int uniformID, double v0, double v1)
        {
            Gl.Uniform2(uniformID, v0, v1);
        }



        public static void Uniform(int uniformID, Vector3 value)
        {
            OpenTK.Vector3 v = value;
            Gl.Uniform3(uniformID, ref v);
        }
        public static void Uniform(int uniformID, int v0, int v1, int v2)
        {
            Gl.Uniform3(uniformID, v0, v1, v2);
        }
        public static void Uniform(int uniformID, float v0, float v1, float v2)
        {
            Gl.Uniform3(uniformID, v0, v1, v2);
        }
        public static void Uniform(int uniformID, double v0, double v1, double v2)
        {
            Gl.Uniform3(uniformID, v0, v1, v2);
        }



        public static void Uniform(int uniformID, Vector4 value)
        {
            OpenTK.Vector4 v = value;
            Gl.Uniform4(uniformID, ref v);
        }
        public static void Uniform(int uniformID, int v0, int v1, int v2, int v3)
        {
            Gl.Uniform4(uniformID, v0, v1, v2, v3);
        }
        public static void Uniform(int uniformID, float v0, float v1, float v2, float v3)
        {
            Gl.Uniform4(uniformID, v0, v1, v2, v3);
        }
        public static void Uniform(int uniformID, double v0, double v1, double v2, double v3)
        {
            Gl.Uniform4(uniformID, v0, v1, v2, v3);
        }



        public static void Uniform(int uniformID, bool transpose, Matrix4 value)
        {
            OpenTK.Matrix4 v = value;
            Gl.UniformMatrix4(uniformID, transpose, ref v);
        }
        public static void Uniform(int uniformID, bool transpose, Matrix4x2 value)
        {
            OpenTK.Matrix4x2 v = value;
            Gl.UniformMatrix4x2(uniformID, transpose, ref v);
        }
        public static void Uniform(int uniformID, bool transpose, Matrix4x3 value)
        {
            OpenTK.Matrix4x3 v = value;
            Gl.UniformMatrix4x3(uniformID, transpose, ref v);
        }

        public static void Uniform(int uniformID, bool transpose, Matrix3 value)
        {
            OpenTK.Matrix3 v = value;
            Gl.UniformMatrix3(uniformID, transpose, ref v);
        }
        public static void Uniform(int uniformID, bool transpose, Matrix3x2 value)
        {
            OpenTK.Matrix3x2 v = value;
            Gl.UniformMatrix3x2(uniformID, transpose, ref v);
        }
        public static void Uniform(int uniformID, bool transpose, Matrix3x4 value)
        {
            OpenTK.Matrix3x4 v = value;
            Gl.UniformMatrix3x4(uniformID, transpose, ref v);
        }

        public static void Uniform(int uniformID, bool transpose, Matrix2 value)
        {
            OpenTK.Matrix2 v = value;
            Gl.UniformMatrix2(uniformID, transpose, ref v);
        }
        public static void Uniform(int uniformID, bool transpose, Matrix2x3 value)
        {
            OpenTK.Matrix2x3 v = value;
            Gl.UniformMatrix2x3(uniformID, transpose, ref v);
        }
        public static void Uniform(int uniformID, bool transpose, Matrix2x4 value)
        {
            OpenTK.Matrix2x4 v = value;
            Gl.UniformMatrix2x4(uniformID, transpose, ref v);
        }
        #endregion

        public static void VertexAttribPointer(int index, int size, VertexAttribPointerType type, bool normalized, int stride, int offset)
        {
            Gl.VertexAttribPointer(index, size, (gl.VertexAttribPointerType)type, normalized, stride, offset);
        }

        public static void Viewport(int x, int y, int width, int height)
        {
            Gl.Viewport(x, y, width, height);
        }

        public static void StencilFunc(StencilFunction func, int @ref, int mask)
        {
            Gl.StencilFunc((gl.StencilFunction)func, @ref, mask);
        }

        public static void StencilFuncSeparate(StencilFace face, StencilFunction func, int @ref, int mask)
        {
            Gl.StencilFuncSeparate((gl.StencilFace)face, (gl.StencilFunction)func, @ref, mask);
        }

        public static void StencilOpSeparate(StencilFace face, StencilOp sfail, StencilOp dpfail, StencilOp dppass)
        {
            Gl.StencilOpSeparate((gl.StencilFace)face, (gl.StencilOp)sfail, (gl.StencilOp)dpfail, (gl.StencilOp)dppass);
        }

        // Utility

        private void CaptureScreenshot(string filename, Resolution resolution)
        {
                Bitmap bmp = new Bitmap(resolution.W, resolution.H);
                System.Drawing.Imaging.BitmapData data = bmp.LockBits(
                    new Rectangle(0, 0, resolution.W, resolution.H),
                    System.Drawing.Imaging.ImageLockMode.WriteOnly,
                    System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                ReadPixels(0, 0, resolution.W, resolution.H, PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
                Finish();
                bmp.UnlockBits(data);
                bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);

                string screenshotFilename = filename;

                bmp.Save(screenshotFilename, System.Drawing.Imaging.ImageFormat.Png);
                bmp.Dispose();

                Debug.logInfo(0, "[ INFO ] Screenshot Taken", "OK");
        }

    }
}
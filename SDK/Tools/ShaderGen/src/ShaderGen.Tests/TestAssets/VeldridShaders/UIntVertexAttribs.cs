using ShaderGen;
using System.Numerics;
using System.Runtime.InteropServices;

namespace TestShaders.VeldridShaders
{
    public class UIntVertexAttribs
    {
        public struct Vertex
        {
            [PositionSemantic]
            public Vector2 Position;
            [ColorSemantic]
            public UInt4 Color_Int;
        }

        public struct FragmentInput
        {
            [SystemPositionSemantic]
            public Vector4 Position;
            [ColorSemantic]
            public Vector4 Color;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Info
        {
            public uint ColorNormalizationFactor;
            private float _padding0;
            private float _padding1;
            private float _padding2;
        }

        public Info InfoBuffer;

        [VertexShader]
        public FragmentInput VS(Vertex input)
        {
            FragmentInput output;
            output.Position = new Vector4(input.Position, 0, 1);
            output.Color = new Vector4(input.Color_Int.X, input.Color_Int.Y, input.Color_Int.Z, input.Color_Int.W) / InfoBuffer.ColorNormalizationFactor;
            return output;
        }

        [FragmentShader]
        public Vector4 FS(FragmentInput input)
        {
            return input.Color;
        }
    }
}

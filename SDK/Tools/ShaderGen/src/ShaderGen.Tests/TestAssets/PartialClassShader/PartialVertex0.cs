using ShaderGen;
using System.Numerics;

namespace TestShaders
{
    public partial class PartialVertex
    {
        public struct VertexInput
        {
            [VertexSemantic(SemanticType.Position)] public Vector3 Position;
            [VertexSemantic(SemanticType.Color)] public Vector4 Color;
        }

        public Matrix4x4 First;
        public Matrix4x4 Second;
    }
}

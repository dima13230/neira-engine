layout (location = 0) uniform vec2 aPos;
layout (location = 1) uniform int layer;

uniform float scale;

void main()
{
	gl_Position = vec4(aPos, layer, 1.0);
	gl_PointSize = scale;
}

﻿
in vec2 v_TexCoord;

out vec4 color;

uniform sampler2D sampler0;		// Scene
uniform sampler2D sampler1;		// DOF Scene
uniform sampler2D sampler2;		// COC
uniform sampler2D sampler3;		// Bokeh


void main()
{
	vec3 scene = texture(sampler0, v_TexCoord).rgb;
	vec3 dof = texture(sampler1, v_TexCoord).rgb;
	float coc = texture(sampler2, v_TexCoord).r;
	vec3 bokeh = texture(sampler3, v_TexCoord).rgb;

	
	float lerp = clamp(coc * MATH_2_PI, 0.0, 1.0);

	vec3 final = mix(scene, dof + bokeh, lerp);

	color = vec4(final, 1.0);
}

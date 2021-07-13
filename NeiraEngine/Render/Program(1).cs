﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Graphics.OpenGL;

using NeiraEngine.Render.OpenGL;

namespace NeiraEngine.Render
{
    public class Program
    {
        public int pid { get; set; }

        public int glsl_version { get; set; }

        private Dictionary<string, int> _uniforms;

        public int[] compute_workgroup_size { get; private set; }

        public Program(int glsl_version, ShaderFile[] shader_pipeline)
        {
            this.glsl_version = glsl_version;
            _uniforms = new Dictionary<string, int>();

            try
            {
                createProgram(shader_pipeline);
            }
            catch (Exception e)
            {
                Debug.logError("[ ERROR ] Program Linking", "FAILED\n" + e.Message);
            }
        }


        //------------------------------------------------------
        // Program Templating
        //------------------------------------------------------

        // Helper to enable material feature
        public void setUniform1(string uniform_name, int enable_flag)
        {
            OGL.Uniform(getUniform(uniform_name), enable_flag);
        }

        // Setup program to load meshes
        public void enable_MeshLoading()
        {
            addUniform(RenderHelper.uModel);
            addUniform(RenderHelper.uModel_Normal);
            addUniform(RenderHelper.uModel_Previous);

            addUniform(RenderHelper.uEnableDiffuseTexture);
            addUniform(RenderHelper.uDiffuseTextureUnit);
            addUniform(RenderHelper.uDiffuseColor);   
            addUniform(RenderHelper.uEmission);

            addUniform(RenderHelper.uEnableSpecularTexture);
            addUniform(RenderHelper.uSpecularTextureUnit);
            addUniform(RenderHelper.uSpecularColor);
            addUniform(RenderHelper.uSpecularShininess);
            
            addUniform(RenderHelper.uEnableNormalTexture);
            addUniform(RenderHelper.uNormalTextureUnit);

            addUniform(RenderHelper.uEnableDisplacementTexture);
            addUniform(RenderHelper.uDisplacementTextureUnit);
            addUniform(RenderHelper.uDisplacementStrength);

            addUniform(RenderHelper.uEnableParallaxTexture);
            addUniform(RenderHelper.uParallaxTextureUnit);

            addUniform(RenderHelper.uEnableSkinning);
            addUniform(RenderHelper.uBoneMatrices);
        }

        // Setup program to calculate lighting
        public void enable_LightCalculation()
        {
            addUniform(RenderHelper.uLightPosition);
            addUniform(RenderHelper.uLightDirection);
            addUniform(RenderHelper.uLightColor);
            addUniform(RenderHelper.uLightIntensity);
            addUniform(RenderHelper.uLightFalloff);
            addUniform(RenderHelper.uLightSpotAngle);
            addUniform(RenderHelper.uLightSpotBlur);
        }

        // Setup sampler uniforms for textures
        public void enable_Samplers(int num_samplers)
        {
            for (int i = 0; i < num_samplers; i ++)
            {
                addUniform(RenderHelper.uSamplerBase + i);
            }
        }


        //------------------------------------------------------
        // Program Helpers
        //------------------------------------------------------

        public void bind()
        {
            GL.UseProgram(pid);
        }


        // Add uniform to the uniforms dictionary
        public void addUniform(string uniform_name)
        {
            int temp = GL.GetUniformLocation(pid, uniform_name);
            _uniforms.Add(uniform_name, temp);
        }

        // Retrieve uniform from the uniforms dictionary if it exists
        public int getUniform(string uniform_name)
        {
            int temp_model_uniform = -1;
            if (_uniforms.TryGetValue(uniform_name, out temp_model_uniform))
            {
                return temp_model_uniform;
            }
            else
            {
                Debug.logError("Mising Uniform: ", "\"" + uniform_name + "\"");
                return -1;
            }
            //return _uniforms[uniform_name];
        }

        // Get uniform for texture/image samplers
        public int getSamplerUniform(int sampler_index)
        {
            return getUniform("sampler" + sampler_index);
        }

        //------------------------------------------------------
        // Program Creation
        //------------------------------------------------------

        // Loads the shader pipepine into a program
        private void createProgram(ShaderFile[] shaders)
        {
            pid = GL.CreateProgram();
            bool is_compute = false;

            foreach (ShaderFile sf in shaders)
            {
                if (sf.type == ShaderType.ComputeShader) is_compute = true;
                int shader_id = sf.gl_compile(glsl_version);
                if(shader_id == 0)
                {
                    throw new OpenTK.GraphicsException("Shader in pipeline failed to compile :<");
                }
                GL.AttachShader(pid, shader_id);
            }

            GL.LinkProgram(pid);


            int max_error_length = 2048;
            StringBuilder error_text = new StringBuilder("", max_error_length);
            int error_length;
            GL.GetProgramInfoLog(pid, max_error_length, out error_length, error_text);


            string log_name = " Program Linking";

            if (error_length > 11)
            {
                Debug.logError("[ ERROR ]" + log_name, "FAILED\n" + error_text);
            }
            else
            {
                Debug.logInfo(2, "[ INFO ]" + log_name, "SUCCESS");

                // Load info from shaders
                compute_workgroup_size = new int[3];
                if(is_compute) GL.GetProgram(pid, (GetProgramParameterName)All.ComputeWorkGroupSize, compute_workgroup_size);
            }

        }


    }
}

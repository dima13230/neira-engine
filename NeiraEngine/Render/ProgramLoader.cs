using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;   

namespace NeiraEngine.Render
{
    public static class ProgramLoader
    {
        private static int _glsl_version;

        public static void Init(int glsl_version)
        {
            _glsl_version = glsl_version;
        }

        //------------------------------------------------------
        // Loaders
        //------------------------------------------------------

        public static Program createProgram(ShaderFile[] shader_pipeline)
        {
            return createProgram(_glsl_version, shader_pipeline);
        }

        public static Program createProgram(int glsl_version, ShaderFile[] shader_pipeline)
        {
            for(int i = 0; i < shader_pipeline.Length; i++)
            {
                shader_pipeline[i].base_path = EngineHelper.path_glsl_base;
            }

            return new Program(glsl_version, shader_pipeline);
        }

        //------------------------------------------------------
        // Templated Loaders
        //------------------------------------------------------

        public static Program createProgram_PostProcessing(ShaderFile[] shader_pipeline)
        {
            ShaderFile[] new_shader_pipline = new ShaderFile[shader_pipeline.Length + 1];

            new_shader_pipline[0] = new ShaderFile(ShaderType.VertexShader, EngineHelper.path_glsl_common_generic_vs, null);
            shader_pipeline.CopyTo(new_shader_pipline, 1);

            return createProgram(_glsl_version, new_shader_pipline);
        }

        public static Program createProgram_Geometry(ShaderFile[] shader_pipeline)
        {
            ShaderFile[] new_shader_pipline = new ShaderFile[shader_pipeline.Length + 1];

            new_shader_pipline[0] = new ShaderFile(ShaderType.VertexShader, EngineHelper.path_glsl_common_generic_geometry, null);
            shader_pipeline.CopyTo(new_shader_pipline, 1);

            return createProgram(_glsl_version, new_shader_pipline);
        }
    }
}
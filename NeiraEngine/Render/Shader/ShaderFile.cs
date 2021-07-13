using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using OpenTK.Graphics.OpenGL;

namespace NeiraEngine.Render
{
    public class ShaderFile
    {
        public ShaderType type { get; set; }

        public string base_path { get; set; }

        public string filename { get; set; }

        public string[] dependencies { get; set; }

        public string[] extensions { get; set; }


        public ShaderFile(ShaderType type, string filename, string[] dependencies)
        {
            this.type = type;
            this.filename = filename;
            this.dependencies = dependencies;
        }

        public ShaderFile(ShaderType type, string filename, string[] dependencies, string[] extensions)
        {
            this.type = type;
            this.filename = filename;
            this.dependencies = dependencies;
            this.extensions = extensions;
        }

        private string loadShaderFile(string filename)
        {
            try
            {
                StreamReader sr = new StreamReader(base_path + filename);
                string shader = sr.ReadToEnd();
                sr.Close();
                return shader;
            }
            catch (Exception e)
            {

                Debug.logError("Shader file not found!?", filename + "\n" + e.Message);
                return null;
            }
        }


        public int gl_compile(int glsl_version)
        {
            return gl_compile(glsl_version, "");
        }

        public int gl_compile(int glsl_version, string extensions)
        {
            int shader_id = GL.CreateShader((OpenTK.Graphics.OpenGL.ShaderType)type);

            string shader_source = loadShaderFile(filename);
            string shader_additions = "";

            int added_line_count = 0;

            // Add any depenancies into the shader file
            if (!(dependencies == null))
            {
                foreach (string s in dependencies)
                {
                    string dependency = loadShaderFile(s);
                    //added_line_count += dependancy.Split('\n').Length;
                    shader_additions = dependency + "\n" + shader_additions;
                }
            }

            // Add any extensions to a variable and include below #version preprocessor
            string combined_extensions = "\n";
            if (!(this.extensions == null))
            {
                foreach (string extension in this.extensions)
                {
                    combined_extensions += extension + "\n";
                    //added_line_count += extension.Split('\n').Length;
                }
            }

            // Add glsl version and MATH_PI at top of shader file
            string MATH_PI = "#define MATH_PI 3.1415926535897932384626433832795";
            string MATH_HALF_PI = "#define MATH_HALF_PI 1.57079632679489661923132169163975";
            string MATH_2_PI = "#define MATH_2_PI 6.283185307179586476925286766559";

            shader_additions =
                "#version " + glsl_version + "\n" +
                combined_extensions + "\n" +
                MATH_PI + "\n" +
                MATH_HALF_PI + "\n" +
                MATH_2_PI + "\n" +
                shader_additions;

            added_line_count = shader_additions.Split('\n').Length;

            shader_source =
                shader_additions + "\n" + 
                shader_source;

            try
            {
                GL.ShaderSource(shader_id, shader_source);
                GL.CompileShader(shader_id);


                int max_error_length = 2048;
                //StringBuilder error_text = new StringBuilder("", max_error_length);
                string error_text;
                int error_length;
                GL.GetShaderInfoLog(shader_id, max_error_length, out error_length, out error_text);

                string shader_type_string;
                switch (type)
                {
                    case ShaderType.VertexShader: shader_type_string = "VS"; break;
                    case ShaderType.TessControlShader: shader_type_string = "TC"; break;
                    case ShaderType.TessEvaluationShader: shader_type_string = "TE"; break;
                    case ShaderType.GeometryShader: shader_type_string = "GS"; break;
                    case ShaderType.FragmentShader: shader_type_string = "FS"; break;
                    case ShaderType.ComputeShader: shader_type_string = "CS"; break;
                    default: shader_type_string = "SHADER"; break;
                }

                string log_name = shader_type_string + ": " + filename;
                if (error_length > 11)
                {
                    // Complicated mess to add included shader files line length to error line number
                    string error_text_final = "";
                    foreach (string error in error_text.Split('\n'))
                    {
                        Match error_lines = Regex.Match(error, "0\\((\\d+)\\)");
                        int line_number = 0;
                        try
                        {
                            line_number = int.Parse(error_lines.Groups[1].ToString());
                            line_number = line_number - added_line_count;
                        }
                        catch
                        {
                            error_text_final += error + "\n";
                            continue;
                        }

                        error_text_final += "0(" + line_number + ") / " + error + "\n";
                    }
                    Debug.logError(log_name, "FAILED\n" + error_text_final);
                    return 0;
                }
                else
                {
                    Debug.logInfo(1, log_name, "SUCCESS");
                    return shader_id;
                }

            }
            catch (Exception e)
            {
                Debug.logError("That shader is null", e.Message);
                return 0;
            }

        }


    }
}

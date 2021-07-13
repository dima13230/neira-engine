using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;

namespace AutoClassRecallGen
{
    class Program
    {
        static void Main(string[] args)
        {
            string MethodBlock(MethodInfo Method)
            {
                if (Method.IsStatic)
                {
                    bool isUnsafe = false;
                    string argsstr = "";
                    string universaltypesstr = "";
                    string noTypeArgsStr = "";
                    ParameterInfo[] parameters = Method.GetParameters();

                    List<string> universalTypes = new List<string>();
                    List<string> universalTypesNoSym = new List<string>();
                    List<string> strparams = new List<string>();
                    List<string> notypestrparams = new List<string>();

                    for (int i = 0; i < parameters.Length; i++)
                    {
                        string parTypeName = parameters[i].ParameterType.Name;
                        string universalType = parTypeName;
                        string universalTypeNoSym = universalType;
                        string parTypeFullName = parameters[i].ParameterType.FullName;
                        bool isOut = false;
                        bool isUniversal = false;

                        Match match = Regex.Match(parTypeName, @"T\d+");

                        if (parTypeName.Contains("&"))
                        {
                            parTypeName = "out " + parTypeName.Replace("&", "");
                            isOut = true;
                        }

                        if (parTypeName.Contains("*"))
                            isUnsafe = true;

                        if (match.Success)
                        {

                            if (isUnsafe)
                                universalTypeNoSym = universalType.Replace("*", "");

                            universalTypeNoSym = Regex.Replace(universalTypeNoSym, "\u005B[,]+\u005D", "");

                            isUniversal = true;
                            universalTypes.Add(universalType);
                            universalTypesNoSym.Add(universalTypeNoSym);
                        }

                        if (parameters[i].ParameterType.Namespace == "System" || parameters[i].ParameterType.Namespace == "System.Collection.Generic" || parameters[i].ParameterType.Namespace == "System.Linq" || parameters[i].ParameterType.Namespace == "System.Text" || parameters[i].ParameterType.Namespace == "System.Threading.Tasks")
                        {
                            strparams.Add(parTypeName + " " + parameters[i].Name.Replace("params", "@params"));
                        }
                        else
                        {
                            if(!isUniversal)
                                strparams.Add(parTypeFullName + " " + parameters[i].Name.Replace("params", "@params"));
                            else
                                strparams.Add(universalType + " " + parameters[i].Name.Replace("params", "@params"));
                        }

                        notypestrparams.Add(isOut ? "out " + parameters[i].Name.Replace("params", "@params") : parameters[i].Name.Replace("params", "@params"));
                    }

                    argsstr = string.Join(",", strparams);
                    noTypeArgsStr = string.Join(",", notypestrparams);
                    if(universalTypes.Count > 0)
                        universaltypesstr = $"<{string.Join(",", universalTypesNoSym)}>";

                    string methodDefinitions = "public static";

                    if (isUnsafe)
                        methodDefinitions += " unsafe";

                    return Method.ReturnType != typeof(void)
                        ? $"    {methodDefinitions} {Method.ReturnType.Name} {Method.Name}{universaltypesstr}( {argsstr} )\n    {{\n        return {args[1]}. {Method.Name}{universaltypesstr}( {noTypeArgsStr} );\n    }}"
                        : $"    {methodDefinitions} void {Method.Name}{universaltypesstr}( {argsstr} )\n    {{\n        {args[1]}.{Method.Name}{universaltypesstr}( {noTypeArgsStr} );\n    }}";
                }
                else
                {
                    return "";
                }
            }

            if (args.Length == 3)
            {
                Assembly assembly = Assembly.LoadFile(Path.GetFullPath(args[0]));
                Type type = assembly.GetType(args[1]);
                MethodInfo[] methods = type.GetMethods();

                string methodstr = "";



                foreach (MethodInfo method in methods)
                {
                    methodstr += MethodBlock(method) + "\n";
                }

                string RecallClass = @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

";

                RecallClass += $"public static class {type.Name}\n{{\n{methodstr}\n}}";
                File.WriteAllText(Path.GetFullPath(args[2]), RecallClass);
            }
            else if (args.Length == 4)
            {
                Assembly assembly = Assembly.LoadFile(Path.GetFullPath(args[0]));
                Type type = assembly.GetType(args[1]);
                MethodInfo[] methods = type.GetMethods();

                string methodstr = "";

                foreach (MethodInfo method in methods)
                {
                    if (bool.Parse(args[3]))
                    {
                        if (!methodstr.Contains("unsafe"))
                        {
                            methodstr += MethodBlock(method) + "\n";
                        }
                    }
                    else
                    {
                        methodstr += MethodBlock(method) + "\n";
                    }
                }

                string RecallClass = @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

";

                RecallClass += $"public class {type.Name}\n{{\n{methodstr}\n}}";
                File.WriteAllText(Path.GetFullPath(args[2]), RecallClass);
            }
        }

        void dsa(int[] @params)
        {
            
        }
    }
}
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace NeiraEngine.Input
{
    public class ConfigReader
    {
        public Dictionary<string, string[]> values = new Dictionary<string, string[]>();

        public ConfigReader(string path)
        {
            foreach (string line in File.ReadAllLines(path))
            {
                if (!line.StartsWith("//") && line.Length > 0)
                {
                    string[] splitted = line.Split(' ');

                    string name = splitted[0];

                    List<string> args = new List<string>();

                    int quoteStartIndex = 0;

                    for (int i = 0; i < splitted.Length - 1; i++)
                    {
                        if (splitted[i + 1].StartsWith("\""))
                        {
                            quoteStartIndex = i;
                            args.Add(splitted[i + 1].Remove(0, 1));
                        }
                        else
                        {
                            if (quoteStartIndex != 0)
                            {
                                if (splitted[i + 1].EndsWith("\""))
                                {
                                    args[quoteStartIndex] += " " + splitted[i + 1].Remove(splitted[i + 1].Length - 1, 1);
                                    quoteStartIndex = 0;
                                }
                                else
                                    args[quoteStartIndex] += " " + splitted[i + 1];
                            }
                            else
                            {
                                args.Add(splitted[i + 1]);
                            }
                        }
                    }
                    values.Add(name, args.ToArray());
                }
            }
        }

        public ConfigReader(string[] lines)
        {
            foreach (string line in lines)
            {
                if (!line.StartsWith("//") && line.Length > 0)
                {
                    string[] splitted = line.Split(' ');

                    string name = splitted[0];

                    string[] args = new string[splitted.Length - 1];

                    for (int i = 0; i < args.Length; i++)
                    {
                        args[i] = splitted[i + 1];
                    }
                    values.Add(name, args);
                }
            }
        }

        #region Get Values

        //General

        public bool TryGet(string key, string[] out_values)
        {
            try
            {
                out_values = values[key];
                return true;
            }
            catch (KeyNotFoundException e)
            {
                return false;
            }
        }

        // String

        public bool TryGetString(string key, ref string out_string)
        {
            try
            {
                out_string = values[key][0];
                return true;
            }
            catch (KeyNotFoundException e)
            {
                return false;
            }
        }

        public bool TryGetString(string key, int index, ref string out_string)
        {
            try
            {
                out_string = values[key][index];
                return true;
            }
            catch (KeyNotFoundException e)
            {
                return false;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }

        // Float

        public bool TryGetFloat(string key, ref float out_float)
        {
            try
            {
                out_float = float.Parse(values[key][0], CultureInfo.InvariantCulture);
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public bool TryGetFloat(string key, int index, ref float out_float)
        {
            try
            {
                out_float = float.Parse(values[key][index], CultureInfo.InvariantCulture);
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
            catch (FormatException)
            {
                return false;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }

        // Int

        public bool TryGetInt(string key, ref int out_int)
        {
            try
            {
                out_int = int.Parse(values[key][0], CultureInfo.InvariantCulture);
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public bool TryGetInt(string key, int index, ref int out_int)
        {
            try
            {
                out_int = int.Parse(values[key][index], CultureInfo.InvariantCulture);
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
            catch (FormatException)
            {
                return false;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }

        // Boolean

        public bool TryGetBool(string key, ref bool out_bool)
        {
            try
            {
                out_bool = bool.Parse(values[key][0]);
                return true;
            }
            catch (KeyNotFoundException e)
            {
                return false;
            }
            catch (FormatException e)
            {
                return false;
            }
        }

        public bool TryGetBool(string key, int index, ref bool out_bool)
        {
            try
            {
                out_bool = bool.Parse(values[key][index]);
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
            catch (FormatException)
            {
                return false;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }
        #endregion
    }
}
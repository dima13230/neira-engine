using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDK
{
    class ProjectCompiler
    {
        private static byte ByteEncrypt(byte b, int key)
        {
            return (byte)(b ^ key);
        }

        private static byte[] BytesEncrypt(byte[] bytes, int key)
        {
            byte[] b = new byte[bytes.Length];
            for (int i = 0; i < b.Length; i++)
                b[i] = ByteEncrypt(bytes[i], key);
            return b;
        }

        private static byte[] StrToByteArray(string st, Encoding enc)
        {
            return enc.GetBytes(st);
        }

        private static string ByteArrayToStr(byte[] bstr, Encoding enc)
        {
            return enc.GetString(bstr);
        }

        private static int StringToByteKey(string str)
        {
            int key = 0;
            foreach (char c in str)
                key += c ^ 2;
            return key;
        }

        internal void Compile(string projectName, )

    }
}

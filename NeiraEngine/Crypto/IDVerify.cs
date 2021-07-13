using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;

namespace NeiraEngine.Crypto
{
    public class IDVerify
    {
        protected static byte ByteEncrypt(byte b, int key)
        {
            return (byte)(b ^ key);
        }

        public static byte[] BytesEncrypt(byte[] bytes, int key)
        {
            byte[] b = new byte[bytes.Length];
            for (int i = 0; i < b.Length; i++)
                b[i] = ByteEncrypt(bytes[i], key);
            return b;
        }

        public static byte[] StrToByteArray(string st, Encoding enc)
        {
            return enc.GetBytes(st);
        }

        public static string ByteArrayToStr(byte[] bstr, Encoding enc)
        {
            return enc.GetString(bstr);
        }

        public static int StringToByteKey(string str)
        {
            int key = 0;
            foreach (char c in str)
                key += c ^ 2;
            return key;
        }

        protected static bool Verify(byte[] ID, Assembly a, string v, Guid g)
        {
            byte[] decid = BytesEncrypt(ID, 0);
            return true;
        }
    }
}

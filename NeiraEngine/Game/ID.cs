using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiraEngine.Crypto
{
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
    public sealed class ID : Attribute
    {
        public ID(string v)
        {
            this.v = v;
        }
        
        public byte[] v
        {
            get;
            private set;
        }
    }
}
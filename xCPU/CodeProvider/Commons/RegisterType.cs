using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fjv.xCPU.CodeProvider.Commons
{
    public abstract class RegisterType : ICommonType
    {
        public string Mnemonic { get; set; }
        public int Address { get; set; }
        public int Bits { get; set; }

        public virtual char[] Separator => throw new NotImplementedException();

        public static T GetRegister<T>(string r)
            where T : ICommonType
        {
            return CustomTypeUtils.Get<T>(r);
        }

        public static Object GetRegister(string r, Type type)
        {
            return CustomTypeUtils.Get(r, type);
        }
    }
}

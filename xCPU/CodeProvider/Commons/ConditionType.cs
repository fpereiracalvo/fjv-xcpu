using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fjv.xCPU.CodeProvider.Commons
{
    public abstract class ConditionType : ICommonType
    {
        public string Mnemonic { get; set; }
        public byte Address { get; set; }

        public virtual char[] Separator => throw new NotImplementedException();

        public static T GetCondition<T>(string c)
            where T : ICommonType
        {
            return CustomTypeUtils.Get<T>(c);
        }

        public static Object GetCondition(string c, Type type)
        {
            return CustomTypeUtils.Get(c, type);
        }
    }
}

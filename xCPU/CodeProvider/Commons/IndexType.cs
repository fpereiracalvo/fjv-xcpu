using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fjv.xCPU.CodeProvider.Commons
{
    public abstract class IndexType : ICommonType
    {
        public string Mnemonic { get; set; }
        public byte Address { get; set; }
        public byte Value { get; set; }

        public virtual char[] Separator => throw new NotImplementedException();

        public static T GetIndex<T>(string r)
            where T : ICommonType
        {
            return CustomTypeUtils.Get<T>(r);
        }
    }
}

using Fjv.xCPU.CodeProvider.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fjv.Z80.Commons
{
    public class Z80Index : IndexType
    {
        public override char[] Separator => new char[] { ' ', '+', '(', ')' };

        public static Z80Index IX = new Z80Index() {
            Mnemonic = "ix",
            Address = 0xdd
        };

        public static Z80Index IY = new Z80Index() {
            Mnemonic = "iy",
            Address = 0xfd
        };

        public static Z80Index GetIndex(string i)
        {
            return IndexType.GetIndex<Z80Index>(i);
        }
    }
}

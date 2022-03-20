using Fjv.xCPU.CodeProvider.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fjv.Z80.Commons
{
    public class Z80PairRegister : RegisterType
    {
        public override char[] Separator => new char[] { '(', ')' };

        //par.
        public static Z80PairRegister BC = new Z80PairRegister()
        {
            Mnemonic = "bc",
            Address = 0x00
        };

        public static Z80PairRegister DE = new Z80PairRegister()
        {
            Mnemonic = "de",
            Address = 0x01
        };

        public static Z80PairRegister HL = new Z80PairRegister()
        {
            Mnemonic = "hl",
            Address = 0x02
        };

        //pila.
        public static Z80PairRegister SP = new Z80PairRegister()
        {
            Mnemonic = "sp",
            Address = 0x03
        };

        public static Z80PairRegister AF = new Z80PairRegister()
        {
            Mnemonic = "af",
            Address = 0x03
        };

        public static Z80PairRegister IX = new Z80PairRegister()
        {
            Mnemonic = "ix",
            Address = 0x02
        };

        public static Z80PairRegister IY = new Z80PairRegister()
        {
            Mnemonic = "iy",
            Address = 0x02
        };

        public static Z80PairRegister GetRegister(string r)
        {
            return RegisterType.GetRegister<Z80PairRegister>(r);
        }
    }
}

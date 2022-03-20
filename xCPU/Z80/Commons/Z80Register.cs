using Fjv.xCPU.CodeProvider.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fjv.Z80.Commons
{
    public class Z80Register : RegisterType
    {
        public override char[] Separator => new char[] { '(', ')' };

        public Z80Register()
        {
            //default.
            Bits = 8;
        }

        public static Z80Register B = new Z80Register() {
            Mnemonic = "b",
            Address = 0x00
        };

        public static Z80Register C = new Z80Register()
        {
            Mnemonic = "c",
            Address = 0x01
        };

        public static Z80Register D = new Z80Register()
        {
            Mnemonic = "d",
            Address = 0x02
        };

        public static Z80Register E = new Z80Register()
        {
            Mnemonic = "e",
            Address = 0x03
        };

        public static Z80Register H = new Z80Register()
        {
            Mnemonic = "h",
            Address = 0x04
        };

        public static Z80Register L = new Z80Register()
        {
            Mnemonic = "l",
            Address = 0x05
        };

        public static Z80Register A = new Z80Register()
        {
            Mnemonic = "a",
            Address = 0x07
        };

        public static Z80Register I = new Z80Register()
        {
            Mnemonic = "i",
            Address = 0
        };

        public static Z80Register R = new Z80Register()
        {
            Mnemonic = "r",
            Address = 0
        };

        public static Z80Register GetRegister(string r)
        {
            return RegisterType.GetRegister<Z80Register>(r);
        }
    }
}

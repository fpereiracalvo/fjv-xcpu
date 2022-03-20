using Fjv.xCPU.CodeProvider.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fjv.Z80.Commons
{
    public class Z80PrimeRegister : RegisterType
    {
        public override char[] Separator => new char[] { '\0' };

        public Z80PrimeRegister()
        {
            //default.
            Bits = 8;
        }

        public static Z80PrimeRegister BPrime = new Z80PrimeRegister()
        {
            Mnemonic = "b'",
            Address = 0x00
        };

        public static Z80PrimeRegister CPrime = new Z80PrimeRegister()
        {
            Mnemonic = "c'",
            Address = 0x01
        };

        public static Z80PrimeRegister DPrime = new Z80PrimeRegister()
        {
            Mnemonic = "d'",
            Address = 0x02
        };

        public static Z80PrimeRegister EPrime = new Z80PrimeRegister()
        {
            Mnemonic = "e'",
            Address = 0x03
        };

        public static Z80PrimeRegister HPrime = new Z80PrimeRegister()
        {
            Mnemonic = "h'",
            Address = 0x04
        };

        public static Z80PrimeRegister LPrime = new Z80PrimeRegister()
        {
            Mnemonic = "l'",
            Address = 0x05
        };

        public static Z80PrimeRegister APrime = new Z80PrimeRegister()
        {
            Mnemonic = "a'",
            Address = 0x07
        };

        public static Z80PrimeRegister BCPrime = new Z80PrimeRegister()
        {
            Mnemonic = "bc'",
            Address = 0x00
        };

        public static Z80PrimeRegister DEPrime = new Z80PrimeRegister()
        {
            Mnemonic = "de'",
            Address = 0x01
        };

        public static Z80PrimeRegister HLPrime = new Z80PrimeRegister()
        {
            Mnemonic = "hl'",
            Address = 0x02
        };

        public static Z80PrimeRegister AFPrime = new Z80PrimeRegister()
        {
            Mnemonic = "af'",
            Address = 0x03
        };

        public static Z80PrimeRegister GetRegister(string r)
        {
            return RegisterType.GetRegister<Z80PrimeRegister>(r);
        }
    }
}

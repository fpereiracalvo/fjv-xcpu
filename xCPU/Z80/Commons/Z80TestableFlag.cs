using Fjv.xCPU.CodeProvider.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fjv.Z80.Commons
{
    public class Z80TestableFlag : ConditionType
    {
        public override char[] Separator => new char[] { '\0' };

        public static Z80TestableFlag NZ = new Z80TestableFlag() {
            Mnemonic = "nz",
            Address = 0x00
        };

        public static Z80TestableFlag Z = new Z80TestableFlag()
        {
            Mnemonic = "z",
            Address = 0x01
        };

        public static Z80TestableFlag NC = new Z80TestableFlag()
        {
            Mnemonic = "nc",
            Address = 0x02
        };

        public static Z80TestableFlag C = new Z80TestableFlag()
        {
            Mnemonic = "c",
            Address = 0x03
        };

        public static Z80TestableFlag PO = new Z80TestableFlag()
        {
            Mnemonic = "po",
            Address = 0x04
        };

        public static Z80TestableFlag PE = new Z80TestableFlag()
        {
            Mnemonic = "pe",
            Address = 0x05
        };

        public static Z80TestableFlag P = new Z80TestableFlag()
        {
            Mnemonic = "p",
            Address = 0x06
        };

        public static Z80TestableFlag M = new Z80TestableFlag()
        {
            Mnemonic = "m",
            Address = 0x07
        };

        public static Z80TestableFlag GetCondition(string c)
        {
            return ConditionType.GetCondition<Z80TestableFlag>(c);
        }
    }
}

using Fjv.xCPU.CodeProvider;
using Fjv.xCPU.CodeProvider.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fjv.Z80.Instructions
{
    /// <summary>
    /// Set de instrucciones Zilog Z84C00 - Z80 CPU.
    /// </summary>
    public class Z84C00InstructionSet : InstructionSetBase<CpuInstructionType>
    {
        public override string Name => "Zilog - Z8400/Z84C00";

        public override Type GetParamType(string v)
        {
            var type = base.GetParamType(v);

            if (type != null)
            {
                return type;
            }

            return null;
        }

        public Z84C00InstructionSet()
            //set pointer pattern sample.
            : base(new Fjv.xCPU.CodeProvider.Globals.Pointers(@"(?<=\().*(?=\))"))
        {
            this.AddType(typeof(Commons.Z80Index))
                .AddType(typeof(Commons.Z80PrimeRegister))
                .AddType(typeof(Commons.Z80Register))
                .AddType(typeof(Commons.Z80PairRegister))
                .AddType(typeof(Commons.Z80TestableFlag));

            //8bit load group.
            this.AddInstructions(_8BitLoadGroup.GetSet());

            //16bit load group.
            this.AddInstructions(_16bitsLoadGroup.GetSet());

            //exchange, block transfer, block search group.
            this.AddInstructions(ExchangeGroup.GetSet());

            //8bit arithmetical and logical group.
            this.AddInstructions(_8bitArithmeticGroup.GetSet());

            //general purpose arithmetic group & cpu control group.
            this.AddInstructions(GeneralPurposeArithmeticAndCpuControlGroup.GetSet());

            //16bit arithmetic group.
            this.AddInstructions(_16BitArithmeticGroup.GetSet());

            //rotate and shift group.
            this.AddInstructions(RotateAndShiftGroup.GetSet());

            //bit set, reset & test group.
            this.AddInstructions(BitSetResetAndTestGroup.GetSet());

            //jump group.
            this.AddInstructions(JumpGroup.GetSet());

            //call and return group.
            this.AddInstructions(CallReturnGroup.GetSet());

            //input and output group.
            this.AddInstructions(InputOutputGroup.GetSet());
        }
    }
}

using Fjv.xCPU.CodeProvider;
using Fjv.xCPU.CodeProvider.Commons;
using Fjv.xCPU.CodeProvider.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Fjv.Z80.Instructions
{
    public class GeneralPurposeArithmeticAndCpuControlGroup : InstructionSetBase<CpuInstructionType>
    {
        public static List<CpuInstructionType> GetSet()
        {
            var general = new GeneralPurposeArithmeticAndCpuControlGroup();

            general
                .AddInstruction(new CpuInstructionType
                {
                    Mnemonic = "daa",
                    Size = new Func<IInstructionArgument, int>((x) => { return 1; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) =>
                    {
                        return new byte[] { 0x27 };
                    })
                })
                .AddInstruction(new CpuInstructionType
                {
                    Mnemonic = "cpl",
                    Size = new Func<IInstructionArgument, int>((x) => { return 1; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) =>
                    {
                        return new byte[] { 0x2f };
                    })
                })
                .AddInstruction(new CpuInstructionType
                {
                    Mnemonic = "neg",
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) =>
                    {
                        return new byte[] { 0xed, 0x44 };
                    })
                })
                .AddInstruction(new CpuInstructionType
                {
                    Mnemonic = "ccf",
                    Size = new Func<IInstructionArgument, int>((x) => { return 1; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) =>
                    {
                        return new byte[] { 0x3f };
                    })
                })
                .AddInstruction(new CpuInstructionType
                {
                    Mnemonic = "scf",
                    Size = new Func<IInstructionArgument, int>((x) => { return 1; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) =>
                    {
                        return new byte[] { 0x37 };
                    })
                })
                .AddInstruction(new CpuInstructionType
                {
                    Mnemonic = "nop",
                    Size = new Func<IInstructionArgument, int>((x) => { return 1; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) =>
                    {
                        return new byte[] { 0x00 };
                    })
                })
                .AddInstruction(new CpuInstructionType
                {
                    Mnemonic = "halt",
                    Size = new Func<IInstructionArgument, int>((x) => { return 1; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) =>
                    {
                        return new byte[] { 0x76 };
                    })
                })
                .AddInstruction(new CpuInstructionType
                {
                    Mnemonic = "di",
                    Size = new Func<IInstructionArgument, int>((x) => { return 1; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) =>
                    {
                        return new byte[] { 0xf3 };
                    })
                })
                .AddInstruction(new CpuInstructionType
                {
                    Mnemonic = "ei",
                    Size = new Func<IInstructionArgument, int>((x) => { return 1; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) =>
                    {
                        return new byte[] { 0xfb };
                    })
                })
                .AddInstruction(new CpuInstructionType
                {
                    Mnemonic = "im",
                    Operand = new Type[] { typeof(byte) },
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) =>
                    {
                        var values = new byte[] { 0x46, 0x56, 0x5e };
                        var indexValue = (byte)x.LeftValue;

                        if (indexValue > 2)
                        {
                            throw new Exception("Value expected: 0, 1 or 2.");
                        }

                        return new byte[] { 0xed, values[indexValue] };
                    })
                });

            return general.Instructions;
        }
    }
}

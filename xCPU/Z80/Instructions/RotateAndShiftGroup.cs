using Fjv.xCPU.CodeProvider;
using Fjv.xCPU.CodeProvider.Commons;
using Fjv.xCPU.CodeProvider.Tools;
using Fjv.Z80.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fjv.Z80.Instructions
{
    public class RotateAndShiftGroup : InstructionSetBase<CpuInstructionType>
    {
        public static List<CpuInstructionType> GetSet()
        {
            var group = new RotateAndShiftGroup()
                .AddInstruction(new CpuInstructionType {
                    Mnemonic = "rlca",
                    Size = new Func<IInstructionArgument, int>((x)=> { return 1; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x)=> {
                        return new byte[] { 0x07 };
                    })
                })
                .AddInstruction(new CpuInstructionType
                {
                    Mnemonic = "rla",
                    Size = new Func<IInstructionArgument, int>((x) => { return 1; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        return new byte[] { 0x17 };
                    })
                })
                .AddInstruction(new CpuInstructionType
                {
                    Mnemonic = "rrca",
                    Size = new Func<IInstructionArgument, int>((x) => { return 1; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        return new byte[] { 0x0f };
                    })
                })
                .AddInstruction(new CpuInstructionType
                {
                    Mnemonic = "rra",
                    Size = new Func<IInstructionArgument, int>((x) => { return 1; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        return new byte[] { 0x1f };
                    })
                })
                .AddInstruction(new CpuInstructionType
                {
                    Mnemonic = "rld",
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        return new byte[] { 0xed, 0x6f };
                    })
                })
                .AddInstruction(new CpuInstructionType
                {
                    Mnemonic = "rrd",
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        return new byte[] { 0xed, 0x67 };
                    })
                });

            group.AddInstructions(GetRclCodes("rlc", 0x00));
            group.AddInstructions(GetRclCodes("rl",  0x02));
            group.AddInstructions(GetRclCodes("rrc", 0x01));
            group.AddInstructions(GetRclCodes("rr",  0x03));
            group.AddInstructions(GetRclCodes("sla", 0x04));
            group.AddInstructions(GetRclCodes("sra", 0x05));
            group.AddInstructions(GetRclCodes("srl", 0x07));

            return group.Instructions;
        }

        private static List<CpuInstructionType> GetRclCodes(string mnemonic, byte code)
        {
            return new RotateAndShiftGroup()
                // xxx r
                .AddInstruction(new CpuInstructionType {
                    Mnemonic = mnemonic,
                    Operand = new Type[] { typeof(Z80Register) },
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var left = (Z80Register)x.LeftValue;

                        return new byte[] { 0xcb, (byte)(left.Address | code << 3) };
                    })
                })
                // xxx (hl)
                .AddInstruction(new CpuInstructionType {
                    Mnemonic = mnemonic,
                    Operand = new Type[] { typeof(Z80PairRegister) },
                    LeftPointer = true,
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Constraints = new ICommonType[] { Z80PairRegister.HL },
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var left = (Z80PairRegister)x.LeftValue;

                        if (left != Z80PairRegister.HL)
                        {
                            throw new Exception("Register expected: hl.");
                        }

                        return new byte[] { 0xcb, (byte)(0x06 | code << 3) };
                    })
                })
                // xxx (ix+d) / rlc (iy + d)
                .AddInstruction(new CpuInstructionType
                {
                    Mnemonic = mnemonic,
                    Operand = new Type[] { typeof(Z80Index) },
                    Size = new Func<IInstructionArgument, int>((x) => { return 4; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var index = ((string)x.Left).Replace("(", "").Replace(")", "").Trim();
                        var split = index.Split('+');

                        var left = Z80Index.GetIndex(index);

                        byte d = 0;
                        if (index.IndexOf("+") > 0)
                        {
                            d = Bytes.GetByte(split.LastOrDefault().Trim());
                        }

                        return new byte[] { (byte)left.Address, 0xcb, d, (byte)(0x06 | code << 3) };
                    })
                }).Instructions;
        }
    }
}

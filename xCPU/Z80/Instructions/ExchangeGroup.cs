using Fjv.xCPU.CodeProvider;
using Fjv.xCPU.CodeProvider.Commons;
using Fjv.Z80.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fjv.Z80.Instructions
{
    public class ExchangeGroup : InstructionSetBase<CpuInstructionType>
    {
        public static List<CpuInstructionType> GetSet()
        {
            var group = new ExchangeGroup();

            //ex p,p
            group.AddInstruction(new CpuInstructionType() {
                    Mnemonic = "ex",
                    Operand = new Type[] { typeof(Commons.Z80PairRegister), typeof(Commons.Z80PairRegister) },
                    Size = new Func<IInstructionArgument, int>((x)=> { return 1; }),
                    Constraints = new ICommonType[] { Z80PairRegister.DE, Z80PairRegister.HL },
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var left = (Z80PairRegister)x.LeftValue;
                        var right = (Z80PairRegister)x.RightValue;

                        if (left == Commons.Z80PairRegister.DE && right == Commons.Z80PairRegister.HL)
                        {
                            return new byte[] { 0xeb };
                        }

                        throw new Exception("Register expected: de, hl");
                    })
                })

                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "ex",
                    Operand = new Type[] { typeof(Commons.Z80PairRegister), typeof(Commons.Z80PrimeRegister) },
                    Size = new Func<IInstructionArgument, int>((x) => { return 1; }),
                    Constraints = new ICommonType[] { Z80PairRegister.AF, Z80PrimeRegister.AFPrime },
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var left = (Z80PairRegister)x.LeftValue;
                        var right = (Z80PrimeRegister)x.RightValue;

                        if (left == Z80PairRegister.AF && right == Z80PrimeRegister.AFPrime)
                        {
                            return new byte[] { 0x08 };
                        }

                        throw new Exception("Register expected: af, af'");
                    })
                })

                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "exx",
                    Size = new Func<IInstructionArgument, int>((x) => { return 1; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        return new byte[] { 0xd9 };
                    })
                })

                // ex (p), p
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "ex",
                    Operand = new Type[] { typeof(Commons.Z80PairRegister), typeof(Commons.Z80PairRegister) },
                    LeftPointer = true,
                    Size = new Func<IInstructionArgument, int>((x) => { return 1; }),
                    Constraints = new ICommonType[] { Z80PairRegister.SP, Z80PairRegister.HL },
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var left = (Z80PairRegister)x.LeftValue;
                        var right = (Z80PairRegister)x.RightValue;

                        if (left == Z80PairRegister.SP && right == Z80PairRegister.HL)
                        {
                            return new byte[] { 0xe3 };
                        }

                        throw new Exception("Register expected: sp, hl");
                    })
                })

                // ex (p), p
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "ex",
                    Operand = new Type[] { typeof(Commons.Z80PairRegister), typeof(Commons.Z80Index) },
                    LeftPointer = true,
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Constraints = new ICommonType[] { Z80PairRegister.SP },
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var left = (Z80PairRegister)x.LeftValue;
                        var right = (Z80Index)x.RightValue;

                        if (left == Commons.Z80PairRegister.SP)
                        {
                            return new byte[] { right.Address, 0xe3 };
                        }

                        throw new Exception("Register expected: sp, ix or iy'");
                    })
                })

                //ldi
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "ldi",
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        return new byte[] { 0xed, 0xad };
                    })
                })

                //ldir
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "ldir",
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        return new byte[] { 0xed, 0x80 };
                    })
                })

                //ldd
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "ldd",
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        return new byte[] { 0xed, 0xa8 };
                    })
                })

                //lddr
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "lddr",
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        return new byte[] { 0xed, 0xb8 };
                    })
                })

                //cpi
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "cpi",
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        return new byte[] { 0xed, 0xa1 };
                    })
                })

                //cpir
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "cpir",
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        return new byte[] { 0xed, 0xb1 };
                    })
                })

                //cpd
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "cpd",
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        return new byte[] { 0xed, 0xa9 };
                    })
                })

                //cpdr
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "cpdr",
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        return new byte[] { 0xed, 0xb9 };
                    })
                });

            return group.Instructions;
        }
    }
}

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
    public class InputOutputGroup : InstructionSetBase<CpuInstructionType>
    {
        public static List<CpuInstructionType> GetSet()
        {
            var group = new InputOutputGroup();

            // in a,(n)
            group.AddInstruction(new CpuInstructionType() {
                    Mnemonic = "in",
                    Operand = new Type[] { typeof(Commons.Z80Register), typeof(byte), },
                    RightPointer = true,
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Constraints = new ICommonType[] { Z80Register.A },
                    Instruction = new Func<IInstructionArgument, byte[]>((x)=> {
                        var left = (Z80Register)x.LeftValue;

                        if (left == Z80Register.A)
                        {
                            var address = (byte)x.RightValue;

                            var hex = new byte[] {
                                0xdb,
                                address
                            };

                            return hex;
                        }

                        throw new Exception("Register expected: a.");
                    })
                })

                // in r,(c)
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "in",
                    Operand = new Type[] { typeof(Commons.Z80Register), typeof(Commons.Z80Register) },
                    RightPointer = true,
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Constraints = new ICommonType[] { Z80Register.C },
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var left = (Z80Register)x.LeftValue;
                        var right = (Z80Register)x.RightValue;

                        if (right == Z80Register.C)
                        {
                            var reg = (byte)left.Address << 3;

                            var hex = new byte[] {
                                0xed,
                                (byte)(0x40 | reg)
                            };

                            return hex;
                        }

                        throw new Exception("Register expected: c.");
                    })
                })

                // ini
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "ini",
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var hex = new byte[] {
                                0xed,
                                0xa2
                            };

                        return hex;
                    })
                })

                // inir
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "inir",
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var hex = new byte[] {
                                0xed,
                                0xb2
                            };

                        return hex;
                    })
                })

                // ind
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "ind",
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var hex = new byte[] {
                                0xed,
                                0xaa
                            };

                        return hex;
                    })
                })

                // indr
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "indr",
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var hex = new byte[] {
                                0xed,
                                0xba
                            };

                        return hex;
                    })
                })

                // out (n),a
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "out",
                    Operand = new Type[] { typeof(byte), typeof(Commons.Z80Register) },
                    LeftPointer = true,
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Constraints = new ICommonType[] { Z80Register.A },
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var right = (Z80Register)x.RightValue;

                        if (right == Commons.Z80Register.A)
                        {
                            var address = (byte)x.LeftValue;

                            var hex = new byte[] {
                                0xd3,
                                address
                            };

                            return hex;
                        }

                        throw new Exception("Register expected: a.");
                    })
                })


                // out (c), r
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "out",
                    Operand = new Type[] { typeof(Commons.Z80Register), typeof(Commons.Z80Register) },
                    LeftPointer = true,
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Constraints = new ICommonType[] { Z80Register.C },
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var left = (Z80Register)x.LeftValue;
                        var right = (Z80Register)x.RightValue;

                        if (left == Commons.Z80Register.C)
                        {
                            var reg = (byte)right.Address << 3;

                            var hex = new byte[] {
                                0xed,
                                (byte)(0x41 | reg)
                            };

                            return hex;
                        }

                        throw new Exception("Register expected: c.");
                    })
                })

                // outi
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "outi",
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var hex = new byte[] {
                                0xed,
                                0xa3
                            };

                        return hex;
                    })
                })

                // otir
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "otir",
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var hex = new byte[] {
                                0xed,
                                0xb3
                            };

                        return hex;
                    })
                })

                // outd
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "outd",
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var hex = new byte[] {
                                0xed,
                                0xab
                            };

                        return hex;
                    })
                })

                // otdr
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "otdr",
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var hex = new byte[] {
                                0xed,
                                0xbb
                            };

                        return hex;
                    })
                });

            return group.Instructions;
        }
    }
}

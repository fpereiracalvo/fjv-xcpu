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
    public class _16BitArithmeticGroup : InstructionSetBase<CpuInstructionType>
    {
        public static List<CpuInstructionType> GetSet()
        {
            var group = new _16BitArithmeticGroup()
                .AddInstruction(new CpuInstructionType {
                    Mnemonic = "add",
                    Operand = new Type[] { typeof(Z80PairRegister), typeof(Z80PairRegister) },
                    Size = new Func<IInstructionArgument, int>((x) => { return 1; }),
                    Constraints = new ICommonType[] { Z80PairRegister.HL, Z80PairRegister.BC, Z80PairRegister.DE, Z80PairRegister.HL, Z80PairRegister.SP },
                    Instruction = new Func<IInstructionArgument, byte[]>((x)=> {
                        var left = (Z80PairRegister)x.LeftValue;
                        var right = (Z80PairRegister)x.RightValue;

                        if (left == Z80PairRegister.HL)
                        {
                            if (right == Z80PairRegister.BC || 
                                right == Z80PairRegister.DE ||
                                right == Z80PairRegister.HL ||
                                right == Z80PairRegister.SP)
                            {
                                var address = (byte)right.Address;

                                return new byte[] { (byte)(0x09 | (address << 4)) };
                            }
                        }

                        throw new Exception("Register expected: bc, de, hl, sp.");
                    })
                })
                .AddInstruction(new CpuInstructionType
                {
                    Mnemonic = "adc",
                    Operand = new Type[] { typeof(Z80PairRegister), typeof(Z80PairRegister) },
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Constraints = new ICommonType[] { Z80PairRegister.HL, Z80PairRegister.BC, Z80PairRegister.DE, Z80PairRegister.HL, Z80PairRegister.SP },
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var left = (Z80PairRegister)x.LeftValue;
                        var right = (Z80PairRegister)x.RightValue;

                        if (left == Z80PairRegister.HL)
                        {
                            if (right == Z80PairRegister.BC ||
                                right == Z80PairRegister.DE ||
                                right == Z80PairRegister.HL ||
                                right == Z80PairRegister.SP)
                            {
                                var address = (byte)right.Address;

                                return new byte[] { 0xed, (byte)(0x0a | (address << 4)) };
                            }
                        }

                        throw new Exception("Register expected: bc, de, hl, sp.");
                    })
                })
                .AddInstruction(new CpuInstructionType
                {
                    Mnemonic = "sbc",
                    Operand = new Type[] { typeof(Z80PairRegister), typeof(Z80PairRegister) },
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Constraints = new ICommonType[] { Z80PairRegister.HL, Z80PairRegister.BC, Z80PairRegister.DE, Z80PairRegister.HL, Z80PairRegister.SP },
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var left = (Z80PairRegister)x.LeftValue;
                        var right = (Z80PairRegister)x.RightValue;

                        if (left == Z80PairRegister.HL)
                        {
                            if (right == Z80PairRegister.BC ||
                                right == Z80PairRegister.DE ||
                                right == Z80PairRegister.HL ||
                                right == Z80PairRegister.SP)
                            {
                                var address = (byte)right.Address;

                                return new byte[] { 0xed, (byte)(0x02 | (address << 4)) };
                            }
                        }

                        throw new Exception("Register expected: bc, de, hl, sp.");
                    })
                })
                .AddInstruction(new CpuInstructionType
                {
                    Mnemonic = "add",
                    Operand = new Type[] { typeof(Z80Index), typeof(Z80PairRegister) },
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Constraints = new ICommonType[] { Z80PairRegister.BC, Z80PairRegister.DE, Z80PairRegister.IX, Z80PairRegister.IY, Z80PairRegister.SP },
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var left = (Z80Index)x.LeftValue;
                        var right = (Z80PairRegister)x.RightValue;

                        if (right == Z80PairRegister.BC ||
                            right == Z80PairRegister.DE ||
                            right == Z80PairRegister.IX ||
                            right == Z80PairRegister.IY ||
                            right == Z80PairRegister.SP)
                        {
                            var index = (byte)left.Address;
                            var address = (byte)right.Address;

                            return new byte[] { index, (byte)(0x09 | (address << 4)) };
                        }

                        throw new Exception("Register expected: bc, de, ix/iy, sp.");
                    })
                })
                .AddInstruction(new CpuInstructionType
                {
                    Mnemonic = "inc",
                    Operand = new Type[] { typeof(Z80PairRegister)},
                    Size = new Func<IInstructionArgument, int>((x) => { return 1; }),
                    Constraints = new ICommonType[] { Z80PairRegister.BC, Z80PairRegister.DE, Z80PairRegister.HL, Z80PairRegister.SP },
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var left = (Z80PairRegister)x.LeftValue;

                        if (left == Z80PairRegister.BC ||
                            left == Z80PairRegister.DE ||
                            left == Z80PairRegister.HL ||
                            left == Z80PairRegister.SP)
                        {
                            var address = (byte)left.Address;

                            return new byte[] { (byte)(0x03 | (address << 4)) };
                        }

                        throw new Exception("Register expected: bc, de, hl, sp.");
                    })
                })
                .AddInstruction(new CpuInstructionType
                {
                    Mnemonic = "inc",
                    Operand = new Type[] { typeof(Z80Index) },
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var left = (Z80Index)x.LeftValue;

                        return new byte[] { (byte)left.Address, 0x23 };
                    })
                })
                .AddInstruction(new CpuInstructionType
                {
                    Mnemonic = "dec",
                    Operand = new Type[] { typeof(Z80PairRegister) },
                    Size = new Func<IInstructionArgument, int>((x) => { return 1; }),
                    Constraints = new ICommonType[] { Z80PairRegister.BC, Z80PairRegister.DE, Z80PairRegister.HL, Z80PairRegister.SP },
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var left = (Z80PairRegister)x.LeftValue;

                        if (left == Z80PairRegister.BC ||
                            left == Z80PairRegister.DE ||
                            left == Z80PairRegister.HL ||
                            left == Z80PairRegister.SP)
                        {
                            var address = (byte)left.Address;

                            return new byte[] { (byte)(0x0c | (address << 4)) };
                        }

                        throw new Exception("Register expected: bc, de, hl, sp.");
                    })
                })
                .AddInstruction(new CpuInstructionType
                {
                    Mnemonic = "dec",
                    Operand = new Type[] { typeof(Z80Index) },
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var left = (Z80Index)x.LeftValue;

                        return new byte[] { (byte)left.Address, 0x2b };
                    })
                });

            return group.Instructions;
        }
    }
}

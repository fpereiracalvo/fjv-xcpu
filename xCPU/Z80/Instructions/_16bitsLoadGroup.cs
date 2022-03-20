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
    public class _16bitsLoadGroup : InstructionSetBase<CpuInstructionType>
    {
        public static List<CpuInstructionType> GetSet()
        {
            var group = new _16bitsLoadGroup();

            var mnemonic = "ld";

            //ld dd, nn
            group.AddInstruction(new CpuInstructionType()
            {
                Mnemonic = mnemonic,
                Operand = new Type[] { typeof(Commons.Z80PairRegister), typeof(UInt16) },
                Size = new Func<IInstructionArgument, int>((x) => { return 3; }),
                Constraints = new ICommonType[] { Z80PairRegister.BC, Z80PairRegister.DE, Z80PairRegister.HL, Z80PairRegister.SP },
                Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                    var left = (Z80PairRegister)x.LeftValue;
                    var right = (UInt16)x.RightValue;

                    if (left == Z80PairRegister.BC ||
                        left == Z80PairRegister.DE ||
                        left == Z80PairRegister.HL ||
                        left == Z80PairRegister.SP)
                    {
                        var hex = new List<byte>();

                        hex.Add((byte)(0x01 | (left.Address << 4)));

                        var value = BitConverter.GetBytes(right);

                        hex.AddRange(value);

                        return hex.ToArray();
                    }

                    throw new Exception("Register expected: bc, de, hl, o sp.");
                })
            })

            .AddInstruction(new CpuInstructionType()
            {
                Mnemonic = mnemonic,
                Operand = new Type[] { typeof(Commons.Z80PairRegister), typeof(byte) },
                Size = new Func<IInstructionArgument, int>((x) => { return 3; }),
                Constraints = new ICommonType[] { Z80PairRegister.BC, Z80PairRegister.DE, Z80PairRegister.HL, Z80PairRegister.SP },
                Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                    var left = (Z80PairRegister)x.LeftValue;
                    var right = Convert.ToUInt16((byte)x.RightValue);

                    if (left == Z80PairRegister.BC ||
                        left == Z80PairRegister.DE ||
                        left == Z80PairRegister.HL ||
                        left == Z80PairRegister.SP)
                    {
                        var hex = new List<byte>();

                        hex.Add((byte)(0x01 | (left.Address << 4)));

                        var value = BitConverter.GetBytes(right);

                        hex.AddRange(value);

                        return hex.ToArray();
                    }

                    throw new Exception("Register expected: bc, de, hl, o sp.");
                })
            })

            //ld i, nn
            .AddInstruction(new CpuInstructionType()
            {
                Mnemonic = mnemonic,
                Operand = new Type[] { typeof(Commons.Z80Index), typeof(UInt16) },
                Size = new Func<IInstructionArgument, int>((x) => { return 4; }),
                Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                    var left = (Z80Index)x.LeftValue;
                    var right = (UInt16)x.RightValue;

                    return new byte[] { left.Address, 0x21 }.Concat(BitConverter.GetBytes(right)).ToArray();
                })
            })

            //ld dd, (nn)
            .AddInstruction(new CpuInstructionType() {
                Mnemonic = mnemonic,
                Operand = new Type[] { typeof(Commons.Z80PairRegister), typeof(UInt16) },
                RightPointer = true,
                Size = new Func<IInstructionArgument, int>((x) => {
                    var left = (Z80PairRegister)x.LeftValue;

                    if (left == Z80PairRegister.HL)
                    {
                        return 3;
                    }

                    return 4;
                }),
                Constraints = new ICommonType[] { Z80PairRegister.HL, Z80PairRegister.BC, Z80PairRegister.DE, Z80PairRegister.SP },
                Instruction = new Func<IInstructionArgument, byte[]>((x) =>
                {
                    var left = (Z80PairRegister)x.LeftValue;
                    var right = (UInt16)x.RightValue;

                    if (left == Z80PairRegister.HL)
                    {
                        return new byte[] { 0x2a }.Concat(BitConverter.GetBytes(right)).ToArray();
                    }
                    else if (left == Z80PairRegister.BC || left == Z80PairRegister.DE || left == Z80PairRegister.SP)
                    {
                        return new byte[] { 0xed, (byte)(0x4b | (left.Address << 4)) }.Concat(BitConverter.GetBytes(right)).ToArray();
                    }

                    throw new Exception("Register expected: bc, de, o sp.");
                })
            })

            //ld i, (nn)
            .AddInstruction(new CpuInstructionType()
            {
                Mnemonic = mnemonic,
                Operand = new Type[] { typeof(Commons.Z80Index), typeof(UInt16) },
                RightPointer = true,
                Size = new Func<IInstructionArgument, int>((x) => { return 4; }),
                Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                    var left = (Z80Index)x.LeftValue;
                    var right = (UInt16)x.RightValue;

                    return new byte[] { left.Address, 0x2a }.Concat(BitConverter.GetBytes(right)).ToArray();
                })
            })

            //ld (nn), dd
            .AddInstruction(new CpuInstructionType()
            {
                Mnemonic = mnemonic,
                Operand = new Type[] { typeof(UInt16), typeof(Commons.Z80PairRegister) },
                LeftPointer = true,
                Size = new Func<IInstructionArgument, int>((x) => {
                    var right = (Z80PairRegister)x.RightValue;

                    if (right == Z80PairRegister.HL)
                    {
                        return 3;
                    }

                    return 4;
                }),
                Constraints = new ICommonType[] { Z80PairRegister.HL, Z80PairRegister.BC, Z80PairRegister.DE, Z80PairRegister.SP },
                Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                    var left = (UInt16)x.LeftValue;
                    var right = (Z80PairRegister)x.RightValue;

                    if (right == Z80PairRegister.HL)
                    {
                        return new byte[] { 0x22 }.Concat(BitConverter.GetBytes(left)).ToArray();
                    }
                    else if (right == Commons.Z80PairRegister.BC || right == Commons.Z80PairRegister.DE || right == Commons.Z80PairRegister.SP)
                    {
                        return new byte[] { 0xed, (byte)(0x43 | (right.Address << 4)) }.Concat(BitConverter.GetBytes(left)).ToArray();
                    }

                    throw new Exception("Register expected: bc, de, o sp.");
                })
            })

            //ld (nn), i
            .AddInstruction(new CpuInstructionType()
            {
                Mnemonic = mnemonic,
                Operand = new Type[] { typeof(UInt16), typeof(Commons.Z80Index) },
                LeftPointer = true,
                Size = new Func<IInstructionArgument, int>((x) => { return 4; }),
                Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                    var left = (UInt16)x.LeftValue;
                    var right = (Z80Index)x.RightValue;

                    return new byte[] { right.Address, 0x22 }.Concat(BitConverter.GetBytes(left)).ToArray();
                })
            })

            //ld dd, dd
            .AddInstruction(new CpuInstructionType()
            {
                Mnemonic = mnemonic,
                Operand = new Type[] { typeof(Commons.Z80PairRegister), typeof(Commons.Z80PairRegister) },
                Size = new Func<IInstructionArgument, int>((x) => { return 1; }),
                Constraints = new ICommonType[] { Z80PairRegister.SP, Z80PairRegister.HL },
                Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                    var left = (Z80PairRegister)x.LeftValue;
                    var right = (Z80PairRegister)x.RightValue;

                    if (left == Commons.Z80PairRegister.SP && right == Commons.Z80PairRegister.HL)
                    {
                        return new byte[] { 0xf9 };
                    }

                    throw new Exception("Register expected: sp y hl.");
                })
            })

            //ld dd, i
            .AddInstruction(new CpuInstructionType()
            {
                Mnemonic = mnemonic,
                Operand = new Type[] { typeof(Commons.Z80PairRegister), typeof(Commons.Z80Index) },
                Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                Constraints = new ICommonType[] { Z80PairRegister.SP },
                Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                    var left = (Z80PairRegister)x.LeftValue;
                    var right = (Z80Index)x.RightValue;

                    if (left == Commons.Z80PairRegister.SP)
                    {
                        return new byte[] { right.Address, 0xf9 };
                    }

                    throw new Exception("Register expected: sp");
                })
            })

            //push dd
            .AddInstruction(new CpuInstructionType()
            {
                Mnemonic = "push",
                Operand = new Type[] { typeof(Commons.Z80PairRegister) },
                Size = new Func<IInstructionArgument, int>((x) => { return 1; }),
                Constraints = new ICommonType[] { Z80PairRegister.BC, Z80PairRegister.DE, Z80PairRegister.HL, Z80PairRegister.AF },
                Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                    var left = (Z80PairRegister)x.LeftValue;

                    if (left == Z80PairRegister.BC ||
                        left == Z80PairRegister.DE ||
                        left == Z80PairRegister.HL ||
                        left == Z80PairRegister.AF)
                    {
                        return new byte[] { (byte)(0xc5 | (left.Address << 4)) };
                    }

                    throw new Exception("Register expected: bc, de, hl, af");
                })
            })

            //push i
            .AddInstruction(new CpuInstructionType()
            {
                Mnemonic = "push",
                Operand = new Type[] { typeof(Commons.Z80Index) },
                Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                    var left = (Z80Index)x.LeftValue;

                    return new byte[] { left.Address, 0xe5 };
                })
            })

            //pop dd
            .AddInstruction(new CpuInstructionType()
            {
                Mnemonic = "pop",
                Operand = new Type[] { typeof(Commons.Z80PairRegister)},
                Size = new Func<IInstructionArgument, int>((x) => { return 1; }),
                Constraints = new ICommonType[] { Z80PairRegister.BC, Z80PairRegister.DE, Z80PairRegister.HL, Z80PairRegister.AF },
                Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                    var left = (Z80PairRegister)x.LeftValue;

                    if (left == Z80PairRegister.BC ||
                        left == Z80PairRegister.DE ||
                        left == Z80PairRegister.HL ||
                        left == Z80PairRegister.AF)
                    {
                        return new byte[] { (byte)(0xc1 | (left.Address << 4)) };
                    }

                    throw new Exception("Register expected: bc, de, hl, af");
                })
            })

            //pop i
            .AddInstruction(new CpuInstructionType()
            {
                Mnemonic = "pop",
                Operand = new Type[] { typeof(Commons.Z80Index)},
                Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                    var left = (Z80Index)x.LeftValue;

                    return new byte[] { left.Address, 0xe1 };
                })
            });

            return group.Instructions;
        }
    }
}

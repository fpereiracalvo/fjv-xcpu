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
    public class JumpGroup : InstructionSetBase<CpuInstructionType>
    {
        public static List<CpuInstructionType> GetSet()
        {
            var group = new JumpGroup();

            //jp nn (16 bits)
            group.AddInstruction(new CpuInstructionType() {
                    Mnemonic = "jp",
                    Operand = new Type[] { typeof(UInt16) },
                    Size = new Func<IInstructionArgument, int>((x) => { return 3; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var address = (UInt16)x.LeftValue;

                        var value = BitConverter.GetBytes(address);

                        var hex = new List<byte>() {
                            0xc3
                        };

                        hex.AddRange(value);

                        return hex.ToArray();
                    })
                })

                //jp n (soporte para recibir 8 bits) >> lo pasa a 16bits.
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "jp",
                    Operand = new Type[] { typeof(byte) },
                    Size = new Func<IInstructionArgument, int>((x) => { return 3; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var address = (byte)x.LeftValue;

                        //2 bytes.
                        var value = BitConverter.GetBytes(address);

                        var hex = new List<byte>() {
                            0xc3
                        };

                        hex.AddRange(value);

                        return hex.ToArray();
                    })
                })

                //jp cc, nn
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "jp",
                    Operand = new Type[] { typeof(Commons.Z80TestableFlag), typeof(UInt16) },
                    Size = new Func<IInstructionArgument, int>((x) => { return 3; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var left = (byte)((Z80TestableFlag)x.LeftValue).Address;
                        var address = (UInt16)x.RightValue;

                        var value = BitConverter.GetBytes(address);

                        var hex = new List<byte>() {
                            (byte)(0xc2 | (byte)left << 3)
                        };

                        hex.AddRange(value);

                        return hex.ToArray();
                    })
                })

                //jr e -> sbyte
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "jr",
                    Operand = new Type[] { typeof(sbyte) },
                    RightPointer = false,
                    LeftPointer = false,
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        //var left = ByteHandler.GetSByte((string)x.Left);
                        var left = (sbyte)x.LeftValue;

                        if (left > 127)
                        {
                            throw new Exception("Value expected between: -126 and 129.");
                        }

                        return new byte[] { 0x18, (byte)(left - 2) };
                    })
                })

                //jr e -> byte
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "jr",
                    Operand = new Type[] { typeof(byte) },
                    RightPointer = false,
                    LeftPointer = false,
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        try
                        {
                            var left = Convert.ToSByte(x.LeftValue); //(sbyte)x.LeftValue;

                            if (left < -126)
                            {
                                throw new Exception();
                            }

                            return new byte[] { 0x18, (byte)(left - 2) };
                        }
                        catch (Exception)
                        {
                            throw new Exception("Value expected between: -126 and 129.");
                        }
                    })
                })
                    
                //jr cc, e (byte)
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "jr",
                    Operand = new Type[] { typeof (Commons.Z80TestableFlag), typeof(byte) },
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Constraints = new ICommonType[] { Z80TestableFlag.C, Z80TestableFlag.NC, Z80TestableFlag.NZ },
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var left = (Z80TestableFlag)x.LeftValue;
                        var right = (byte)x.RightValue;

                        if (right > 129)
                        {
                            throw new Exception("Value expected between: -126 and 129.");
                        }

                        if (left.Equals(Commons.Z80TestableFlag.C))
                        {
                            return new byte[] { 0x38, (byte)(right - 2) };
                        }
                        else if (left.Equals(Commons.Z80TestableFlag.NC))
                        {
                            return new byte[] { 0x30, (byte)(right - 2) };
                        }
                        else if (left.Equals(Commons.Z80TestableFlag.NZ))
                        {
                            return new byte[] { 0x20, (byte)(right - 2) };
                        }

                        throw new Exception($"Condition expected: {Commons.Z80TestableFlag.C.Mnemonic}, {Commons.Z80TestableFlag.NC.Mnemonic}, {Commons.Z80TestableFlag.NZ.Mnemonic}.");
                    })
                })

                //jr cc, e (sbyte)
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "jr",
                    Operand = new Type[] { typeof(Commons.Z80TestableFlag), typeof(sbyte) },
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Constraints = new ICommonType[] { Z80TestableFlag.C, Z80TestableFlag.NC, Z80TestableFlag.NZ },
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var left = (Z80TestableFlag)x.LeftValue;

                        var right = (sbyte)x.RightValue;

                        if (right < -126)
                        {
                            throw new Exception("Value expected between: -126 and 129.");
                        }

                        if (left.Equals(Commons.Z80TestableFlag.C))
                        {
                            return new byte[] { 0x38, (byte)(right - 2) };
                        }
                        else if (left.Equals(Commons.Z80TestableFlag.NC))
                        {
                            return new byte[] { 0x30, (byte)(right - 2) };
                        }
                        else if (left.Equals(Commons.Z80TestableFlag.NZ))
                        {
                            return new byte[] { 0x20, (byte)(right - 2) };
                        }

                        throw new Exception($"Condition expected: {Commons.Z80TestableFlag.C.Mnemonic}.");
                    })
                })

                //jp z, e (byte)
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "jp",
                    Operand = new Type[] { typeof(Commons.Z80TestableFlag), typeof(byte) },
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Constraints = new ICommonType[] { Z80TestableFlag.Z },
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var left = (Z80TestableFlag)x.LeftValue;
                        var right = (byte)x.RightValue;

                        if (right > 129)
                        {
                            throw new Exception("Values expected between: -126 and 129.");
                        }

                        if (left == Z80TestableFlag.Z)
                        {
                            return new byte[] { 0x28, (byte)(right - 2) };
                        }

                        throw new Exception($"Condition expected: {Commons.Z80TestableFlag.Z.Mnemonic}.");
                    })
                })

                //jp z, e (sbyte)
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "jp",
                    Operand = new Type[] { typeof(Commons.Z80TestableFlag), typeof(sbyte) },
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Constraints = new ICommonType[] { Z80TestableFlag.Z },
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var left = (Z80TestableFlag)x.LeftValue;

                        var right = (sbyte)x.RightValue;

                        if (right < -126)
                        {
                            throw new Exception("Values expected between: -126 and 129.");
                        }

                        if (left == Z80TestableFlag.Z)
                        {
                            return new byte[] { 0x28, (byte)(right - 2) };
                        }

                        throw new Exception($"Condition expected: {Commons.Z80TestableFlag.Z.Mnemonic}.");
                    })
                })

                //jp hl
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "jp",
                    Operand = new Type[] { typeof(Commons.Z80PairRegister) },
                    Size = new Func<IInstructionArgument, int>((x) => { return 1; }),
                    Constraints = new ICommonType[] { Z80PairRegister.HL },
                    LeftPointer = true,
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var left = (Z80PairRegister)x.LeftValue;

                        if (left == Commons.Z80PairRegister.HL)
                        {
                            return new byte[] { 0xe9 };
                        }

                        throw new Exception($"Register expected: {Commons.Z80PairRegister.HL}");
                    })
                })

                //jp i
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "jp",
                    Operand = new Type[] { typeof(Commons.Z80Index) },
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    LeftPointer = true,
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var i = ((string)x.Left).Replace("(", "").Replace(")", "").Trim();

                        var left = Commons.Z80Index.GetIndex(i);

                        return new byte[] { left.Address, 0xe9 };
                    })
                })

                // djnz e
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "djnz",
                    Operand = new Type[] { typeof(byte) },
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var left = (byte)x.LeftValue;

                        if (left > 127)
                        {
                            throw new Exception("Values expected between: -126 and 129.");
                        }

                        return new byte[] { 0x18, (byte)(left - 2) };
                    })
                })

                //djnz e
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "djnz",
                    Operand = new Type[] { typeof(sbyte) },
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        try
                        {
                            var left = (sbyte)x.LeftValue;

                            if (left < -126)
                            {
                                throw new Exception();
                            }

                            return new byte[] { 0x18, (byte)(left - 2) };
                        }
                        catch
                        {
                            throw new Exception("Values expected between: -126 and 129.");
                        }
                    })
                });

            return group.Instructions;
        }
    }
}

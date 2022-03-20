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
    public class _8BitLoadGroup : InstructionSetBase<CpuInstructionType>
    {
        public static List<CpuInstructionType> GetSet()
        {
            var group = new _8BitLoadGroup();

            var mnemonic = "ld";

            //ld r,r'
            group.AddInstruction(new CpuInstructionType() {
                    Mnemonic = mnemonic,
                    Operand = new Type[] { typeof(Z80Register), typeof(Z80PrimeRegister) },
                    Size = new Func<IInstructionArgument, int>((x) => { return 1; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        byte hex = 0x40;

                        var left = (byte)((Z80Register)x.LeftValue).Address;
                        var right = (byte)((Z80PrimeRegister)x.RightValue).Address;


                        left = (byte)(left << 3);
                        hex = (byte)(hex | left | right);

                        return new byte[1] { hex };
                    })
                })

                //ld r,n
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = mnemonic,
                    Operand = new Type[] { typeof(Z80Register), typeof(byte) },
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var registro = (Z80Register)x.LeftValue;
                        var right = (byte)x.RightValue;

                        byte hex = 0x06;

                        var left = (byte)(registro.Address << 3);
                        hex = (byte)(hex | left);

                        return new byte[2] { hex, right };
                    })
                })

                //ld r,(hl) / ld a,(bc) / ld a,(de) -> (a == r)
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = mnemonic,
                    Operand = new Type[] { typeof(Commons.Z80Register), typeof(Commons.Z80PairRegister) },
                    RightPointer = true,
                    Size = new Func<IInstructionArgument, int>((x) => { return 1; }),
                    Constraints = new ICommonType[] { Z80PairRegister.HL, Z80Register.A, Z80PairRegister.BC, Z80PairRegister.DE },
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var left = (Z80Register)x.LeftValue;
                        var right = (Z80PairRegister)x.RightValue;

                        if (right == Commons.Z80PairRegister.HL)
                        {
                            byte hex = 0x46;

                            hex = (byte)(hex | (left.Address << 3));

                            return new byte[1] { hex };
                        }
                        else if (left == Commons.Z80Register.A && (right == Commons.Z80PairRegister.BC || right == Commons.Z80PairRegister.DE))
                        {
                            byte hex = 0x0a;

                            hex = (byte)(hex | (right.Address << 4));

                            return new byte[1] { hex };
                        }

                        throw new Exception("Register expected: a, hl, bc o de.");
                    })
                })

                //ld (hl),r / ld (bc),a / ld (de),a -> (a == r)
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = mnemonic,
                    Operand = new Type[] { typeof(Z80PairRegister), typeof(Z80Register) },
                    LeftPointer = true,
                    Size = new Func<IInstructionArgument, int>((x) => { return 1; }),
                    Constraints = new ICommonType[] { Z80PairRegister.HL, Z80Register.A, Z80PairRegister.BC, Z80PairRegister.DE },
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var left = (Z80PairRegister)x.LeftValue;
                        var right = (Z80Register)x.RightValue;

                        if (left == Z80PairRegister.HL)
                        {
                            byte hex = 0x70;

                            hex = (byte)(hex | (right.Address));

                            return new byte[1] { hex };
                        }
                        else if (right == Z80Register.A && (left == Z80PairRegister.BC || left == Z80PairRegister.DE))
                        {
                            byte hex = 0x02;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      

                            hex = (byte)(hex | (left.Address << 4));

                            return new byte[1] { hex };
                        }

                        //todo: agregar código de error.
                        throw new Exception("Register expected: a, hl, bc o de.");
                    })
                })

                //ld (hl),n
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = mnemonic,
                    Operand = new Type[] { typeof(Commons.Z80PairRegister), typeof(byte) },
                    LeftPointer = true,
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Constraints = new ICommonType[] { Z80PairRegister.HL },
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var left = (Z80PairRegister)x.LeftValue;

                        if (left == Z80PairRegister.HL)
                        {
                            byte hex = 0x36;
                            byte rigth = (byte)x.RightValue;

                            return new byte[2] { hex, rigth };
                        }

                        //todo: agregar código de error.
                        throw new Exception("Register expected: hl");
                    })
                })

                //ld r,(ix+d) / ld r,(iy+d)
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = mnemonic,
                    Operand = new Type[] { typeof(Z80Register), typeof(Z80Index) },
                    Size = new Func<IInstructionArgument, int>((x) => { return 3; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var i = ((string)x.Right).Replace("(", "").Replace(")", "").Trim();
                        var left = (byte)((Z80Register)x.LeftValue).Address;
                        var split = i.Split('+');

                        var right = Z80Index.GetIndex(split[0].Trim());

                        var hex = new List<byte>() {
                            right.Address,
                            (byte)(0x46 | (left << 3))
                        };

                        if (i.IndexOf("+") > 0)
                        {
                            var d = split.LastOrDefault().Trim();

                            hex.Add(Bytes.GetByte(d));

                            return hex.ToArray();
                        }

                        throw new Exception($"Was expected ({right.Mnemonic}+n), n = 0 ~255.");
                    })
                })

                //ld (ix+d),r / ld (iy+d),r
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = mnemonic,
                    Operand = new Type[] { typeof(Commons.Z80Index), typeof(Commons.Z80Register) },
                    Size = new Func<IInstructionArgument, int>((x) => { return 3; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var i = ((string)x.Left).Replace("(", "").Replace(")", "").Trim();
                        var right = (byte)((Z80Register)x.RightValue).Address;
                        var split = i.Split('+');

                        var hex = new List<byte>() {
                            Commons.Z80Index.GetIndex(split[0].Trim()).Address,
                            (byte)(0x70 | (right))
                        };

                        if (i.IndexOf("+") > 0)
                        {
                            var d = split.LastOrDefault().Trim();

                            hex.Add(Bytes.GetByte(d));

                            return hex.ToArray();
                        }

                        return null;
                    })
                })

                //ld (ix+d),n / ld (iy+d),n
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = mnemonic,
                    Operand = new Type[] { typeof(Commons.Z80Index), typeof(byte) },
                    Size = new Func<IInstructionArgument, int>((x) => { return 4; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var i = ((string)x.Left).Replace("(", "").Replace(")", "").Trim();
                        var n = (byte)x.RightValue;

                        var split = i.Split('+');

                        var hex = new List<byte>() {
                            Commons.Z80Index.GetIndex(split[0].Trim()).Address,
                            0x36
                        };

                        if (i.IndexOf("+") > 0)
                        {
                            var d = split.LastOrDefault().Trim();

                            hex.Add(Bytes.GetByte(d));

                            hex.Add(n);

                            return hex.ToArray();
                        }

                        throw new Exception("An index was expected.");
                    })
                })

                //ld a,(nn)
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = mnemonic,
                    Operand = new Type[] { typeof(Commons.Z80Register), typeof(UInt16) },
                    RightPointer = true,
                    Size = new Func<IInstructionArgument, int>((x) => { return 3; }),
                    Constraints = new ICommonType[] { Z80Register.A },
                    Instruction = new Func<IInstructionArgument, byte[]>((x) =>
                    {
                        var left = (Z80Register)x.LeftValue;
                        var right = (UInt16)x.RightValue;

                        if (left == Z80Register.A)
                        {
                            return new byte[] { 0x3a }.Concat(BitConverter.GetBytes(right)).ToArray();
                        }

                        throw new Exception("Register expected: a.");
                    })
                })

                //ld (nn),a
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = mnemonic,
                    Operand = new Type[] { typeof(UInt16), typeof(Commons.Z80Register) },
                    LeftPointer = true,
                    Size = new Func<IInstructionArgument, int>((x) => { return 3; }),
                    Constraints = new ICommonType[] { Z80Register.A },
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var right = (Z80Register)x.RightValue;

                        if (right == Z80Register.A)
                        {
                            var left = (UInt16)x.LeftValue;

                            var bytes = BitConverter.GetBytes(left);

                            var hex = new List<byte>() {
                                0x32
                            };

                            hex.AddRange(bytes.ToList());

                            return hex.ToArray();
                        }

                        throw new Exception($"Register expected: a.");
                    })
                })

                //ld a,i / ld a,r / ld i,a / ld r,a
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = mnemonic,
                    Operand = new Type[] { typeof(Commons.Z80Register), typeof(Commons.Z80Register) },
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Constraints = new ICommonType[] { Z80Register.A, Z80Register.I, Z80Register.R },
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var left = (Z80Register)x.LeftValue;
                        var right = (Z80Register)x.RightValue;

                        var hex = new List<byte>() {
                            0xed
                        };

                        if (left == Commons.Z80Register.A && right == Commons.Z80Register.I)
                        {
                            hex.Add(0x57);
                            return hex.ToArray();
                        }
                        else if (left == Commons.Z80Register.A && right == Commons.Z80Register.R)
                        {
                            hex.Add(0x5f);
                            return hex.ToArray();
                        }
                        else if (right == Commons.Z80Register.A && left == Commons.Z80Register.I)
                        {
                            hex.Add(0x47);
                            return hex.ToArray();
                        }
                        else if (right == Commons.Z80Register.A && left == Commons.Z80Register.R)
                        {
                            hex.Add(0x4f);
                            return hex.ToArray();
                        }

                        throw new Exception("Register expected: a, i, r");
                    })
                });

            return group.Instructions;
        }
    }
}

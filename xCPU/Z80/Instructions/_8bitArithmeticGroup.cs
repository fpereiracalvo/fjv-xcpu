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
    public class _8bitArithmeticGroup : InstructionSetBase<CpuInstructionType>
    {
        private static Z80Register[] registers = new Z80Register[] {
            Z80Register.A,
            Z80Register.B,
            Z80Register.C,
            Z80Register.D,
            Z80Register.E,
            Z80Register.H,
            Z80Register.L,
        };

        public static List<CpuInstructionType> GetSet()
        {
            var group = new _8bitArithmeticGroup();

            group.AddInstructions(CreateExtensionInstructions("add", 0x00));
            group.AddInstructions(CreateExtensionInstructions("adc", 0x01));
            group.AddInstructions(CreateExtensionInstructionsWithoutA("sub", 0x02));
            group.AddInstructions(CreateExtensionInstructions("sbc", 0x03));
            group.AddInstructions(CreateExtensionInstructionsWithoutA("and", 0x04));
            group.AddInstructions(CreateExtensionInstructionsWithoutA("or",  0x05));
            group.AddInstructions(CreateExtensionInstructionsWithoutA("xor", 0x06));
            group.AddInstructions(CreateExtensionInstructionsWithoutA("cp",  0x07));

            group.AddInstructions(CreateEffectInstructions("inc",  0x04));
            group.AddInstructions(CreateEffectInstructions("dec",  0x05));

            return group.Instructions;
        }

        private static List<CpuInstructionType> CreateExtensionInstructions(string mnemonic, byte operation)
        {
            var arithmetic = new _8bitArithmeticGroup();

            //xxx r, r
            arithmetic
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = mnemonic,
                    Operand = new Type[] { typeof(Z80Register), typeof(Z80Register) },
                    Size = new Func<IInstructionArgument, int>((s) => { return 1; }),
                    Constraints = new ICommonType[] { Z80Register.A },
                    Instruction = new Func<IInstructionArgument, byte[]>((x) =>
                    {
                        var left = (Z80Register)x.LeftValue;
                        var right = (Z80Register)x.RightValue;

                        if (left == Z80Register.A)
                        {
                            if (registers.Contains(right))
                            {
                                return new byte[] { (byte)((0x80 | operation << 3) | right.Address) };
                            }

                            throw new Exception($"Register expected: {string.Join(",", registers.Select(s => s.Mnemonic).ToList())}");
                        }

                        throw new Exception("Register expected: a.");
                    })
                })

                //xxx r, n
                .AddInstruction(new CpuInstructionType()
                {
                    Mnemonic = mnemonic,
                    Operand = new Type[] { typeof(Z80Register), typeof(byte) },
                    Size = new Func<IInstructionArgument, int>((s) => { return 2; }),
                    Constraints = new ICommonType[] { Z80Register.A },
                    Instruction = new Func<IInstructionArgument, byte[]>((x) =>
                    {
                        var left = (Z80Register)x.LeftValue;
                        var right = (byte)x.RightValue;

                        if (left == Z80Register.A)
                        {
                            return new byte[] { (byte)(0xc6 | operation << 3), right };
                        }

                        throw new Exception("Register expected: a.");
                    })
                })

                //xxx a, (hl)
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = mnemonic,
                    Operand = new Type[] { typeof(Z80Register), typeof(Z80PairRegister) },
                    RightPointer = true,
                    Size = new Func<IInstructionArgument, int>((s) => { return 1; }),
                    Constraints = new ICommonType[] { Z80Register.A, Z80PairRegister.HL },
                    Instruction = new Func<IInstructionArgument, byte[]>((x) =>
                    {
                        var left = (Z80Register)x.LeftValue;
                        var right = (Z80PairRegister)x.RightValue;

                        if (left == Z80Register.A)
                        {
                            if (right == Z80PairRegister.HL)
                            {
                                return new byte[] { (byte)(0x86 | operation << 3) };
                            }

                            throw new Exception("Register expected: hl.");
                        }

                        throw new Exception("Register expected: a.");
                    })
                })

                //xxx a, (ix+d) 
                //xxx a, (iy+d)
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = mnemonic,
                    Operand = new Type[] { typeof(Z80Register), typeof(Commons.Z80Index) },
                    Size = new Func<IInstructionArgument, int>((s) => { return 3; }),
                    Constraints = new ICommonType[] { Z80Register.A, },
                    Instruction = new Func<IInstructionArgument, byte[]>((x) =>
                    {
                        var i = ((string)x.Right).Replace("(", "").Replace(")", "").Trim();
                        var split = i.Split('+');

                        var left = (Z80Register)x.LeftValue;
                        var right = Z80Index.GetIndex(split[0].Trim());

                        if (left == Z80Register.A)
                        {
                            byte d = 0;
                            if (i.IndexOf("+") > 0)
                            {
                                d = Bytes.GetByte(split.LastOrDefault().Trim());
                            }

                            return new byte[] { right.Address, (byte)(0x86 | operation << 3), d };
                        }

                        throw new Exception("Register expected: a.");
                    })
                });

            return arithmetic.Instructions;
        }

        private static List<CpuInstructionType> CreateExtensionInstructionsWithoutA(string mnemonic, byte operation)
        {
            var arithmetic = new _8bitArithmeticGroup();

            //xxx r
            arithmetic.AddInstruction(new CpuInstructionType() {
                    Mnemonic = mnemonic,
                    Operand = new Type[] { typeof(Z80Register) },
                    Size = new Func<IInstructionArgument, int>((s) => { return 1; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var left = (Z80Register)x.LeftValue;

                        if (registers.Contains(left))
                        {
                            return new byte[] { (byte)((0x80 | operation << 3) | left.Address) };
                        }

                        throw new Exception($"Register expected: {string.Join(",", registers.Select(s => s.Mnemonic).ToList())}");
                    })
                })

                //xxx n
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = mnemonic,
                    Operand = new Type[] { typeof(byte) },
                    Size = new Func<IInstructionArgument, int>((s) => { return 2; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var left = (byte)x.LeftValue;

                        return new byte[] { (byte)(0xc6 | operation << 3), left };
                    })
                })

                //xxx (hl)
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = mnemonic,
                    Operand = new Type[] { typeof(Z80PairRegister) },
                    LeftPointer = true,
                    Constraints = new ICommonType[] { Z80PairRegister.HL, },
                    Size = new Func<IInstructionArgument, int>((s) => { return 1; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var left = (Z80PairRegister)x.LeftValue;

                        if (left == Z80PairRegister.HL)
                        {
                            return new byte[] { (byte)(0x86 | operation << 3) };
                        }

                        throw new Exception("Register expected: hl.");
                    })
                })

                //xxx (ix+d)
                //xxx (iy+d)
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = mnemonic,
                    Operand = new Type[] { typeof(Z80Index) },
                    Size = new Func<IInstructionArgument, int>((s) => { return 3; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var i = ((string)x.Left).Replace("(", "").Replace(")", "").Trim();
                        var split = i.Split('+');

                        var left = Z80Index.GetIndex(split[0].Trim());

                        byte d = 0;
                        if (i.IndexOf("+") > 0)
                        {
                            d = Bytes.GetByte(split.LastOrDefault().Trim());
                        }

                        return new byte[] { left.Address, (byte)(0x86 | operation << 3), d };
                    })
                });

            return arithmetic.Instructions;
        }

        private static List<CpuInstructionType> CreateEffectInstructions(string mnemonic, byte operation)
        {
            var arithmetic = new _8bitArithmeticGroup();

            //xxx r
            arithmetic.AddInstruction(new CpuInstructionType() {
                    Mnemonic = mnemonic,
                    Operand = new Type[] { typeof(Z80Register) },
                    Size = new Func<IInstructionArgument, int>((s) => { return 1; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var left = (Z80Register)x.LeftValue;

                        return new byte[] { (byte)(0x00 | (left.Address << 3) | operation)};
                    })
                })

                //xxx (hl)
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = mnemonic,
                    Operand = new Type[] { typeof(Z80PairRegister) },
                    LeftPointer = true,
                    Constraints = new ICommonType[] { Z80PairRegister.HL, },
                    Size = new Func<IInstructionArgument, int>((s) => { return 1; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var left = (Z80PairRegister)x.LeftValue;

                        if (left == Z80PairRegister.HL)
                        {
                            return new byte[] { (byte)(0x30 | operation) };
                        }

                        throw new Exception("Register expected: hl.");
                    })
                })

                //xxx (ix+d)
                //xxx (iy+d)
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = mnemonic,
                    LeftPointer = true,
                    Operand = new Type[] { typeof(Z80Index) },
                    Size = new Func<IInstructionArgument, int>((s) => { return 3; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var i = ((string)x.Left).Replace("(", "").Replace(")", "").Trim();
                        var split = i.Split('+');

                        var left = Z80Index.GetIndex(split[0].Trim());

                        byte d = 0;
                        if (i.IndexOf("+") > 0)
                        {
                            d = Bytes.GetByte(split.LastOrDefault().Trim());
                        }

                        return new byte[] { left.Address, (byte)(0x30 | operation), d };
                    })
                });

            return arithmetic.Instructions;
        }
    }
}

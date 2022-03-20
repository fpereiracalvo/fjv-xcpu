using Fjv.xCPU.CodeProvider;
using Fjv.xCPU.CodeProvider.Commons;
using Fjv.xCPU.CodeProvider.Tools;
using Fjv.Z80.Commons;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fjv.Z80.Instructions
{
    public class BitSetResetAndTestGroup : InstructionSetBase<CpuInstructionType>
    {
        public static List<CpuInstructionType> GetSet()
        {
            var group = new BitSetResetAndTestGroup();

            group.AddInstructions(GetInstructionsByCode("bit", 0x01));
            group.AddInstructions(GetInstructionsByCode("set", 0x03));
            group.AddInstructions(GetInstructionsByCode("res", 0x02));

            return group.Instructions;
        }

        private static List<CpuInstructionType> GetInstructionsByCode(string mnemonic, byte code)
        {
            var shiftedCode = (code << 6);

            var group = new BitSetResetAndTestGroup()
                .AddInstruction(new CpuInstructionType
                {
                    Mnemonic = mnemonic,
                    Operand = new Type[] { typeof(byte), typeof(Z80Register) },
                    Size = new Func<IInstructionArgument, int>(x => { return 2; }),
                    Instruction = new Func<IInstructionArgument, byte[]>(x => {
                        var left = (byte)x.LeftValue;
                        var right = (Z80Register)x.RightValue;

                        if (left > 7)
                        {
                            throw new Exception("Value expected between: 0 and 7");
                        }

                        return new byte[] { 0xcb, (byte)(shiftedCode | (left << 3) | (right.Address)) };
                    })
                })
                .AddInstruction(new CpuInstructionType
                {
                    Mnemonic = mnemonic,
                    Operand = new Type[] { typeof(byte), typeof(Z80PairRegister) },
                    RightPointer = true,
                    Size = new Func<IInstructionArgument, int>(x => { return 2; }),
                    Constraints = new ICommonType[] { Z80PairRegister.HL },
                    Instruction = new Func<IInstructionArgument, byte[]>(x => {
                        var left = (byte)x.LeftValue;
                        var right = (Z80PairRegister)x.RightValue;

                        if (left > 7)
                        {
                            throw new Exception("Value expected between: 0 and 7");
                        }

                        if (right == Z80PairRegister.HL)
                        {
                            return new byte[] { 0xcb, (byte)((shiftedCode | 0x06) | (left << 3)) };
                        }

                        throw new Exception("Register expected: hl.");
                    })
                })
                .AddInstruction(new CpuInstructionType
                {
                    Mnemonic = mnemonic,
                    Operand = new Type[] { typeof(byte), typeof(Z80Index) },
                    RightPointer = true,
                    Size = new Func<IInstructionArgument, int>(x => { return 4; }),
                    Instruction = new Func<IInstructionArgument, byte[]>(x => {
                        var left = (byte)x.LeftValue;

                        if (left > 7)
                        {
                            throw new Exception("Value expected between: 0 and 7");
                        }

                        var right = (Z80Index)x.RightValue;

                        var i = (x.Right).Replace("(", "").Replace(")", "").Trim();
                        var split = i.Split('+');

                        if (i.IndexOf("+") > 0)
                        {
                            var d = Bytes.GetByte(split.LastOrDefault().Trim());

                            return new byte[] { right.Address, 0xcb, d, (byte)((shiftedCode | 0x06) | (left << 3)) };
                        }

                        throw new Exception($"Was expected: ({right.Mnemonic}+n), n = 0 ~255.");
                    })
                });

            return group.Instructions;
        }
    }
}

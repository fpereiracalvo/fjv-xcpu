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
    public class CallReturnGroup : InstructionSetBase<CpuInstructionType>
    {
        public static List<CpuInstructionType> GetSet()
        {
            var group = new CallReturnGroup();

            //call nn
            group.AddInstruction(new CpuInstructionType() {
                    Mnemonic = "call",
                    Operand = new Type[] { typeof(UInt16) },
                    Size = new Func<IInstructionArgument, int>((x) => { return 3; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var address = (UInt16)x.LeftValue;

                        var value = BitConverter.GetBytes(address);
                    
                        var hex = new List<byte>() {
                            0xcd
                        };

                        hex.AddRange(value);

                        return hex.ToArray();
                    })
                })

                //call cc,nn
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "call",
                    Operand = new Type[] { typeof(Commons.Z80TestableFlag), typeof(UInt16) },
                    Size = new Func<IInstructionArgument, int>((x) => { return 3; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var left = (byte)((Z80TestableFlag)x.LeftValue).Address;
                        var address = (UInt16)x.RightValue;

                        var value = BitConverter.GetBytes(address);

                        var hex = new List<byte>() {
                            (byte)(0xc4 | (byte)left << 3)
                        };

                        hex.AddRange(value);

                        return hex.ToArray();
                    })
                })

                //ret
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "ret",
                    Size = new Func<IInstructionArgument, int>((x) => { return 1; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        return new byte[] { 0xc9 };
                    })
                })

                //ret cc -> cc = condition.
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "ret",
                    Operand = new Type[] { typeof(Commons.Z80TestableFlag) },
                    Size = new Func<IInstructionArgument, int>((x) => { return 1; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var left = (byte)((Z80TestableFlag)x.LeftValue).Address;

                        return new byte[] { (byte)(0xc0 | left << 3) };
                    })
                })

                //reti
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "reti",
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        return new byte[] { 0xed, 0x4d };
                    })
                })

                //retn
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "retn",
                    Size = new Func<IInstructionArgument, int>((x) => { return 2; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        return new byte[] { 0xed, 0x45 };
                    })
                })

                //rst n
                .AddInstruction(new CpuInstructionType() {
                    Mnemonic = "rst",
                    Operand = new Type[] { typeof(byte) },
                    Size = new Func<IInstructionArgument, int>((x) => { return 1; }),
                    Instruction = new Func<IInstructionArgument, byte[]>((x) => {
                        var left = (byte)x.LeftValue;

                        var p = new Dictionary<byte, byte>() {
                            { 0x00, 0 },
                            { 0x08, 1 },
                            { 0x10, 2 },
                            { 0x18, 3 },
                            { 0x20, 4 },
                            { 0x28, 5 },
                            { 0x30, 6 },
                            { 0x38, 7 }
                        };

                        if (p.ContainsKey(left))
                        {
                            return new byte[] { (byte)(0xc7 | p[left] << 3) };
                        }

                        throw new Exception($"Value expected: {string.Join(",", p.Select(s=>s.Key).ToList())}.");
                    })
                });

            return group.Instructions;
        }
    }
}

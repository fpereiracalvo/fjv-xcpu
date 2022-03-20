using Fjv.xCPU.CodeProvider;
using Fjv.xCPU.CodeProvider.Commons;
using Fjv.xCPU.CodeProvider.Constants;
using System;
using System.Linq;
using System.Text;

namespace Z80Assembly.Instructions
{
    public class Z80AssemblyInstructionSet : InstructionSetBase<AssemblyInstructionType>
    {
        public override string Name => "Zilog - Z80 Assembler";
        public override event EventHandler<string> OnBeginLongProcess;
        public override event EventHandler<string> OnEndLongProcess;

        public Z80AssemblyInstructionSet()
            : base()
        {
            //org origin
            Instructions.Add(new AssemblyInstructionType()
            {
                Mnemonic = "org",
                Operand = new Type[] { typeof(UInt16) },
                Instruction = new Func<IInstructionArgument, AssemblyDataTypeResult[]>((x) => {
                    var address = (UInt16)x.LeftValue;

                    return new AssemblyDataTypeResult[] {
                        new AssemblyDataTypeResult {
                            Object=(int)address,
                            DataType = DataType.Information
                        }
                    };
                })
            });

            //org origin
            Instructions.Add(new AssemblyInstructionType()
            {
                Mnemonic = "org",
                Operand = new Type[] { typeof(byte) },
                Instruction = new Func<IInstructionArgument, AssemblyDataTypeResult[]>((x) => {
                    var address = (byte)x.LeftValue;

                    return new AssemblyDataTypeResult[] {
                        new AssemblyDataTypeResult {
                            Object=(int)address,
                            DataType = DataType.Information
                        }
                    };
                })
            });

            //equ equate
            Instructions.Add(new AssemblyInstructionType()
            {
                Mnemonic = "equ",
                Operand = new Type[] { typeof(UInt16) },
                Instruction = new Func<IInstructionArgument, AssemblyDataTypeResult[]>((x) => {
                    var value = (UInt16)x.LeftValue;

                    return new AssemblyDataTypeResult[] {
                        new AssemblyDataTypeResult {
                            Object = value,
                            DataType = DataType.Information
                        }
                    };
                })
            });

            //equ equate
            Instructions.Add(new AssemblyInstructionType()
            {
                Mnemonic = "equ",
                Operand = new Type[] { typeof(byte) },
                Instruction = new Func<IInstructionArgument, AssemblyDataTypeResult[]>((x) => {
                    var value = (byte)x.LeftValue;

                    return new AssemblyDataTypeResult[] {
                        new AssemblyDataTypeResult {
                            Object = value,
                            DataType = DataType.Information
                        }
                    };
                })
            });

            //ext "<external or remote file>"
            Instructions.Add(new AssemblyInstructionType()
            {
                Mnemonic = "ext",
                RegexPattern = "\"(.*?)\"",
                Instruction = new Func<IInstructionArgument, AssemblyDataTypeResult[]>((x) => {
                    var source = x.Left?.Replace("\"", "");
                    var sourcepath = x.Source;

                    Uri.TryCreate(source, UriKind.Absolute, out Uri uri);

                    var file = System.IO.Path.Combine(sourcepath, source);

                    if (System.IO.File.Exists(source))
                    {
                        return new AssemblyDataTypeResult[] {
                            new AssemblyDataTypeResult {
                                Object = System.IO.File.ReadAllLines(source).ToArray(),
                                DataType = DataType.Code
                            }
                        };
                    }
                    else if (System.IO.File.Exists(file))
                    {
                        return new AssemblyDataTypeResult[] {
                            new AssemblyDataTypeResult {
                                Object = System.IO.File.ReadAllLines(file).ToArray(),
                                DataType = DataType.Code
                            }
                        };
                    }
                    else if (uri.IsAbsoluteUri)
                    {
                        OnBeginLongProcess?.Invoke(this, $"Load {uri.AbsoluteUri}...");

                        var client = new System.Net.WebClient();
                        string downloadString = client.DownloadString(uri);

                        var temp = System.IO.Path.GetTempFileName();
                        System.IO.File.WriteAllText(temp, downloadString);

                        OnEndLongProcess?.Invoke(this, $"{uri.AbsoluteUri} ok.");

                        return new AssemblyDataTypeResult[] {
                            new AssemblyDataTypeResult {
                                Object = System.IO.File.ReadAllLines(temp),
                                DataType = DataType.Code
                            }
                        };
                    }
                    else
                    {
                        throw new Exception($"{file} does not exist.");
                    }
                })
            });

            Instructions.Add(new AssemblyInstructionType()
            {
                Mnemonic = "defm",
                //todo: use regex pattern to recognize concatenation string+string/string+value.
                RegexPattern = "\"(.*?)\"|\'(.*?)\'",
                Instruction = new Func<IInstructionArgument, AssemblyDataTypeResult[]>((x) => {
                    var messages = x.Left.Trim();

                    //todo: recognize C scaped character.

                    if (messages.Length >= 2)
                    {
                        messages = messages.Substring(1, messages.Length - 2);
                    }

                    return new AssemblyDataTypeResult[] {
                        new AssemblyDataTypeResult {
                            Object = Encoding.ASCII.GetBytes(string.Join("", messages)),
                            DataType = DataType.Data
                        }
                    };
                })
            });
        }
    }
}

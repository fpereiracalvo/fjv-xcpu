using Fjv.xCPU.CodeProvider.Commons;
using Fjv.xCPU.CodeProvider.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fjv.xCPU.CodeCompiler
{
    public sealed class SinglePassCompiler
    {
        IInstructionResolverProvider<byte> _cpuCodeResolver;
        IInstructionResolverProvider<AssemblyDataTypeResult> _asmCodeResolver;

        LabelStorage _labelstorage;

        List<LineCode> _linecodes;

        public event EventHandler OnBeginCompile;
        public event EventHandler OnEndCompile;
        public event EventHandler<Exception> OnException;

        public SinglePassCompiler(
            IInstructionResolverProvider<byte> cpuCodeResolver,
            IInstructionResolverProvider<AssemblyDataTypeResult> asmCodeResolver)
        {
            _cpuCodeResolver = cpuCodeResolver;
            _asmCodeResolver = asmCodeResolver;

            _labelstorage = new LabelStorage(_cpuCodeResolver, _asmCodeResolver);

            _cpuCodeResolver.SetLabelProvider(_labelstorage);
            _asmCodeResolver.SetLabelProvider(_labelstorage);

            _labelstorage.OnCallCpuInstruction += Labelstorage_OnCallCpuInstruction;
            _labelstorage.OnCallAssemblyInstruction += Labelstorage_OnCallAssemblyInstruction;

            _linecodes = new List<LineCode>();
        }

        private void Labelstorage_OnCallAssemblyInstruction(object sender, LabelStorage.AssemblyInstructionEventArgs e)
        {
            e.Result = GetAssemblyDataTypeResult(e.Line);
        }

        private void Labelstorage_OnCallCpuInstruction(object sender, LabelStorage.CpuInstructionEventArgs e)
        {
            e.Result = ProcessCpuInstruction(e.Line, e.Index);
        }

        public List<LineCode> GetLineCodes()
        {
            return _linecodes;
        }

        public bool Make(string source, out List<byte> bytes)
        {
            var lines = System.IO.File.ReadAllLines(source);

            return Make(lines, out bytes);
        }

        public bool Make(string[] lines, out List<byte> bytes)
        {
            bytes = new List<byte>();
            bool hasErrors = false;

            OnBeginCompile?.Invoke(this, EventArgs.Empty);

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i].Trim();

                try
                {
                    bytes.AddRange(ProcessLine(i, line, bytes.Count));

                    _labelstorage.ValueReplacement(_linecodes, bytes);
                }
                catch (Exception ex)
                {
                    hasErrors = true;

                    OnException?.Invoke(this, new Exception($"{i + 1}: \"{line}\". {ex.Message}", ex));
                }

                Thread.Sleep(0);
            }

            OnEndCompile?.Invoke(this, EventArgs.Empty);

            if (hasErrors)
            {
                return hasErrors;
            }

            return false;
        }

        private byte[] ProcessLine(int i, string line, int origin)
        {
            var bytes = new List<byte>();

            var index = new Func<int>(() =>
            {
                return bytes.Count + origin;
            });

            if (!string.IsNullOrWhiteSpace(line))
            {
                var isAsmInstruction = _asmCodeResolver.HasInstruction(line);
                var isCpuInstruction = _cpuCodeResolver.HasInstruction(line);

                var new_line = PushLabel(line, origin, bytes, isCpuInstruction, isAsmInstruction);

                if (!new_line.Equals(line))
                {
                    isCpuInstruction = _cpuCodeResolver.HasInstruction(new_line);
                    isAsmInstruction = _asmCodeResolver.HasInstruction(new_line);
                }

                if (isAsmInstruction)
                {
                    ProcessAsmInstruction(new_line, origin, bytes, index);
                }
                else if (isCpuInstruction)
                {
                    var hex = ProcessCpuInstruction(new_line, index());


                    if (hex != null)
                    {
                        bytes.AddRange(hex);
                    }

                    _linecodes.Add(new LineCode(i + 1, index() - hex.Length, line, hex));
                }
            }

            return bytes.ToArray();
        }

        private string PushLabel(
            string line, int origin, List<byte> bytes,
            bool isCpuInstruction, bool isAsmInstruction)
        {
            //si no existen comandos que satisfagan la instrucción se considera label.
            if (!isCpuInstruction && !isAsmInstruction)
            {
                var commandline = new CommandLine(line, new string[] { });

                if (commandline.HasLabel)
                {
                    return _labelstorage.Add(origin, bytes, commandline);
                }
            }

            return line;
        }

        private void ProcessAsmInstruction(string line, int origin, List<byte> bytes, Func<int> index)
        {
            var result = GetAssemblyDataTypeResult(line);

            result.ToList().ForEach(r =>
            {
                switch (r.DataType)
                {
                    case DataType.Data:
                        var hex = (byte[])r.Object;

                        bytes.AddRange(hex);

                        _linecodes.Add(new LineCode(0, index() - hex.Length, line, hex));

                        break;
                    case DataType.Code:
                        var _lines_ = (string[])r.Object;
                        var ii = 0;
                        foreach (var l in _lines_)
                        {
                            if (l.Trim().Length > 0)
                            {
                                bytes.AddRange(ProcessLine(ii, l, index()));
                            }

                            Thread.Sleep(0);
                            ii++;
                        }

                        break;
                    case DataType.Information:
                        var size = (int)r.Object;
                        if (index() > size)
                        {
                            throw new Exception($"The current code length is greater than the specified origin: [{index()}] > [{origin}]");
                        }

                        bytes.AddRange(Enumerable.Range(0, size - index()).Select(s => (byte)0).ToList());
                        break;
                    case DataType.Internal:
                        //todo: internal operations.
                        break;
                    default:
                        break;
                }
            });
        }

        private AssemblyDataTypeResult[] GetAssemblyDataTypeResult(string line)
        {
            var asm = _asmCodeResolver.GetInstruction(line);

            if (asm == null)
            {
                throw new Exception("An error exist on the instruction.");
            }

            var result = asm?.Instruction.Invoke(asm.Arguments);

            return result;
        }

        private byte[] ProcessCpuInstruction(string line, int index)
        {
            var instruction = _cpuCodeResolver.GetInstruction(line);

            if (instruction.Replacements.Any())
            {
                var item = new LabelStorage.Label<CpuInstructionType>()
                {
                    Clone = (CpuInstructionType)instruction.Clone(),
                    Index = index
                };

                _labelstorage.Add(item);
                return new byte[instruction.Size(instruction.Arguments)];
            }

            var hex = instruction?.Instruction.Invoke(instruction.Arguments);

            return hex;
        }
    }
}

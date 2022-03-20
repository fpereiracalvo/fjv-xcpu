using Fjv.xCPU.CodeProvider.Commons;
using Fjv.xCPU.CodeProvider.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fjv.xCPU.CodeCompiler
{
    internal class LabelStorage : ILabelProvider
    {
        public class CpuInstructionEventArgs
        {
            public string Line { get; set; }
            public int Index { get; set; }

            public byte[] Result { get; set; }

            public CpuInstructionEventArgs(string line, int index)
            {
                this.Line = line;
                this.Index = index;
            }

        }

        public class AssemblyInstructionEventArgs
        {
            public string Line { get; set; }
            public int Origin { get; set; }

            public AssemblyDataTypeResult[] Result { get; set; }

            public AssemblyInstructionEventArgs(string line, int origin)
            {
                this.Line = line;
                this.Origin = origin;
            }

        }

        Dictionary<string, LabelValue> _labels;
        public Dictionary<string, LabelValue> Labels => _labels;

        public List<Label<CpuInstructionType>> CpuLabels { get; set; }

        public event EventHandler<CpuInstructionEventArgs> OnCallCpuInstruction;
        public event EventHandler<AssemblyInstructionEventArgs> OnCallAssemblyInstruction;

        IInstructionResolverProvider<byte> _cpuCodeResolver;
        IInstructionResolverProvider<AssemblyDataTypeResult> _asmCodeResolver;

        public LabelStorage(IInstructionResolverProvider<byte> cpu, IInstructionResolverProvider<AssemblyDataTypeResult> asm)
        {
            _labels = new Dictionary<string, LabelValue>();

            CpuLabels = new List<Label<CpuInstructionType>>();

            _cpuCodeResolver = cpu;
            _asmCodeResolver = asm;
        }

        public class Label<T>
        {
            public int Index { get; set; }
            public T Clone { get; set; }
        }

        public string Add(int origin, List<byte> bytes, CommandLine commandline)
        {
            var instructionline = $"{commandline.Left} {commandline.Right}".Trim();

            var name = commandline.Label;

            if (_cpuCodeResolver.IsExistCommonTypesMnemonic(name) || _asmCodeResolver.IsExistCommonTypesMnemonic(name))
            {
                throw new Exception($"El nombre {name} no puede ser usado como label.");
            }

            if (_labels.ContainsKey(name))
            {
                throw new Exception($"El nombre {name} ya está en uso.");
            }

            if (string.IsNullOrEmpty(instructionline))
            {
                _labels.Add(name, new LabelValue()
                {
                    Type = typeof(ushort),
                    Value = (ushort)origin
                });
            }
            else if (_cpuCodeResolver.HasInstruction(instructionline))
            {
                _labels.Add(name, new LabelValue()
                {
                    Type = typeof(ushort),
                    Value = (ushort)bytes.Count
                });

                return instructionline;
            }
            else if(_asmCodeResolver.HasInstruction(instructionline))
            {
                var eventArgs = new AssemblyInstructionEventArgs(instructionline, origin);

                OnCallAssemblyInstruction?.Invoke(this, eventArgs);

                var result = eventArgs.Result.SingleOrDefault();

                switch (result.DataType)
                {
                    case DataType.Data:
                    case DataType.Code:
                        _labels.Add(name, new LabelValue()
                        {
                            Type = typeof(ushort),
                            Value = (ushort)bytes.Count
                        });

                        break;
                    case DataType.Information:
                        _labels.Add(name, new LabelValue()
                        {
                            Type = result.Object.GetType(),
                            Value = result.Object
                        });

                        instructionline = string.Empty;

                        break;
                    case DataType.Internal:
                        break;
                    default:
                        break;
                }
            }

            return instructionline;
        }

        internal void Add(Label<CpuInstructionType> item)
        {
            CpuLabels.Add(item);
        }

        internal void UpdateCpuLabels(List<Label<CpuInstructionType>> list)
        {
            CpuLabels = list;
        }

        internal List<Label<CpuInstructionType>> ToList()
        {
            return CpuLabels.Where(s => s.Clone.Replacements.Any()).ToList();
        }

        internal void ValueReplacement(List<LineCode> linecodes, List<byte> bytes)
        {
            var removed = false;

            CpuLabels.ForEach(label =>
            {
                var removeReplacements = new List<string>();
                foreach (var item in label.Clone.Replacements)
                {
                    if (Labels.ContainsKey(item))
                    {
                        removeReplacements.Add(item);

                        removed = true;
                    }
                }

                removeReplacements.ForEach(x => { label.Clone.Replacements.Remove(x); });

                if (label.Clone.Replacements.Count() == 0 && removed)
                {
                    var eventArg = new CpuInstructionEventArgs(label.Clone.Line, label.Index);

                    OnCallCpuInstruction?.Invoke(this, eventArg);

                    var lcode = linecodes.SingleOrDefault(x => x.MemoryIndex == label.Index);

                    for (int i = 0; i < label.Clone.Size(label.Clone.Arguments); i++)
                    {
                        bytes[label.Index + i] = eventArg.Result[i];
                    }

                    if (lcode != null)
                    {
                        lcode.Bytes = eventArg.Result;
                    }
                }
            });

            if (removed)
            {
                UpdateCpuLabels(this.ToList());
            }
        }

        public List<LabelValue> Get()
        {
            return _labels.Select(s => s.Value).ToList();
        }

        public LabelValue Get(string labelname)
        {
            return _labels.ContainsKey(labelname) ? _labels[labelname] : null;
        }

        public Dictionary<string, LabelValue> GetDictionary()
        {
            return _labels;
        }

        public string GetString(string labelname)
        {
            return CodeProvider.Tools.LabelValues.GetLabelValue(labelname, _labels);
        }

        public void Add(string labelname, LabelValue value)
        {
            _labels.Add(labelname, value);
        }
    }
}

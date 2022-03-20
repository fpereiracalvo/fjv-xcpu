using System;
using System.Collections.Generic;
using System.Linq;
using Fjv.xCPU.CodeCompiler;
using Fjv.xCPU.CodeProvider;
using Fjv.xCPU.CodeProvider.Commons;
using Fjv.xCPU.HexFile;

namespace Fjv.Z80Compiler
{
    public class ProgramCompiler
    {
        SinglePassCompiler _singlePassCompiler;

        public SinglePassCompiler SinglePassCompiler => _singlePassCompiler;
        IHexHandler _hexfile;

        public event EventHandler<string> OnException;

        public ProgramCompiler(IProvider<CpuInstructionType, byte> cpuCodeProvider, IProvider<AssemblyInstructionType, AssemblyDataTypeResult> asmCodeProvider)
        {
            _singlePassCompiler = new SinglePassCompiler(cpuCodeProvider.GetInstructionResolverProvider(), asmCodeProvider.GetInstructionResolverProvider());

            _singlePassCompiler.OnException += RaiseOnException;
        }

        public ProgramCompiler(IProvider<CpuInstructionType, byte> cpuCodeProvider, IProvider<AssemblyInstructionType, AssemblyDataTypeResult> asmCodeProvider, IHexHandler hexHandler)
            : this(cpuCodeProvider, asmCodeProvider)
        {
            _hexfile = hexHandler;
        }

        private void RaiseOnException(object sender, Exception e)
        {
            OnException?.Invoke(this, e.Message);
        }

        public bool Make(string asmfile, out byte[] bytes)
        {
            var hasExceptions = _singlePassCompiler.Make(asmfile, out List<byte> listbytes); ;

            bytes = listbytes.ToArray();

            return hasExceptions;
        }

        public void SaveAsHex(string outhex, byte[] bytes)
        {
            _hexfile.Save(outhex, bytes);
        }

        public void SaveAsList(string listfile)
        {
            var text = GetListString();

            System.IO.File.WriteAllText(listfile, text);
        }

        public string GetHexString(byte[] bytes)
        {
            return _hexfile.GetHexString(bytes);
        }

        public string GetListString()
        {
            using (var writer = new System.IO.StringWriter())
            {
                _singlePassCompiler.GetLineCodes().ForEach(line => {
                    writer.Write($"      {line.Line}\n");
                    writer.Write($"{line.MemoryIndex.ToString("X").PadLeft(4, '0')}: ");
                    writer.Write($"{string.Join(" ", line.Bytes.Select(s => $"{s:X}".PadLeft(2, '0')))}\n\n");
                });

                return writer.ToString();
            }
        }
    }
}

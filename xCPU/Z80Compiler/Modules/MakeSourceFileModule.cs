using System;
using Fjv.xCPU.HexFile.Intel;
using Fjv.Modules;
using Fjv.Modules.Attributes;
using Fjv.Z80Compiler;

namespace Z80Compiler.Modules
{
    [Module("-x")]
    public class MakeSourceFileModule : IDefaultModule
    {
        ProgramCompiler _compiler;
        bool _hasException;
        byte[] _bytes;

        public byte[] Load(byte[] input, string[] args, int index)
        {
            var source = System.Text.Encoding.UTF8.GetString(input);

            var cpuCodeProvider = new Fjv.Z80.Provider();
            var asmCodeProvider = new Z80Assembly.Provider();

            _compiler = new ProgramCompiler(cpuCodeProvider, asmCodeProvider, new HexHandler());

            _compiler.OnException += Compiler_OnException;

            Console.WriteLine($"Build:\n\t{source}");

            _hasException = _compiler.Make(source, out _bytes);

            if (_hasException)
            {
                Console.WriteLine("End with errors.");

                return null;
            }

            return _bytes;
        }

        private void Compiler_OnException(object sender, string e)
        {
            Console.WriteLine(e);
        }

        [Option("--h")]
        public byte[] SaveHexOutput(string outfile)
        {
            _compiler.SaveAsHex(outfile, _bytes);

            return _bytes;
        }

        [Option("--l")]
        public byte[] SaveListOutput(string outfile)
        {
            _compiler.SaveAsList(outfile);

            return _bytes;
        }
    }
}

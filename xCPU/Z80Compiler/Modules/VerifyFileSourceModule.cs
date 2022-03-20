using System;
using System.IO;
using Fjv.Modules;
using Fjv.Modules.Attributes;

namespace Z80Compiler.Modules
{
    [Module("-f", Fjv.Modules.Commons.ModuleRunningControl.Input)]
    public class VerifyFileSourceModule : IArgumentableModule
    {
        public byte[] Load(byte[] input, byte[] moduleArgument, string[] args, int index)
        {
            var source = System.Text.Encoding.UTF8.GetString(moduleArgument);

            if (!Path.GetExtension(source).ToLower().Equals(".asm"))
            {
                throw new Exception("The file name hasn't the correct extension.");
            }

            if (!File.Exists(source))
            {
                throw new Exception("The specified source file doesn´t exist.");
            }

            return moduleArgument;
        }
    }
}

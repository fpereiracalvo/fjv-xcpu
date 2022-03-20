using System;
using System.Linq;
using Fjv.xCPU.HexFile.Intel;
using Fjv.Modules;

namespace Fjv.Z80Compiler
{
    class Program
    {
        static int Main(string[] args)
        {
            if (!args.Any())
            {
                return 0;
            }

            var moduleFactory = new ModuleFactory(typeof(Program).Assembly);

            byte[] buffer = moduleFactory.Run(args);

            return 0;
        }

        private static void Compiler_OnException(object sender, string e)
        {
            Console.WriteLine(e);
        }
    }
}

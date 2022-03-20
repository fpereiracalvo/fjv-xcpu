using System;
using System.Collections.Generic;
using System.Diagnostics;
using Fjv.xCPU.CodeProvider;
using Fjv.xCPU.CodeProvider.Commons;
using NUnit.Framework;

namespace Z80Compiler.Test
{
    public class MakeWithValueParametersTest
    {
        IProvider<CpuInstructionType, byte> cpuCodeProvider;
        IProvider<AssemblyInstructionType, AssemblyDataTypeResult> asmCodeProvider;

        [SetUp]
        public void Setup()
        {
            cpuCodeProvider = new Fjv.Z80.Provider();
            asmCodeProvider = new Z80Assembly.Provider();
        }

        [Test]
        public void Make_asm_with_binary_values_test()
        {
            var lines = new string[] {
                "ld (hl), a",
                "ld (hl), 0b00010000",
                "ld (hl), 0b00010001",
                "ld (hl), 0b00010011",
                "ld (hl), 0b00010110",
                "ld (0b0000000010001111), hl",
                "ld (0000000010001111b), hl",
                "add a, (hl)",
                "jr 0b00010000",
                "jr 00010000b"
            };

            var program = new Fjv.Z80Compiler.ProgramCompiler(cpuCodeProvider, asmCodeProvider);

            program.OnException += Program_OnException;

            program.SinglePassCompiler.Make(lines, out List<byte> bytes);
        }

        private void Program_OnException(object sender, string e)
        {
            Debug.WriteLine(e);

            Assert.Fail();
        }
    }
}

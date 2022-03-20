using System;
using System.Collections.Generic;
using System.Diagnostics;
using Fjv.xCPU.CodeProvider;
using Fjv.xCPU.CodeProvider.Commons;
using NUnit.Framework;

namespace Z80Compiler.Test
{
    public class MakeWithPointerTest
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
        public void Make_asm_pointers_test()
        {
            var lines = new string[] {
                "ld (hl), a",
                "ld (hl), 0x10",
                "ld (0x0010), hl",
                "add a, (hl)"
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

        [Test]
        public void Make_asm_with_spaces_in_pointers_test()
        {
            var lines = new string[] {
                "ld (    hl    ), a",
                "ld ( hl ), 0x10",
                "ld ( 0x0010       ), hl",
                "add a, (    hl     )"
            };

            var program = new Fjv.Z80Compiler.ProgramCompiler(cpuCodeProvider, asmCodeProvider);

            program.OnException += Program_OnException_Error;

            program.SinglePassCompiler.Make(lines, out List<byte> bytes);
        }

        private void Program_OnException_Error(object sender, string e)
        {
            Debug.WriteLine(e);

            Assert.Fail();
        }
    }
}

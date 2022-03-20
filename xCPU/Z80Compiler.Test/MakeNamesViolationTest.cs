using System;
using System.Collections.Generic;
using System.Diagnostics;
using Fjv.xCPU.CodeProvider;
using Fjv.xCPU.CodeProvider.Commons;
using NUnit.Framework;

namespace Z80Compiler.Test
{
    public class MakeNamesViolationTest
    {
        int errors;

        IProvider<CpuInstructionType, byte> cpuCodeProvider;
        IProvider<AssemblyInstructionType, AssemblyDataTypeResult> asmCodeProvider;

        [SetUp]
        public void Setup()
        {
            cpuCodeProvider = new Fjv.Z80.Provider();
            asmCodeProvider = new Z80Assembly.Provider();
        }

        [Test]
        public void Make_asm_ix_label_name_test()
        {
            errors = 0;

            var lines = new string[] {
                "ix equ 0x10",
                "iy equ 0x10",
                "a equ 0x00",
                "ld (iy+ix), 0x00",
            };

            var program = new Fjv.Z80Compiler.ProgramCompiler(cpuCodeProvider, asmCodeProvider);

            program.OnException += Program_OnException_Error;

            program.SinglePassCompiler.Make(lines, out List<byte> bytes);

            Assert.AreEqual(lines.Length, errors);
        }

        private void Program_OnException_Error(object sender, string e)
        {
            errors++;
            Debug.WriteLine(e);
        }
    }
}

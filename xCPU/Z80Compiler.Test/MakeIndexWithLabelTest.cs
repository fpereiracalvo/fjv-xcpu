using System;
using System.Collections.Generic;
using System.Diagnostics;
using Fjv.xCPU.CodeProvider;
using Fjv.xCPU.CodeProvider.Commons;
using NUnit.Framework;

namespace Z80Compiler.Test
{
    public class MakeIndexWithLabelTest
    {
        string[] lines;

        IProvider<CpuInstructionType, byte> cpuCodeProvider;
        IProvider<AssemblyInstructionType, AssemblyDataTypeResult> asmCodeProvider;

        [SetUp]
        public void Setup()
        {
            cpuCodeProvider = new Fjv.Z80.Provider();
            asmCodeProvider = new Z80Assembly.Provider();
        }

        [Test]
        public void Make_asm_index_label_test()
        {
            lines = new string[] {
                "test_label equ 0x10",
                "ld (ix+test_label), 0x00",
            };

            var program = new Fjv.Z80Compiler.ProgramCompiler(cpuCodeProvider, asmCodeProvider);

            program.OnException += Program_OnException;

            program.SinglePassCompiler.Make(lines, out List<byte> bytes);
        }

        [Test]
        public void Make_asm_error_index_ushort_test()
        {
            lines = new string[] {
                "test_label equ 0x0010",
                "ld (iy+test_label), 0x00",
            };

            var program = new Fjv.Z80Compiler.ProgramCompiler(cpuCodeProvider, asmCodeProvider);

            program.OnException += Program_OnException_Error;

            program.SinglePassCompiler.Make(lines, out List<byte> bytes);
        }

        private void Program_OnException(object sender, string e)
        {
            Debug.WriteLine(e);

            Assert.Fail();
        }

        private void Program_OnException_Error(object sender, string e)
        {
            Debug.WriteLine(e);

            Assert.Pass();
        }
    }
}

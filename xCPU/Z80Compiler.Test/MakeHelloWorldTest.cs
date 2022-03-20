using System.Diagnostics;
using Fjv.xCPU.CodeProvider;
using Fjv.xCPU.CodeProvider.Commons;
using Fjv.xCPU.HexFile;
using Fjv.xCPU.HexFile.Intel;
using NUnit.Framework;

namespace Z80Compiler.Test
{
    public class MakeHelloWorldTest
    {
        byte[] result = new byte[] {
            0xcd, 0x30, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x68, 0x65, 0x6c, 0x6c, 0x6f, 0x20, 0x77, 0x6f,
            0x72, 0x6c, 0x64, 0x21, 0x2e, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x3e, 0x01, 0xd3, 0x00, 0x3e, 0x0f, 0xd3, 0x00,
            0x21, 0x10, 0x00, 0xc3, 0x3f, 0x00, 0x23, 0x3e,
            0x2e, 0xbe, 0xca, 0x30, 0x00, 0x46, 0x78, 0xd3,
            0x08, 0xc3, 0x3e, 0x00 };
        string basedir;
        string source;

        IProvider<CpuInstructionType, byte> _cpuCodeProvider;
        IProvider<AssemblyInstructionType, AssemblyDataTypeResult> _asmCodeProvider;
        IHexHandler _hexHandler;

        [SetUp]
        public void Setup()
        {
            basedir = System.AppDomain.CurrentDomain.BaseDirectory;
            source = System.IO.Path.Combine(basedir, "Samples", "HelloWorld", "helloworld.asm");

            _cpuCodeProvider = new Fjv.Z80.Provider();
            _asmCodeProvider = new Z80Assembly.Provider();

            _hexHandler = new HexHandler();
        }

        [Test]
        public void Make_asm_code()
        {
            var program = new Fjv.Z80Compiler.ProgramCompiler(_cpuCodeProvider, _asmCodeProvider, _hexHandler);

            program.OnException += Program_OnException_Asm_Code;

            program.Make(source, out byte[] bytes);

            Assert.AreEqual(result.Length, bytes.Length);

            for (int i = 0; i < result.Length; i++)
            {
                Assert.AreEqual(result[i], bytes[i]);
            }
        }

        private void Program_OnException_Asm_Code(object sender, string e)
        {
            Debug.WriteLine(e);

            Assert.Fail();
        }

        [Test]
        public void Generate_hex_string()
        {
            var program = new Fjv.Z80Compiler.ProgramCompiler(_cpuCodeProvider, _asmCodeProvider, _hexHandler);

            program.Make(source, out byte[] bytes);

            var hex = program.GetHexString(bytes);

            IHexHandler hexHandler = new HexHandler();

            var assertHex = hexHandler.GetHexString(result);

            Assert.AreEqual(assertHex.Length, hex.Length);

            for (int i = 0; i < assertHex.Length; i++)
            {
                Assert.AreEqual(assertHex[i], hex[i]);
            }
        }

        [Test]
        public void Generate_lst_string()
        {
            var filelist = System.IO.Path.Combine(basedir, "Samples", "HelloWorld", "helloworld.hex.lst");
            var assertList = System.IO.File.ReadAllText(filelist);

            var program = new Fjv.Z80Compiler.ProgramCompiler(_cpuCodeProvider, _asmCodeProvider, _hexHandler);

            program.Make(source, out byte[] bytes);

            var lst = program.GetListString();

            Assert.AreEqual(assertList.Length, lst.Length);

            for (int i = 0; i < assertList.Length; i++)
            {
                Assert.AreEqual(assertList[i], lst[i]);
            }
        }
    }
}

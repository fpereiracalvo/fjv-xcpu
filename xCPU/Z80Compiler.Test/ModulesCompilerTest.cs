using System;
using Fjv.Modules;
using NUnit.Framework;

namespace Z80Compiler.Test
{
    public class ModulesCompilerTest
    {
        ModuleFactory _moduleFactory;

        string _basedir;
        string _source;

        string _outHexSource;
        string _outHexListSource;

        [SetUp]
        public void Setup()
        {
            _moduleFactory = new ModuleFactory(typeof(Fjv.Z80Compiler.ProgramCompiler).Assembly);

            _basedir = System.AppDomain.CurrentDomain.BaseDirectory;
            _source = System.IO.Path.Combine(_basedir, "Samples", "HelloWorld", "helloworld.asm");

            _outHexSource = "out.hex";
            _outHexListSource = "out.lst";

            DeleteFiles(_outHexSource);
            DeleteFiles(_outHexListSource);
        }

        private void DeleteFiles(string path)
        {
            try
            {
                System.IO.File.Delete(path);
            }
            catch
            { }
        }

        [Test]
        public void MakeHexArgumentedTest()
        {
            var args = new string[] { "-f", _source, "-x", "--h", _outHexSource };

            byte[] buffer = _moduleFactory.Run(args);

            if (buffer == null)
            {
                Assert.Fail();
            }

            if (!System.IO.File.Exists(_outHexSource))
            {
                Assert.Fail();
            }
        }

        [Test]
        public void MakeHexListArgumentedTest()
        {
            var args = new string[] { "-f", _source, "-x", "--l", _outHexListSource };

            byte[] buffer = _moduleFactory.Run(args);

            if (buffer == null)
            {
                Assert.Fail();
            }

            if (!System.IO.File.Exists(_outHexListSource))
            {
                Assert.Fail();
            }
        }

        [Test]
        public void MakeHexAndHexListArgumentedTest()
        {
            var args = new string[] { "-f", _source, "-x", "--h", _outHexSource, "--l", _outHexListSource };

            byte[] buffer = _moduleFactory.Run(args);

            if (buffer == null)
            {
                Assert.Fail();
            }

            if (!System.IO.File.Exists(_outHexSource) || !System.IO.File.Exists(_outHexListSource))
            {
                Assert.Fail();
            }
        }
    }
}

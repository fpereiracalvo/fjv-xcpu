using Fjv.xCPU.CodeProvider;
using Fjv.xCPU.CodeProvider.Commons;
using Fjv.Z80.Commons;
using Fjv.Z80.Instructions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Z80.Test
{
	public class Z80TestLineCode
	{
		Z84C00InstructionSet cpuIinstructionSet;
		ILabelProvider labelProvider;

        public class LabelProvider : ILabelProvider
        {
			Dictionary<string, LabelValue> labels = new Dictionary<string, LabelValue>();

			public void Add(string labelname, LabelValue value)
            {
				labels.Add(labelname, value);

			}

            public List<LabelValue> Get()
            {
				return labels.Select(s => s.Value).ToList();
            }

            public LabelValue Get(string labelname)
            {
				return labels.ContainsKey(labelname) ? labels[labelname] : null;
            }

            public Dictionary<string, LabelValue> GetDictionary()
            {
				return labels;
            }

            public string GetString(string labelname)
            {
				return Fjv.xCPU.CodeProvider.Tools.LabelValues.GetLabelValue(labelname, labels);
            }
        }

        [SetUp]
		public void Setup()
		{
			cpuIinstructionSet = new Z84C00InstructionSet();
			labelProvider = new LabelProvider();
		}

		[Test]
		public void Test_line_of_code_with_operations()
		{
			CommandLine commandline;

			var mnemonics = cpuIinstructionSet.GetMnemonics();

			commandline = new CommandLine("ld 0x30 + 10, label", mnemonics);

			Assert.AreEqual(commandline.Command, "ld");
			Assert.AreEqual(commandline.Left, "0x30 + 10");
			Assert.AreEqual(commandline.Right, "label");

			commandline = new CommandLine("ld a, b'", mnemonics);

			Assert.AreEqual(commandline.Command, "ld");
			Assert.AreEqual(commandline.Left, "a");
			Assert.AreEqual(commandline.Right, "b'");

			commandline = new CommandLine("ld b, (hl)", mnemonics);

            Assert.AreEqual(commandline.Command, "ld");
            Assert.AreEqual(commandline.Left, "b");
            Assert.AreEqual(commandline.Right, "(hl)");
        }

		[Test]
		public void Test_line_of_code_with_labels()
		{
			CommandLine<CpuInstructionType, byte> commandline;

			labelProvider.Add("holaMundo", new LabelValue
			{
				Type = typeof(byte),
				Value = 0x10
			});

			labelProvider.Add("label", new LabelValue
			{
				Type = typeof(byte),
				Value = 0x01
			});

			labelProvider.Add("uint16", new LabelValue
			{
				Type = typeof(ushort),
				Value = (ushort)0x0100
			});

			var mnemonics = cpuIinstructionSet.GetMnemonics();

			commandline = new CommandLine<CpuInstructionType, byte>("ld b, (ix+holaMundo)", cpuIinstructionSet, labelProvider);
			var left = commandline.LeftArgument;
			var right = commandline.RightArgument;

			Assert.AreEqual(left.Code, "b");
			Assert.AreEqual(right.Code, "(ix+0x10)");

			commandline = new CommandLine<CpuInstructionType, byte>("out (label), a", cpuIinstructionSet, labelProvider);
			left = commandline.LeftArgument;
			right = commandline.RightArgument;

			Assert.AreEqual(left.Code, "(0x01)");
			Assert.AreEqual(right.Code, "a");

			commandline = new CommandLine<CpuInstructionType, byte>("jp uint16", cpuIinstructionSet, labelProvider);
			left = commandline.LeftArgument;

			Assert.AreEqual(left.Code, "0x0100");
		}
	}
}

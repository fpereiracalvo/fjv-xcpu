using Fjv.Z80.Commons;
using Fjv.Z80.Instructions;
using NUnit.Framework;
using System;
using System.Diagnostics;

namespace Z80.Test
{
    public class Z80TestMethodsGeneration
    {
		/// <summary>
		/// Test methods generation.
		/// </summary>
		[Test]
		public void GetInstructionFileMethodsTest()
		{
			var setup = Fjv.xCPU.CodeProvider.Tools.CpuInstructionTestMethodGeneration.GetSetup<Z84C00InstructionSet>();

			var tests = Fjv.xCPU.CodeProvider.Tools.CpuInstructionTestMethodGeneration.GetAll<Z84C00InstructionSet>(
				left: new Action<System.IO.StringWriter, string, string>((writer, operand, field) =>
				{
					if (operand == nameof(Z80Index))
					{
						writer.WriteLine($"\t\tLeft = \"(ix+0x00)\",");
					}
				}),
				right: new Action<System.IO.StringWriter, string, string>((writer, operand, field) =>
				{
					if (operand == nameof(Z80Index))
					{
						writer.WriteLine($"\t\tRight = \"(ix+0x00)\",");
					}
				}));

			var filename = $"{System.IO.Path.GetTempFileName()}.txt";

			System.IO.File.WriteAllText(filename, setup);
			System.IO.File.AppendAllText(filename, tests);

			Debug.WriteLine($"output file: {filename}");

			Assert.IsTrue(true);
		}
	}
}

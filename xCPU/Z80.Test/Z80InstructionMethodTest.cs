using Fjv.xCPU.CodeProvider.Commons;
using Fjv.xCPU.CodeProvider.Tools;
using Fjv.Z80.Commons;
using Fjv.Z80.Instructions;
using NUnit.Framework;
using System;
using System.Linq;

namespace Z80.Test
{
    public class Z80InstructionMethodTest
    {
		Z84C00InstructionSet cpuIinstructionSet;

		[SetUp]
		public void Setup()
		{
			cpuIinstructionSet = new Z84C00InstructionSet();
		}

		[Test]
		public void Test_ld_no_right_pointer_no_left_pointer_Z80Register_Z80PrimeRegister()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "ld")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Register), typeof(Z80PrimeRegister))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Register.B,
				RightValue = Z80PrimeRegister.BPrime,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_ld_no_right_pointer_no_left_pointer_Z80Register_Byte()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "ld")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Register), typeof(Byte))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Register.B,
				RightValue = Byte.MaxValue,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_ld_has_right_pointer_has_left_pointer_Z80Register_Z80PairRegister()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "ld")
				.Where(s => s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Register), typeof(Z80PairRegister))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Register.A,
				RightValue = Z80PairRegister.HL,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_ld_no_right_pointer_no_left_pointer_Z80PairRegister_Z80Register()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "ld")
				.Where(s => !s.RightPointer)
				.Where(s => s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80PairRegister), typeof(Z80Register))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80PairRegister.HL,
				RightValue = Z80Register.A,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_ld_no_right_pointer_no_left_pointer_Z80PairRegister_Byte()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "ld")
				.Where(s => !s.RightPointer)
				.Where(s => s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80PairRegister), typeof(Byte))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80PairRegister.HL,
				RightValue = Byte.MaxValue,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_ld_no_right_pointer_no_left_pointer_Z80Register_Z80Index()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "ld")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Register), typeof(Z80Index))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Register.B,
				RightValue = Z80Index.IX,
				Right = "(ix+0x00)",
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_ld_no_right_pointer_no_left_pointer_Z80Index_Z80Register()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "ld")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Index), typeof(Z80Register))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Index.IX,
				Left = "(ix+0x00)",
				RightValue = Z80Register.B,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_ld_no_right_pointer_no_left_pointer_Z80Index_Byte()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "ld")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Index), typeof(Byte))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Index.IX,
				Left = "(ix+0x00)",
				RightValue = Byte.MaxValue,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_ld_has_right_pointer_has_left_pointer_Z80Register_UInt16()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "ld")
				.Where(s => s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Register), typeof(UInt16))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Register.A,
				RightValue = UInt16.MaxValue,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_ld_no_right_pointer_no_left_pointer_UInt16_Z80Register()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "ld")
				.Where(s => !s.RightPointer)
				.Where(s => s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(UInt16), typeof(Z80Register))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = UInt16.MaxValue,
				RightValue = Z80Register.A,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_ld_no_right_pointer_no_left_pointer_Z80Register_Z80Register()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "ld")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Register), typeof(Z80Register))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Register.A,
				RightValue = Z80Register.I,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_ld_no_right_pointer_no_left_pointer_Z80PairRegister_UInt16()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "ld")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80PairRegister), typeof(UInt16))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80PairRegister.BC,
				RightValue = UInt16.MaxValue,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);
		}

		[Test]
		public void Test_ld_no_right_pointer_no_left_pointer_Z80PairRegister_byte()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "ld")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80PairRegister), typeof(byte))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80PairRegister.HL,
				RightValue = byte.MaxValue,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);
		}

		[Test]
		public void Test_ld_no_right_pointer_no_left_pointer_Z80Index_UInt16()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "ld")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Index), typeof(UInt16))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Index.IX,
				Left = "(ix+0x00)",
				RightValue = UInt16.MaxValue,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_ld_has_right_pointer_has_left_pointer_Z80PairRegister_UInt16()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "ld")
				.Where(s => s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80PairRegister), typeof(UInt16))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80PairRegister.HL,
				RightValue = UInt16.MaxValue,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_ld_has_right_pointer_has_left_pointer_Z80Index_UInt16()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "ld")
				.Where(s => s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Index), typeof(UInt16))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Index.IX,
				Left = "(ix+0x00)",
				RightValue = UInt16.MaxValue,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_ld_no_right_pointer_no_left_pointer_UInt16_Z80PairRegister()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "ld")
				.Where(s => !s.RightPointer)
				.Where(s => s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(UInt16), typeof(Z80PairRegister))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = UInt16.MaxValue,
				RightValue = Z80PairRegister.HL,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_ld_no_right_pointer_no_left_pointer_UInt16_Z80Index()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "ld")
				.Where(s => !s.RightPointer)
				.Where(s => s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(UInt16), typeof(Z80Index))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = UInt16.MaxValue,
				RightValue = Z80Index.IX,
				Right = "(ix+0x00)",
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_ld_no_right_pointer_no_left_pointer_Z80PairRegister_Z80PairRegister()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "ld")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80PairRegister), typeof(Z80PairRegister))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80PairRegister.SP,
				RightValue = Z80PairRegister.HL,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_ld_no_right_pointer_no_left_pointer_Z80PairRegister_Z80Index()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "ld")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80PairRegister), typeof(Z80Index))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80PairRegister.SP,
				RightValue = Z80Index.IX,
				Right = "(ix+0x00)",
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_push_no_right_pointer_no_left_pointer_Z80PairRegister()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "push")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80PairRegister), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80PairRegister.BC,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_push_no_right_pointer_no_left_pointer_Z80Index()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "push")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Index), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Index.IX,
				Left = "(ix+0x00)",
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_pop_no_right_pointer_no_left_pointer_Z80PairRegister()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "pop")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80PairRegister), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80PairRegister.BC,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_pop_no_right_pointer_no_left_pointer_Z80Index()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "pop")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Index), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Index.IX,
				Left = "(ix+0x00)",
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_ex_no_right_pointer_no_left_pointer_Z80PairRegister_Z80PairRegister()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "ex")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80PairRegister), typeof(Z80PairRegister))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80PairRegister.DE,
				RightValue = Z80PairRegister.HL,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_ex_no_right_pointer_no_left_pointer_Z80PairRegister_Z80PrimeRegister()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "ex")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80PairRegister), typeof(Z80PrimeRegister))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80PairRegister.AF,
				RightValue = Z80PrimeRegister.AFPrime,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_exx_without_operands()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "exx")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(EmptyOperandType), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument();

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_ex_no_right_pointer_no_left_pointer_Z80PairRegister_Z80PairRegister_B()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "ex")
				.Where(s => !s.RightPointer)
				.Where(s => s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80PairRegister), typeof(Z80PairRegister))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80PairRegister.SP,
				RightValue = Z80PairRegister.HL,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_ex_no_right_pointer_no_left_pointer_Z80PairRegister_Z80Index()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "ex")
				.Where(s => !s.RightPointer)
				.Where(s => s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80PairRegister), typeof(Z80Index))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80PairRegister.SP,
				RightValue = Z80Index.IX,
				Right = "(ix+0x00)",
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_ldi_without_operands()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "ldi")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(EmptyOperandType), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument();

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_ldir_without_operands()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "ldir")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(EmptyOperandType), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument();

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_ldd_without_operands()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "ldd")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(EmptyOperandType), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument();

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_lddr_without_operands()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "lddr")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(EmptyOperandType), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument();

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_cpi_without_operands()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "cpi")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(EmptyOperandType), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument();

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_cpir_without_operands()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "cpir")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(EmptyOperandType), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument();

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_cpd_without_operands()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "cpd")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(EmptyOperandType), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument();

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_cpdr_without_operands()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "cpdr")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(EmptyOperandType), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument();

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_add_no_right_pointer_no_left_pointer_Z80Register_Z80Register()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "add")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Register), typeof(Z80Register))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Register.A,
				RightValue = Z80Register.A,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_add_no_right_pointer_no_left_pointer_Z80Register_Byte()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "add")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Register), typeof(Byte))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Register.A,
				RightValue = Byte.MaxValue,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_add_has_right_pointer_has_left_pointer_Z80Register_Z80PairRegister()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "add")
				.Where(s => s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Register), typeof(Z80PairRegister))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Register.A,
				RightValue = Z80PairRegister.HL,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_add_no_right_pointer_no_left_pointer_Z80Register_Z80Index()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "add")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Register), typeof(Z80Index))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Register.A,
				RightValue = Z80Index.IX,
				Right = "(ix+0x00)",
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_adc_no_right_pointer_no_left_pointer_Z80Register_Z80Register()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "adc")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Register), typeof(Z80Register))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Register.A,
				RightValue = Z80Register.A,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_adc_no_right_pointer_no_left_pointer_Z80Register_Byte()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "adc")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Register), typeof(Byte))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Register.A,
				RightValue = Byte.MaxValue,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_adc_has_right_pointer_has_left_pointer_Z80Register_Z80PairRegister()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "adc")
				.Where(s => s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Register), typeof(Z80PairRegister))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Register.A,
				RightValue = Z80PairRegister.HL,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_adc_no_right_pointer_no_left_pointer_Z80Register_Z80Index()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "adc")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Register), typeof(Z80Index))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Register.A,
				RightValue = Z80Index.IX,
				Right = "(ix+0x00)",
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_sub_no_right_pointer_no_left_pointer_Z80Register()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "sub")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Register), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Register.B,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_sub_no_right_pointer_no_left_pointer_Byte()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "sub")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Byte), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Byte.MaxValue,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_sub_no_right_pointer_no_left_pointer_Z80PairRegister()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "sub")
				.Where(s => !s.RightPointer)
				.Where(s => s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80PairRegister), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80PairRegister.HL,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_sub_no_right_pointer_no_left_pointer_Z80Index()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "sub")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Index), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Index.IX,
				Left = "(ix+0x00)",
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_sbc_no_right_pointer_no_left_pointer_Z80Register_Z80Register()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "sbc")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Register), typeof(Z80Register))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Register.A,
				RightValue = Z80Register.A,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_sbc_no_right_pointer_no_left_pointer_Z80Register_Byte()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "sbc")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Register), typeof(Byte))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Register.A,
				RightValue = Byte.MaxValue,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_sbc_has_right_pointer_has_left_pointer_Z80Register_Z80PairRegister()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "sbc")
				.Where(s => s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Register), typeof(Z80PairRegister))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Register.A,
				RightValue = Z80PairRegister.HL,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_sbc_no_right_pointer_no_left_pointer_Z80Register_Z80Index()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "sbc")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Register), typeof(Z80Index))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Register.A,
				RightValue = Z80Index.IX,
				Right = "(ix+0x00)",
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_and_no_right_pointer_no_left_pointer_Z80Register()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "and")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Register), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Register.B,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_and_no_right_pointer_no_left_pointer_Byte()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "and")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Byte), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Byte.MaxValue,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_and_no_right_pointer_no_left_pointer_Z80PairRegister()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "and")
				.Where(s => !s.RightPointer)
				.Where(s => s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80PairRegister), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80PairRegister.HL,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_and_no_right_pointer_no_left_pointer_Z80Index()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "and")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Index), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Index.IX,
				Left = "(ix+0x00)",
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_or_no_right_pointer_no_left_pointer_Z80Register()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "or")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Register), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Register.B,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_or_no_right_pointer_no_left_pointer_Byte()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "or")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Byte), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Byte.MaxValue,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_or_no_right_pointer_no_left_pointer_Z80PairRegister()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "or")
				.Where(s => !s.RightPointer)
				.Where(s => s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80PairRegister), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80PairRegister.HL,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_or_no_right_pointer_no_left_pointer_Z80Index()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "or")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Index), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Index.IX,
				Left = "(ix+0x00)",
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_xor_no_right_pointer_no_left_pointer_Z80Register()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "xor")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Register), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Register.B,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_xor_no_right_pointer_no_left_pointer_Byte()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "xor")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Byte), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Byte.MaxValue,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_xor_no_right_pointer_no_left_pointer_Z80PairRegister()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "xor")
				.Where(s => !s.RightPointer)
				.Where(s => s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80PairRegister), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80PairRegister.HL,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_xor_no_right_pointer_no_left_pointer_Z80Index()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "xor")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Index), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Index.IX,
				Left = "(ix+0x00)",
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_cp_no_right_pointer_no_left_pointer_Z80Register()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "cp")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Register), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Register.B,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_cp_no_right_pointer_no_left_pointer_Byte()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "cp")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Byte), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Byte.MaxValue,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_cp_no_right_pointer_no_left_pointer_Z80PairRegister()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "cp")
				.Where(s => !s.RightPointer)
				.Where(s => s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80PairRegister), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80PairRegister.HL,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_cp_no_right_pointer_no_left_pointer_Z80Index()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "cp")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Index), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Index.IX,
				Left = "(ix+0x00)",
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_inc_no_right_pointer_no_left_pointer_Z80Register()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "inc")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Register), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Register.B,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_inc_no_right_pointer_no_left_pointer_Z80PairRegister()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "inc")
				.Where(s => !s.RightPointer)
				.Where(s => s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80PairRegister), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80PairRegister.HL,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_inc_no_right_pointer_no_left_pointer_Z80Index()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "inc")
				.Where(s => !s.RightPointer)
				.Where(s => s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Index), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Index.IX,
				Left = "(ix+0x00)",
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_dec_no_right_pointer_no_left_pointer_Z80Register()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "dec")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Register), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Register.B,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_dec_no_right_pointer_no_left_pointer_Z80PairRegister()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "dec")
				.Where(s => !s.RightPointer)
				.Where(s => s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80PairRegister), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80PairRegister.HL,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_dec_no_right_pointer_no_left_pointer_Z80Index()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "dec")
				.Where(s => !s.RightPointer)
				.Where(s => s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Index), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Index.IX,
				Left = "(ix+0x00)",
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_daa_without_operands()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "daa")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(EmptyOperandType), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument();

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_cpl_without_operands()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "cpl")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(EmptyOperandType), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument();

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_neg_without_operands()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "neg")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(EmptyOperandType), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument();

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_ccf_without_operands()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "ccf")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(EmptyOperandType), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument();

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_scf_without_operands()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "scf")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(EmptyOperandType), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument();

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_nop_without_operands()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "nop")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(EmptyOperandType), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument();

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_halt_without_operands()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "halt")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(EmptyOperandType), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument();

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_di_without_operands()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "di")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(EmptyOperandType), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument();

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_ei_without_operands()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "ei")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(EmptyOperandType), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument();

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_im_no_right_pointer_no_left_pointer_Byte()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "im")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Byte), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Byte.MinValue,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_add_no_right_pointer_no_left_pointer_Z80PairRegister_Z80PairRegister()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "add")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80PairRegister), typeof(Z80PairRegister))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80PairRegister.HL,
				RightValue = Z80PairRegister.HL,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_adc_no_right_pointer_no_left_pointer_Z80PairRegister_Z80PairRegister()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "adc")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80PairRegister), typeof(Z80PairRegister))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80PairRegister.HL,
				RightValue = Z80PairRegister.HL,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_sbc_no_right_pointer_no_left_pointer_Z80PairRegister_Z80PairRegister()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "sbc")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80PairRegister), typeof(Z80PairRegister))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80PairRegister.HL,
				RightValue = Z80PairRegister.HL,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_add_no_right_pointer_no_left_pointer_Z80Index_Z80PairRegister()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "add")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Index), typeof(Z80PairRegister))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Index.IX,
				Left = "(ix+0x00)",
				RightValue = Z80PairRegister.BC,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_inc_no_right_pointer_no_left_pointer_Z80PairRegister_B()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "inc")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80PairRegister), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80PairRegister.BC,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_inc_no_right_pointer_no_left_pointer_Z80Index_B()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "inc")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Index), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Index.IX,
				Left = "(ix+0x00)",
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_dec_no_right_pointer_no_left_pointer_Z80PairRegister_B()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "dec")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80PairRegister), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80PairRegister.BC,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_dec_no_right_pointer_no_left_pointer_Z80Index_B()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "dec")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Index), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Index.IX,
				Left = "(ix+0x00)",
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_rlca_without_operands()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "rlca")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(EmptyOperandType), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument();

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_rla_without_operands()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "rla")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(EmptyOperandType), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument();

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_rrca_without_operands()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "rrca")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(EmptyOperandType), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument();

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_rra_without_operands()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "rra")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(EmptyOperandType), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument();

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_rld_without_operands()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "rld")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(EmptyOperandType), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument();

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_rrd_without_operands()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "rrd")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(EmptyOperandType), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument();

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_rlc_no_right_pointer_no_left_pointer_Z80Register()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "rlc")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Register), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Register.B,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_rlc_no_right_pointer_no_left_pointer_Z80PairRegister()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "rlc")
				.Where(s => !s.RightPointer)
				.Where(s => s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80PairRegister), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80PairRegister.HL,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_rlc_no_right_pointer_no_left_pointer_Z80Index()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "rlc")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Index), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Index.IX,
				Left = "(ix+0x00)",
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_rl_no_right_pointer_no_left_pointer_Z80Register()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "rl")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Register), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Register.B,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_rl_no_right_pointer_no_left_pointer_Z80PairRegister()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "rl")
				.Where(s => !s.RightPointer)
				.Where(s => s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80PairRegister), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80PairRegister.HL,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_rl_no_right_pointer_no_left_pointer_Z80Index()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "rl")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Index), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Index.IX,
				Left = "(ix+0x00)",
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_rrc_no_right_pointer_no_left_pointer_Z80Register()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "rrc")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Register), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Register.B,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_rrc_no_right_pointer_no_left_pointer_Z80PairRegister()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "rrc")
				.Where(s => !s.RightPointer)
				.Where(s => s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80PairRegister), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80PairRegister.HL,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_rrc_no_right_pointer_no_left_pointer_Z80Index()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "rrc")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Index), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Index.IX,
				Left = "(ix+0x00)",
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_rr_no_right_pointer_no_left_pointer_Z80Register()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "rr")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Register), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Register.B,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_rr_no_right_pointer_no_left_pointer_Z80PairRegister()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "rr")
				.Where(s => !s.RightPointer)
				.Where(s => s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80PairRegister), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80PairRegister.HL,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_rr_no_right_pointer_no_left_pointer_Z80Index()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "rr")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Index), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Index.IX,
				Left = "(ix+0x00)",
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_sla_no_right_pointer_no_left_pointer_Z80Register()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "sla")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Register), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Register.B,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_sla_no_right_pointer_no_left_pointer_Z80PairRegister()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "sla")
				.Where(s => !s.RightPointer)
				.Where(s => s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80PairRegister), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80PairRegister.HL,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_sla_no_right_pointer_no_left_pointer_Z80Index()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "sla")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Index), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Index.IX,
				Left = "(ix+0x00)",
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_sra_no_right_pointer_no_left_pointer_Z80Register()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "sra")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Register), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Register.B,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_sra_no_right_pointer_no_left_pointer_Z80PairRegister()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "sra")
				.Where(s => !s.RightPointer)
				.Where(s => s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80PairRegister), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80PairRegister.HL,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_sra_no_right_pointer_no_left_pointer_Z80Index()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "sra")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Index), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Index.IX,
				Left = "(ix+0x00)",
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_srl_no_right_pointer_no_left_pointer_Z80Register()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "srl")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Register), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Register.B,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_srl_no_right_pointer_no_left_pointer_Z80PairRegister()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "srl")
				.Where(s => !s.RightPointer)
				.Where(s => s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80PairRegister), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80PairRegister.HL,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_srl_no_right_pointer_no_left_pointer_Z80Index()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "srl")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Index), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Index.IX,
				Left = "(ix+0x00)",
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_bit_no_right_pointer_no_left_pointer_Byte_Z80Register()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "bit")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Byte), typeof(Z80Register))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Byte.MinValue,
				RightValue = Z80Register.B,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_bit_has_right_pointer_has_left_pointer_Byte_Z80PairRegister()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "bit")
				.Where(s => s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Byte), typeof(Z80PairRegister))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Byte.MinValue,
				RightValue = Z80PairRegister.HL,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_bit_has_right_pointer_has_left_pointer_Byte_Z80Index()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "bit")
				.Where(s => s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Byte), typeof(Z80Index))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Byte.MinValue,
				RightValue = Z80Index.IX,
				Right = "(ix+0x00)",
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_set_no_right_pointer_no_left_pointer_Byte_Z80Register()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "set")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Byte), typeof(Z80Register))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Byte.MinValue,
				RightValue = Z80Register.B,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_set_has_right_pointer_has_left_pointer_Byte_Z80PairRegister()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "set")
				.Where(s => s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Byte), typeof(Z80PairRegister))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Byte.MinValue,
				RightValue = Z80PairRegister.HL,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_set_has_right_pointer_has_left_pointer_Byte_Z80Index()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "set")
				.Where(s => s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Byte), typeof(Z80Index))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Byte.MinValue,
				RightValue = Z80Index.IX,
				Right = "(ix+0x00)",
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_res_no_right_pointer_no_left_pointer_Byte_Z80Register()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "res")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Byte), typeof(Z80Register))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Byte.MinValue,
				RightValue = Z80Register.B,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_res_has_right_pointer_has_left_pointer_Byte_Z80PairRegister()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "res")
				.Where(s => s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Byte), typeof(Z80PairRegister))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Byte.MinValue,
				RightValue = Z80PairRegister.HL,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_res_has_right_pointer_has_left_pointer_Byte_Z80Index()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "res")
				.Where(s => s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Byte), typeof(Z80Index))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Byte.MinValue,
				RightValue = Z80Index.IX,
				Right = "(ix+0x00)",
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_jp_no_right_pointer_no_left_pointer_UInt16()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "jp")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(UInt16), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = UInt16.MaxValue,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_jp_no_right_pointer_no_left_pointer_Byte()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "jp")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Byte), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Byte.MaxValue,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_jp_no_right_pointer_no_left_pointer_Z80TestableFlag_UInt16()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "jp")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80TestableFlag), typeof(UInt16))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80TestableFlag.NZ,
				RightValue = UInt16.MaxValue,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_jr_no_right_pointer_no_left_pointer_SByte()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "jr")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(SByte), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = SByte.MaxValue,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_jr_no_right_pointer_no_left_pointer_Byte()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "jr")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Byte), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Byte.MinValue,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_jr_no_right_pointer_no_left_pointer_Z80TestableFlag_Byte()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "jr")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80TestableFlag), typeof(Byte))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80TestableFlag.C,
				RightValue = Byte.MinValue,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_jr_no_right_pointer_no_left_pointer_Z80TestableFlag_SByte()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "jr")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80TestableFlag), typeof(SByte))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80TestableFlag.C,
				RightValue = SByte.MaxValue,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_jp_no_right_pointer_no_left_pointer_Z80TestableFlag_Byte()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "jp")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80TestableFlag), typeof(Byte))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80TestableFlag.Z,
				RightValue = Byte.MinValue,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_jp_no_right_pointer_no_left_pointer_Z80TestableFlag_SByte()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "jp")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80TestableFlag), typeof(SByte))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80TestableFlag.Z,
				RightValue = SByte.MaxValue,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_jp_no_right_pointer_no_left_pointer_Z80PairRegister()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "jp")
				.Where(s => !s.RightPointer)
				.Where(s => s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80PairRegister), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80PairRegister.HL,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_jp_no_right_pointer_no_left_pointer_Z80Index()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "jp")
				.Where(s => !s.RightPointer)
				.Where(s => s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Index), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Index.IX,
				Left = "(ix+0x00)",
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_djnz_no_right_pointer_no_left_pointer_Byte()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "djnz")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Byte), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Byte.MinValue,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_djnz_no_right_pointer_no_left_pointer_SByte()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "djnz")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(SByte), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = SByte.MaxValue,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_call_no_right_pointer_no_left_pointer_UInt16()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "call")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(UInt16), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = UInt16.MaxValue,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_call_no_right_pointer_no_left_pointer_Z80TestableFlag_UInt16()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "call")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80TestableFlag), typeof(UInt16))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80TestableFlag.NZ,
				RightValue = UInt16.MaxValue,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_ret_without_operands()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "ret")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(EmptyOperandType), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument();

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_ret_no_right_pointer_no_left_pointer_Z80TestableFlag()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "ret")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80TestableFlag), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80TestableFlag.NZ,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_reti_without_operands()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "reti")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(EmptyOperandType), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument();

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_retn_without_operands()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "retn")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(EmptyOperandType), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument();

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_rst_no_right_pointer_no_left_pointer_Byte()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "rst")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Byte), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Byte.MinValue,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_in_has_right_pointer_has_left_pointer_Z80Register_Byte()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "in")
				.Where(s => s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Register), typeof(Byte))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Register.A,
				RightValue = Byte.MaxValue,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_in_has_right_pointer_has_left_pointer_Z80Register_Z80Register()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "in")
				.Where(s => s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Register), typeof(Z80Register))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Register.C,
				RightValue = Z80Register.C,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_ini_without_operands()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "ini")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(EmptyOperandType), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument();

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_inir_without_operands()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "inir")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(EmptyOperandType), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument();

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_ind_without_operands()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "ind")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(EmptyOperandType), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument();

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_indr_without_operands()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "indr")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(EmptyOperandType), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument();

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_out_no_right_pointer_no_left_pointer_Byte_Z80Register()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "out")
				.Where(s => !s.RightPointer)
				.Where(s => s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Byte), typeof(Z80Register))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Byte.MaxValue,
				RightValue = Z80Register.A,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_out_no_right_pointer_no_left_pointer_Z80Register_Z80Register()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "out")
				.Where(s => !s.RightPointer)
				.Where(s => s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Register), typeof(Z80Register))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument()
			{
				LeftValue = Z80Register.C,
				RightValue = Z80Register.C,
			};

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_outi_without_operands()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "outi")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(EmptyOperandType), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument();

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_otir_without_operands()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "otir")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(EmptyOperandType), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument();

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_outd_without_operands()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "outd")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(EmptyOperandType), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument();

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}

		[Test]
		public void Test_otdr_without_operands()
		{
			CpuInstructionType instruction;
			byte[] bytes;
			InstructionArgument instructionParams;

			// try to get the instruction to test.
			instruction = cpuIinstructionSet.Instructions.Where(s => s.Mnemonic == "otdr")
				.Where(s => !s.RightPointer)
				.Where(s => !s.LeftPointer)
				.Where(s => Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(EmptyOperandType), typeof(EmptyOperandType))))
				.SingleOrDefault();

			// todo: add or replace properties of instruction parameters.
			instructionParams = new InstructionArgument();

			bytes = instruction.Instruction.Invoke(instructionParams);

			// todo: add asserts.
			Assert.AreEqual(instruction.Size.Invoke(instructionParams), bytes.Length);

		}
	}
}

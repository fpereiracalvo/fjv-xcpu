using Fjv.xCPU.CodeProvider.Tools;
using Fjv.xCPU.CodeProvider.Commons;
using Fjv.Z80.Commons;
using Fjv.Z80.Instructions;
using NUnit.Framework;
using System.Linq;

namespace Z80.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CloneInstruction()
        {
            var z80instructionSet = new Z84C00InstructionSet();

            var instruction = z80instructionSet.Instructions.FirstOrDefault();
            var cloned = (CpuInstructionType)z80instructionSet.Instructions.FirstOrDefault().Clone();

            Assert.AreEqual(instruction.Mnemonic, cloned.Mnemonic);
        }

        [Test]
        public void Load8BitTest()
        {
            var z80instructionSet = new Z84C00InstructionSet();

            var ld = "ld";

            //ld r,r'
            var ldrr = z80instructionSet.Instructions.SingleOrDefault(s => s.Mnemonic == ld && 
                Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Register), typeof(Z80PrimeRegister))));

            var bytes = ldrr.Instruction.Invoke(new InstructionArgument {
                LeftValue = Z80Register.A,
                RightValue = Z80PrimeRegister.APrime
            });

            Assert.AreEqual(bytes[0], 0x7f);

            //ld r,n
            ldrr = z80instructionSet.Instructions.SingleOrDefault(s => s.Mnemonic == ld && 
                Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Register), typeof(byte))));

            bytes = ldrr.Instruction.Invoke(new InstructionArgument {
                LeftValue = Z80Register.A,
                RightValue = (byte)0x10
            });

            Assert.AreEqual(bytes[0], 0x3e);
            Assert.AreEqual(bytes[1], 0x10);

            //ld r,(hl) / ld a,(bc) / ld a,(de) -> (a == r)
            ldrr = z80instructionSet.Instructions.SingleOrDefault(s => s.Mnemonic == ld &&
                s.RightPointer == true &&
                Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Register), typeof(Z80PairRegister))));

            bytes = ldrr.Instruction.Invoke(new InstructionArgument
            {
                LeftValue = Z80Register.A,
                RightValue = Z80PairRegister.HL
            });

            Assert.AreEqual(bytes[0], 0x7e);

            bytes = ldrr.Instruction.Invoke(new InstructionArgument
            {
                LeftValue = Z80Register.A,
                RightValue = Z80PairRegister.BC
            });

            Assert.AreEqual(bytes[0], 0x0a);

            bytes = ldrr.Instruction.Invoke(new InstructionArgument
            {
                LeftValue = Z80Register.A,
                RightValue = Z80PairRegister.DE
            });

            Assert.AreEqual(bytes[0], 0x1a);

            //ld (hl),r / ld (bc),a / ld (de),a -> (a == r)
            ldrr = z80instructionSet.Instructions.SingleOrDefault(s => s.Mnemonic == ld &&
                s.LeftPointer == true &&
                Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80PairRegister), typeof(Z80Register))));

            bytes = ldrr.Instruction.Invoke(new InstructionArgument
            {
                LeftValue = Z80PairRegister.HL,
                RightValue = Z80Register.A
            });

            Assert.AreEqual(bytes[0], 0x77);

            bytes = ldrr.Instruction.Invoke(new InstructionArgument
            {
                LeftValue = Z80PairRegister.BC,
                RightValue = Z80Register.A
            });

            Assert.AreEqual(bytes[0], 0x02);

            bytes = ldrr.Instruction.Invoke(new InstructionArgument
            {
                LeftValue = Z80PairRegister.DE,
                RightValue = Z80Register.A
            });

            Assert.AreEqual(bytes[0], 0x12);

            //ld (hl),n
            ldrr = z80instructionSet.Instructions.SingleOrDefault(s => s.Mnemonic == ld &&
                s.LeftPointer == true &&
                Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80PairRegister), typeof(byte))));

            bytes = ldrr.Instruction.Invoke(new InstructionArgument
            {
                LeftValue = Z80PairRegister.HL,
                RightValue = (byte)0xff
            });

            Assert.AreEqual(bytes[0], 0x36);
            Assert.AreEqual(bytes[1], 0xff);

            //ld r,(ix+d) / ld r,(iy+d)
            ldrr = z80instructionSet.Instructions.SingleOrDefault(s => s.Mnemonic == ld &&
                Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Register), typeof(Z80Index))));

            bytes = ldrr.Instruction.Invoke(new InstructionArgument
            {
                LeftValue = Z80Register.A,
                Right = "(ix+0x0a)"
            });

            Assert.AreEqual(bytes[0], 0xdd);
            Assert.AreEqual(bytes[1], 0x7e); //0x46 | (0x07 << 3)
            Assert.AreEqual(bytes[2], 0x0a);

            bytes = ldrr.Instruction.Invoke(new InstructionArgument
            {
                LeftValue = Z80Register.C,
                Right = "(ix+0x10)"
            });

            Assert.AreEqual(bytes[0], 0xdd);
            Assert.AreEqual(bytes[1], 0x4e); //0x46 | (0x01 << 3)
            Assert.AreEqual(bytes[2], 0x10);

            bytes = ldrr.Instruction.Invoke(new InstructionArgument
            {
                LeftValue = Z80Register.A,
                Right = "(iy+0x0a)"
            });

            Assert.AreEqual(bytes[0], 0xfd);
            Assert.AreEqual(bytes[1], 0x7e); //0x46 | (0x07 << 3)
            Assert.AreEqual(bytes[2], 0x0a);

            bytes = ldrr.Instruction.Invoke(new InstructionArgument
            {
                LeftValue = Z80Register.C,
                Right = "(iy+0x10)"
            });

            Assert.AreEqual(bytes[0], 0xfd);
            Assert.AreEqual(bytes[1], 0x4e); //0x46 | (0x01 << 3)
            Assert.AreEqual(bytes[2], 0x10);

            //ld (ix+d),r / ld (iy+d),r
            ldrr = z80instructionSet.Instructions.SingleOrDefault(s => s.Mnemonic == ld &&
                Operands.EqualOperands(s.Operand, Operands.GetOperand(typeof(Z80Index), typeof(Z80Register))));

            bytes = ldrr.Instruction.Invoke(new InstructionArgument
            {
                Left= "(ix+0x0a)",
                RightValue = Z80Register.A
            });

            Assert.AreEqual(bytes[0], 0xdd);
            Assert.AreEqual(bytes[1], 0x77); //0x46 | 0x07
            Assert.AreEqual(bytes[2], 0x0a);

            bytes = ldrr.Instruction.Invoke(new InstructionArgument
            {
                Left = "(ix+0x05)",
                RightValue = Z80Register.C
            });

            Assert.AreEqual(bytes[0], 0xdd);
            Assert.AreEqual(bytes[1], 0x71); //0x46 | 0x01
            Assert.AreEqual(bytes[2], 0x05);

            bytes = ldrr.Instruction.Invoke(new InstructionArgument
            {
                Left = "(iy+0x0a)",
                RightValue = Z80Register.A
            });

            Assert.AreEqual(bytes[0], 0xfd);
            Assert.AreEqual(bytes[1], 0x77); //0x46 | (0x07 << 3)
            Assert.AreEqual(bytes[2], 0x0a);

            bytes = ldrr.Instruction.Invoke(new InstructionArgument
            {
                Left = "(iy+0x05)",
                RightValue = Z80Register.C
            });

            Assert.AreEqual(bytes[0], 0xfd);
            Assert.AreEqual(bytes[1], 0x71); //0x46 | (0x07 << 3)
            Assert.AreEqual(bytes[2], 0x05);
        }
    }
}
using Fjv.xCPU.CodeProvider.Commons;
using System;
using System.Linq;

namespace Fjv.xCPU.CodeProvider.Tools
{
    public class CpuInstructionTestMethodGeneration
    {
        static string instructionSetName = "cpuIinstructionSet";

        public static string GetSetup<T>()
            where T : InstructionSetBase<CpuInstructionType>, new()
        {
            using (var stringWriter = new System.IO.StringWriter())
            {
                stringWriter.WriteLine($"[SetUp]");
                stringWriter.WriteLine($"public void Setup()");
                stringWriter.WriteLine("{");
                stringWriter.WriteLine($"\t{instructionSetName} = new {typeof(T).Name}();");
                stringWriter.WriteLine("}");

                return stringWriter.ToString();
            }
        }

        public static string GetMethod(
            CpuInstructionType cpuInstructionType,
            InstructionSetBase<CpuInstructionType> instructionSet,
            Action<System.IO.StringWriter, string, string> left = null,
            Action<System.IO.StringWriter, string, string> right = null)
        {
            using (var stringWriter = new System.IO.StringWriter())
            {
                var instructionName = "instruction";

                var operands = cpuInstructionType.Operand?.Select(x => new {
                    Type = x,
                    Operand = x.Name,
                    Field = $"{x.Name}.{x.GetFields().FirstOrDefault().Name}"
                }).ToList();

                var rightPointer = cpuInstructionType.RightPointer ? "has_right_pointer" : "no_right_pointer";
                var leftPointer = cpuInstructionType.RightPointer ? "has_left_pointer" : "no_left_pointer";

                stringWriter.WriteLine("[Test]");

                if (operands !=null && operands.Any())
                {
                    if (operands.Count == 1)
                    {
                        stringWriter.WriteLine($"public void Test_{cpuInstructionType.Mnemonic}_{rightPointer}_{operands[0].Operand}()");
                    }
                    else if(operands.Count == 2)
                    {
                        stringWriter.WriteLine($"public void Test_{cpuInstructionType.Mnemonic}_{rightPointer}_{leftPointer}_{operands[0].Operand}_{operands[1].Operand}()");
                    }
                }
                else
                {
                    stringWriter.WriteLine($"public void Test_{cpuInstructionType.Mnemonic}_without_operands()");
                }

                stringWriter.WriteLine("{");

                stringWriter.WriteLine($"{nameof(CpuInstructionType)} {instructionName};");
                stringWriter.WriteLine("byte[] bytes;");
                stringWriter.WriteLine($"{nameof(InstructionArgument)} instructionParams;\n");

                stringWriter.WriteLine("// try to get the instruction to test.");
                stringWriter.WriteLine($"{instructionName} = {instructionSetName}.{nameof(instructionSet.Instructions)}.Where(s=> s.{nameof(cpuInstructionType.Mnemonic)} == \"{cpuInstructionType.Mnemonic}\")");

                if (cpuInstructionType.RightPointer)
                {
                    stringWriter.WriteLine($"\t.Where(s=>s.{nameof(cpuInstructionType.RightPointer)})");
                }
                else
                {
                    stringWriter.WriteLine($"\t.Where(s=>!s.{nameof(cpuInstructionType.RightPointer)})");
                }

                if (cpuInstructionType.LeftPointer)
                {
                    stringWriter.WriteLine($"\t.Where(s=>s.{nameof(cpuInstructionType.LeftPointer)})");
                }
                else
                {
                    stringWriter.WriteLine($"\t.Where(s=>!s.{nameof(cpuInstructionType.LeftPointer)})");
                }

                if (operands != null)
                {
                    if (operands.Count == 1)
                    {
                        stringWriter.WriteLine($"\t.Where(s => {nameof(Operands)}.EqualOperands(s.Operand, {nameof(Operands)}.GetOperand(typeof({operands[0].Operand}), typeof({nameof(EmptyOperandType)}))))");
                    }
                    else if (operands.Count == 2)
                    {
                        stringWriter.WriteLine($"\t.Where(s => {nameof(Operands)}.EqualOperands(s.Operand, {nameof(Operands)}.GetOperand(typeof({operands[0].Operand}), typeof({operands[1].Operand}))))");
                    }
                }
                else
                {
                    stringWriter.WriteLine($"\t.Where(s => {nameof(Operands)}.EqualOperands(s.Operand, {nameof(Operands)}.GetOperand(typeof({nameof(EmptyOperandType)}), typeof({nameof(EmptyOperandType)}))))");
                }
                stringWriter.WriteLine("\t.SingleOrDefault();\n");

                stringWriter.WriteLine("// todo: add or replace properties of instruction parameters.");
                stringWriter.Write($"instructionParams = new {nameof(InstructionArgument)}()");

                if (operands != null)
                {
                    stringWriter.Write(" {\n");

                    if (operands.Count > 0)
                    {
                        var op = operands[0];
                        var reg = cpuInstructionType.Constraints?.ToList().Where(x => x.GetType().Equals(op.Type)).FirstOrDefault();

                        if (reg != null)
                        {
                            stringWriter.WriteLine($"\t\tLeftValue = {op.Operand}.{reg.Mnemonic.ToUpper()},");
                        }
                        else
                        {
                            stringWriter.WriteLine($"\t\tLeftValue = {op.Field},");

                            left?.Invoke(stringWriter, op.Operand, op.Field);
                        }
                    }

                    if (operands.Count > 1)
                    {
                        var op = operands[1];
                        var reg = cpuInstructionType.Constraints?.ToList().Where(x => x.GetType().Equals(op.Type)).FirstOrDefault();

                        if (reg != null)
                        {
                            stringWriter.WriteLine($"\t\tRightValue = {op.Operand}.{reg.Mnemonic.ToUpper()},");
                        }
                        else
                        {
                            stringWriter.WriteLine($"\t\tRightValue = {op.Field},");

                            right?.Invoke(stringWriter, op.Operand, op.Field);
                        }
                    }

                    stringWriter.Write("\t}");
                }
                stringWriter.WriteLine(";\n");

                stringWriter.WriteLine($"\tbytes = {instructionName}.Instruction.Invoke(instructionParams);\n");

                stringWriter.WriteLine("// todo: add asserts.");
                stringWriter.WriteLine($"Assert.AreEqual({instructionName}.Size.Invoke(instructionParams), bytes.Length);");

                stringWriter.WriteLine("}");
                return stringWriter.ToString();
            }
        }

        public static string GetAll<T>(
            Action<System.IO.StringWriter, string, string> left = null,
            Action<System.IO.StringWriter, string, string> right = null)
            where T: InstructionSetBase<CpuInstructionType>, new()
        {
            var instructionSet = new T();

            using (var stringWriter = new System.IO.StringWriter())
            {
                instructionSet.Instructions.ForEach(cpuInstructionType => {
                        var operands = cpuInstructionType.Operand?.Select(x => new {
                            Type = x,
                            Operand = x.Name,
                            Field = $"{x.Name}.{x.GetFields().FirstOrDefault().Name}"
                        }).ToList();

                        var method = GetMethod(cpuInstructionType, instructionSet, left, right);

                        stringWriter.WriteLine(method);
                    });

                return stringWriter.ToString();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Fjv.xCPU.CodeProvider.Commons;
using Fjv.xCPU.CodeProvider.Tools;

namespace Fjv.xCPU.CodeProvider
{
    public class InstructionResolver<T, TResult>
         where T : InstructionTypeBase<TResult>
    {
        InstructionSetBase<T> _instructionsSet;
        string _source;

        ILabelProvider _labelProvider;

        public InstructionResolver(InstructionSetBase<T> instructionsSet)
        {
            _instructionsSet = instructionsSet;
        }

        public InstructionResolver(InstructionSetBase<T> instructionsSet, string source)
            : this(instructionsSet)
        {
            _source = source;
        }

        public void SetLabelStorage(ILabelProvider labelProvider)
        {
            _labelProvider = labelProvider;
        }

        public virtual bool HasInstruction(string line)
        {
            var commandline = new CommandLine<T, TResult>(line, _instructionsSet, _labelProvider);

            if (!commandline.HasCodeValues)
            {
                return false;
            }

            if (!commandline.HasCommand)
            {
                return false;
            }

            return IsExistCommandMnemonic(commandline.Command);
        }

        public virtual InstructionTypeBase<TResult> GetInstruction(string line)
        {
            var commandline = new CommandLine<T, TResult>(line, _instructionsSet, _labelProvider);

            var (left, right) = GetLineArguments(commandline);

            var (instruction, isRegexInstruction) = GetInstruction(commandline, line, left, right);

            if (instruction == null)
            {
                throw new Exception($"The instruction it wasn't recognized.");
            }
            else if (isRegexInstruction)
            {
                return instruction;
            }

            instruction.Replacements = new List<string>();

            instruction.Arguments = new InstructionArgument {
                Instruction = this,
                Labels = _labelProvider,
                Source = _source,
                Left = left?.Code,
                LeftValue = left?.Value,
                Right = right?.Code,
                RightValue = right?.Value
            };

            if (left.Labels.Any() || right.Labels.Any())
            {
                instruction.Replacements.AddRange(left.Labels);
                instruction.Replacements.AddRange(right.Labels);
            }

            instruction.Line = line;

            return instruction;
        }

        public bool IsExistCommandMnemonic(string value)
        {
            var instructions = _instructionsSet.GetMnemonics();

            return  instructions.Contains(value);
        }

        public bool IsExistCommonTypesMnemonic(string value)
        {
            var commonTypes = _instructionsSet.GetCommonTypesMnemonics();

            return commonTypes.Contains(value);
        }

        private (Operand left, Operand right) GetLineArguments(CommandLine<T, TResult> linecode)
        {
            Operand left = new Operand();
            Operand right = new Operand();

            if (linecode.HasLeftArgument)
            {
                left = linecode.LeftArgument;
            }

            if (linecode.HasRightArgument)
            {
                right = linecode.RightArgument;
            }

            return (left, right);
        }

        private (InstructionTypeBase<TResult> instruction, bool isRegexInstruction) GetInstruction(CommandLine<T, TResult> commandline, string line, Operand left, Operand right)
        {
            if (HasRegexInstruction(commandline.Command))
            {
                return (GetRegexInstruction(commandline.Command, line, _source), true);
            }

            return (_instructionsSet.Instructions
                .Where(s => s.Mnemonic == commandline.Command &&
                    Operands.EqualOperands(s.Operand, Operands.GetOperand(left.Type, right.Type)) &&
                    s.LeftPointer == left.IsPointer &&
                    s.RightPointer == right.IsPointer)
                .SingleOrDefault(), false);
        }

        private bool HasRegexInstruction(string mnemonic)
        {
            return _instructionsSet.Instructions.Any(s => s.Mnemonic == mnemonic && s.Operand == null && !string.IsNullOrEmpty(s.RegexPattern));
        }

        private T GetRegexInstruction(string mnemonic, string codeline, string sourcePath)
        {
            var instruction = _instructionsSet.Instructions.SingleOrDefault(s => s.Mnemonic == mnemonic && s.Operand == null && !string.IsNullOrEmpty(s.RegexPattern));

            var _params = Regex.Matches(codeline, instruction.RegexPattern).Cast<Match>().Select(m => m.Value).ToArray();

            instruction.Arguments = new InstructionArgument
            {
                Left = _params.Length > 0 ? _params[0] : null,
                Right = _params.Length > 1 ? _params[1] : null,
                Instruction = instruction,
                Labels = _labelProvider,
                Source = sourcePath
            };

            return instruction;
        }
    }
}

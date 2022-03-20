using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Fjv.xCPU.CodeProvider.Constants;
using Fjv.xCPU.CodeProvider.Extensions;
using Fjv.xCPU.CodeProvider.Tools;

namespace Fjv.xCPU.CodeProvider.Commons
{
    public class CommandLine
    {
        private string _mainPattern = @"(?<command>([aA-zZ0-9]*)) (?<left>(?:[aA-zZ'""]|\d|\+|\-|\/|\*|\(|\)|[aA-zZ'""]|\d| )*)\,*\s*(?<right>(?:[aA-zZ'""]|\d|\+|\-|\/|\*|\(|\)|[aA-zZ'""]|\d| )*)";
        protected string _line;

        public string Label { get; set; }
        public string Command { get; set; }
        public string Left { get; set; }
        public string Right { get; set; }

        public bool HasCodeValues =>
            !(string.IsNullOrWhiteSpace(this.Command) &&
            (string.IsNullOrWhiteSpace(this.Left) ||
            string.IsNullOrWhiteSpace(this.Right)));

        public bool HasLeftArgument => !string.IsNullOrWhiteSpace(this.Left);
        public bool HasRightArgument => !string.IsNullOrWhiteSpace(this.Right);
        public bool HasCommand => !string.IsNullOrWhiteSpace(this.Command);
        public bool HasLabel => !string.IsNullOrWhiteSpace(this.Label);

        public CommandLine(string line, string[] commands)
        {
            Initialize(line, commands);
        }

        private void Initialize(string line, string[] commands)
        {
            _line = line.Trim();

            if (!IsCode(_line))
            {
                return;
            }

            var regex = new Regex(_mainPattern);

            var match = regex.Match(_line);

            if (!match.Success)
            {
                if (commands.Contains(_line))
                {
                    this.Command = _line;
                }
                else
                {
                    this.Label = _line;
                }

                return;
            }

            var command = match.Groups["command"].Value.Trim();
            if (commands.Contains(command))
            {
                this.Command = command;
            }
            else
            {
                this.Label = command;
            }

            this.Left = match.Groups["left"].Value.Trim();
            this.Right = match.Groups["right"].Value.Trim();
        }

        private bool IsCode(string line)
        {
            return !string.IsNullOrWhiteSpace(line.IndexOf(';') >= 0 ? Regex.Match(line, "^(.+?);").Value.Replace(";", "").Trim() : line);
        }
    }

    public class CommandLine<T, TResult> : CommandLine
        where T : InstructionTypeBase<TResult>
    {
        InstructionSetBase<T> _instructionSet;
        ILabelProvider _labelProvider;

        public string Line => _line;

        public Operand LeftArgument => GetLeftArgument();
        public Operand RightArgument => GetRightArgument();

        public CommandLine(string line, InstructionSetBase<T> instructionSet, ILabelProvider labelProvider)
            : base (line, instructionSet.GetMnemonics())
        {
            _instructionSet = instructionSet;
            _labelProvider = labelProvider;
        }

        private (string Code, Type[] Types) GetSideOperand(OperandTypes operand)
        {
            var instruciontPossibleTypes = _instructionSet.Instructions
                .Where(s => s.Mnemonic == this.Command)
                .Select(s => new {
                    LeftOperand = s.Operand.Left(),
                    RightOperand = s.Operand.Right()
                }).Distinct().ToArray();

            switch (operand)
            {
                case OperandTypes.Left:
                    return (this.Left, instruciontPossibleTypes.Select(s => s.LeftOperand).ToArray());
                case OperandTypes.Right:
                    return (this.Right, instruciontPossibleTypes.Select(s => s.RightOperand).ToArray());
                default:
                    throw new Exception("No se ha especificado un operando válido.");
            }
        }

        private Operand GetLeftArgument()
        {
            var sideOperand = GetSideOperand(OperandTypes.Left);

            var argument = new Argument<T, TResult>(_instructionSet, _labelProvider);

            return argument.ProcessArgument(sideOperand.Code, sideOperand.Types);
        }

        private Operand GetRightArgument()
        {
            var sideOperand = GetSideOperand(OperandTypes.Right);

            var argument = new Argument<T, TResult>(_instructionSet, _labelProvider);

            return argument.ProcessArgument(sideOperand.Code, sideOperand.Types);
        }
    }
}

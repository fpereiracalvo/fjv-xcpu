using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Fjv.xCPU.CodeProvider.Commons
{
    public class Argument<T, TResult>
         where T : InstructionTypeBase<TResult>
    {
        InstructionSetBase<T> _instructionSet;
        ILabelProvider _labelProvider;

        static Type[] _numerictypes = new Type[] { typeof(byte), typeof(UInt16) };

        public Argument(InstructionSetBase<T> instructionSet, ILabelProvider labelProvider)
        {
            _instructionSet = instructionSet;
            _labelProvider = labelProvider;
        }

        public Operand ProcessArgument(string operandline, Type[] possibleTypes)
        {
            var operand = new Operand() { Code = operandline };

            var possibleNumericType = possibleTypes.ToArray().Where(s => _numerictypes.Contains(s)).FirstOrDefault();

            operand.Code = GetReplaceLabelArgument(operandline);

            operand.Type = GetParamType(operandline, operand, possibleTypes);

            if (operand.Type == typeof(EmptyOperandType))
            {
                var labelValue = byte.MaxValue.ToString();

                if (possibleNumericType != null && possibleNumericType.Equals(typeof(UInt16)))
                {
                    labelValue = UInt16.MaxValue.ToString();
                }

                //todo: soporte multiples labels en operando.

                var aux = GetValueInPattern(operandline) ?? operandline;

                operand.Labels = new string[] { aux };

                operand.Code = operand.Code.Replace(aux, labelValue);
                operand.Type = _instructionSet.GetParamType(operand.Code);
            }

            operand.IsPointer = IsOperandPointer(operand);

            return operand;
        }

        private bool IsOperandPointer(Operand operand)
        {
            if (_numerictypes.Contains(operand.Type))
            {
                return _instructionSet.IsPointer(operand.Code);
            }
            else
            {
                return _instructionSet.IsPointer(operand.Code, operand.Type);
            }
        }

        private Type GetParamType(string opLine, Operand operand, Type[] possibleTypes)
        {
            var value = GetValueInPattern(opLine);

            if (!string.IsNullOrEmpty(value) && operand.Type == typeof(EmptyOperandType))
            {
                if (IsNumeric(value))
                {
                    return _instructionSet.GetParamType(value);
                }
            }

            return _instructionSet.GetParamType(operand.Code);
        }

        private string GetReplaceLabelArgument(string operandline)
        {
            var labellist = _labelProvider.GetDictionary().Select(s=>s.Key).ToList();

            labellist.ForEach(label => {
                var regex = new Regex(_instructionSet.Labels.Replace(label));

                var match = regex.Match(operandline);

                if (match.Success)
                {
                    operandline = operandline.Replace(label, _labelProvider.GetString(label));
                }
            });

            return operandline;
        }

        private bool IsNumeric(string value)
        {
            return _numerictypes.Contains(_instructionSet.GetParamType(value));
        }

        private string GetValueInPattern(string value)
        {
            var regex = new Regex(_instructionSet.Pointers.Pattern);

            var match = regex.Match(value);

            if (match.Success)
            {
                return match.Value.Trim();
            }

            return null;
        }
    }
}

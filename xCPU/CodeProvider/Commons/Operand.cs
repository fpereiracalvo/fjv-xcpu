using Fjv.xCPU.CodeProvider.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fjv.xCPU.CodeProvider.Commons
{
    public class Operand
    {
        public string Code { get; set; }
        public Type Type { get; set; }
        public bool IsPointer { get; set; }
        public Object Value => GetValueOperand();

        public string[] Labels { get; set; }

        public Operand()
        {
            this.Labels = new string[] { };
            this.Type = typeof(EmptyOperandType);
        }

        public bool HasType => !this.Type.Equals(typeof(EmptyOperandType));

        private object GetValueOperand()
        {
            if (string.IsNullOrWhiteSpace(this.Code))
            {
                return null;
            }

            var code = GetValueBetweenParenthesis(this.Code)??this.Code;

            //todo: mejorar obtención de valor de registros.

            if (this.Type.BaseType.Equals(typeof(RegisterType)))
            {
                var register = RegisterType.GetRegister(this.Code, this.Type);

                return register;
            }
            else if (this.Type.BaseType.Equals(typeof(IndexType)))
            {
                return null;
            }
            else if (this.Type.BaseType.Equals(typeof(ConditionType)))
            {
                var condition = ConditionType.GetCondition(this.Code, this.Type);

                return condition;
            }
            else if (this.Type.Equals(typeof(byte)))
            {
                return Bytes.GetByte(code);
            }
            else if (this.Type.Equals(typeof(UInt16)))
            {
                return Bytes.GetUint16(code);
            }

            return null;
        }

        private string GetValueBetweenParenthesis(string value)
        {
            var regex = new Regex(@"(?<=\().*(?=\))");

            var match = regex.Match(value);

            if (match.Success)
            {
                return match.Value.Trim();
            }

            return null;
        }
    }
}

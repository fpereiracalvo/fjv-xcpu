using Fjv.xCPU.CodeProvider;
using Fjv.xCPU.CodeProvider.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Fjv.xCPU.CodeProvider.Tools
{
    public class Operands
    {
        public static Type[] GetOperand(Type left, Type right)
        {
            if (!left.Equals(typeof(EmptyOperandType)) && right.Equals(typeof(EmptyOperandType)))
            {
                return new Type[] { left };
            }
            else if (!left.Equals(typeof(EmptyOperandType)) && !right.Equals(typeof(EmptyOperandType)))
            {
                return new Type[] { left, right };
            }
            else
            {
                return null;
            }
        }

        public static bool EqualOperands(Type[] a, Type[] b)
        {
            if (a == null && b == null)
            {
                return true;
            }
            else if (a != null && b == null)
            {
                return false;
            }
            else if (a == null && b != null)
            {
                return false;
            }
            else if (a.Length > 0 && b.Length == a.Length)
            {
                var result = true;

                for (int i = 0; i < a.Length; i++)
                {
                    if (!a[i].Equals(b[i]))
                    {
                        result = false;
                    }
                }

                return result;
            }
            else
            {
                return false;
            }
        }
    }
}

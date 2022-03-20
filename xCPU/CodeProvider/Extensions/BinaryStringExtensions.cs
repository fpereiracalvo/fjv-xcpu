using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Fjv.xCPU.CodeProvider.Extensions
{
    public static partial class StringExtensions
    {
        public static bool IsBinaryByte(this string value)
        {
            return value.BinaryEvaluator<bool>(new Func<string, bool>((s) =>
            {
                return true;
            }), () => false);
        }

        public static bool IsBinaryWord(this string value)
        {
            return value.BinaryEvaluator<bool>(new Func<string, bool>((s) =>
            {
                return true;
            }), () => false, 16);
        }

        public static byte GetByteFromBinary(this string binary)
        {
            return GetByteFromBinaryValue<byte>(binary, "The string isn't a valid byte binary value.");
        }

        public static sbyte GetSByteFromBinary(this string binary)
        {
            return GetByteFromBinaryValue<sbyte>(binary, "The string isn't a valid signed byte binary value.");
        }

        public static UInt16 GetWordFromBinary(this string binary)
        {
            return GetByteFromBinaryValue<UInt16>(binary, "The string isn't a valid word binary value.", 16);
        }

        public static T GetByteFromBinaryValue<T>(this string binary, string error, int length = 8)
        {
            var noMatch = false;

            var result = binary.BinaryEvaluator((input) =>
            {
                var m = 1;
                var calculated = Enumerable.Range(0, length).Select(s => int.Parse(input[s].ToString())).Reverse().Select(s =>
                {
                    var value = s * m;
                    m *= 2;
                    return value;
                }).Sum();

                return (T)Convert.ChangeType(calculated, typeof(T));
            }, () => {
                noMatch = true;

                return (T)Convert.ChangeType(0, typeof(T));
            });

            if (!noMatch)
            {
                return result;
            }

            throw new NotFiniteNumberException(error);
        }

        public static T BinaryEvaluator<T>(this string value, Func<string, T> func, Func<T> @default, int length = 8)
        {
            var regexBinary = new Regex($"[01]{{{length}}}");

            var input = value.Trim();

            var hasPointer = input.LastIndexOf('(') == 0 && input.IndexOf(')') == input.Length - 1;

            if (hasPointer)
            {
                input = input.Replace("(", "").Replace(")", "");
            }

            if (input.IndexOf("0b") == 0 || (input.IndexOf("b") > 1 && input.IndexOf("b") == input.Length - 1))
            {
                if (input.IndexOf("0b") == 0)
                {
                    input = input.Substring(2);
                }
                else
                {
                    input = input.Replace("b", "");
                }

                if (regexBinary.IsMatch(input))
                {
                    return func.Invoke(input);
                }
            }

            return @default.Invoke();
        }
    }
}

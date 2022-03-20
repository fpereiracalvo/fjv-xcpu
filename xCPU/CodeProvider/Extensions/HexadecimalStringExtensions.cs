using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fjv.xCPU.CodeProvider.Extensions
{
    public static partial class StringExtensions
    {
        public static byte GetByte(this string s)
        {
            if (s.IsHex())
            {
                var hex = s.GetHex();

                return byte.Parse(hex, System.Globalization.NumberStyles.HexNumber);
            }
            else
            {
                return byte.Parse(s);
            }
        }

        public static sbyte GetSByte(this string s)
        {
            if (s.IsHex())
            {
                var hex = s.GetHex();

                return sbyte.Parse(hex, System.Globalization.NumberStyles.HexNumber);
            }
            else
            {
                return sbyte.Parse(s);
            }
        }

        public static UInt16 GetUInt16(this string s)
        {
            if (s.Is16BitHex())
            {
                var hex = s.GetHex();

                return UInt16.Parse(hex, System.Globalization.NumberStyles.HexNumber);
            }
            else
            {
                return UInt16.Parse(s);
            }
        }

        public static bool IsHex(this string s)
        {
            var input = s.Trim();

            var hasP = input.LastIndexOf('(') == 0 && input.IndexOf(')') == input.Length - 1;

            if (hasP)
            {
                input = input.Replace("(", "").Replace(")", "");
            }

            var regex = new Regex(@"^([A-F]|[a-f]|[0-9])*");

            if (input.IndexOf("0x") == 0 || (input.IndexOf("h") > 1 && input.IndexOf("h") == input.Length - 1))
            {
                var hex = input.Replace("0x", "").Replace("h", "");
                var aux = regex.Match(hex).Value;

                if(aux.Length == hex.Length && aux.Length >=1 && aux.Length<=2)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool Is16BitHex(this string s)
        {
            var input = s.Trim();

            var hasP = input.LastIndexOf('(') == 0 && input.IndexOf(')') == input.Length - 1;

            if (hasP)
            {
                input = input.Replace("(", "").Replace(")", "");
            }

            var regex = new Regex(@"^([A-F]|[a-f]|[0-9])*");

            if (input.IndexOf("0x") == 0 || (input.IndexOf("h") > 1 && input.IndexOf("h") == input.Length - 1))
            {
                var hex = input.Replace("0x", "").Replace("h", "");
                var aux = regex.Match(hex).Value;

                if (aux.Length == hex.Length && aux.Length >= 3 && aux.Length <= 4)
                {
                    return true;
                }
            }

            return false;
        }

        public static string GetHex(this string s)
        {
            var regex = new Regex(@"^([A-F]|[a-f]|[0-9])*");

            if (s.IndexOf("0x") == 0 || (s.IndexOf("h") > 1 && s.IndexOf("h") == s.Length - 1))
            {
                var hex = s.Replace("0x", "").Replace("h", "");
                var aux = regex.Match(hex).Value;

                if (aux.Length == hex.Length)
                {
                    return hex;
                }
            }

            return null;
        }
    }
}

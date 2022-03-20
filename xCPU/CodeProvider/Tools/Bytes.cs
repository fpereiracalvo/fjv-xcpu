using Fjv.xCPU.CodeProvider.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fjv.xCPU.CodeProvider.Tools
{
    public class Bytes
    {
        public static byte GetByte(string s)
        {
            if (s.IsHex())
            {
                return s.GetByte();
            }
            else if (s.IsBinaryByte())
            {
                return s.GetByteFromBinary();
            }
            else
            {
                return byte.Parse(s);
            }
        }

        public static sbyte GetSByte(string s)
        {
            if (s.IsHex())
            {
                return s.GetSByte();
            }
            else if (s.IsBinaryByte())
            {
                return s.GetSByteFromBinary();
            }
            else
            {
                return sbyte.Parse(s);
            }
        }

        public static UInt16 GetUint16(string s)
        {
            if (s.Is16BitHex())
            {
                return s.GetUInt16();
            }
            else if (s.IsBinaryWord())
            {
                return s.GetWordFromBinary();
            }
            else
            {
                return UInt16.Parse(s);
            }
        }
    }
}

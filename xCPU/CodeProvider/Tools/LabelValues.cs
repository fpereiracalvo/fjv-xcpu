using System;
using System.Collections.Generic;
using System.Linq;
using Fjv.xCPU.CodeProvider.Commons;

namespace Fjv.xCPU.CodeProvider.Tools
{
    public class LabelValues
    {
        public static string GetLabelValue(string data, Dictionary<string, LabelValue> labels)
        {
            var label = labels[data];

            if (label.Type.Equals(typeof(byte)))
            {
                return "0x" + (byte.Parse(label.Value.ToString())).ToString("x").PadLeft(2, '0');
            }
            else if (label.Type.Equals(typeof(ushort)))
            {
                return "0x" + string.Join("", BitConverter.GetBytes((ushort)label.Value).Reverse().Select(s => s.ToString("x").PadLeft(2, '0')).ToArray());
            }

            //todo: faltan tipos.

            return "";
        }
    }
}

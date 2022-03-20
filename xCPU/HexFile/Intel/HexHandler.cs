using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fjv.xCPU.HexFile.Intel
{
    public class HexHandler : IHexHandler
    {
        private static string hexformat = ":{l}{a}{t}{d}{c}";

        public virtual string GetHexString(byte[] data)
        {
            var linesInt = data.Length / 16;
            var lines = ((decimal)data.Length / 16) > linesInt ? linesInt + 1 : linesInt;

            lines = lines > 0 ? lines : 1;

            using (var writer = new System.IO.StringWriter())
            {
                for (int i = 0; i < lines; i++)
                {
                    var hexline = GetHexLine(data.Skip(i * 16).Take(16).ToList(), i);

                    writer.WriteLine(hexline);
                }

                writer.WriteLine(":00000001FF");

                return writer.ToString();
            }
        }

        public virtual void Save(string path, byte[] data)
        {
            var content = GetHexString(data);

            System.IO.File.WriteAllText(path, content);
        }

        private string GetHexLine(List<byte> line, int index)
        {
            var hexline = hexformat.Replace("{t}", "00");
            hexline = hexline.Replace("{a}", $"{(index * 16):X}".PadLeft(4, '0'));
            hexline = hexline.Replace("{l}", $"{line.Count:X}".PadLeft(2, '0'));
            hexline = hexline.Replace("{d}", string.Join("", line.Select(s => $"{s:X}".PadLeft(2, '0'))));

            var address = BitConverter.GetBytes(index * 16);
            var sum = ((byte)address.Sum(s => (byte)s) + (byte)line.Count + line.Sum(s => (byte)s));
            var checksum = (byte)((~(byte)sum) + (byte)0x01);
            hexline = hexline.Replace("{c}", $"{checksum:X}".PadLeft(2, '0'));

            return hexline;
        }
    }
}

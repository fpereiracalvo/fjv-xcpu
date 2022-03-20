using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fjv.xCPU.CodeCompiler
{
    public class LineCode
    {
        public string Line { get; set; }
        public byte[] Bytes { get; set; }
        public int SourceIndex { get; set; }
        public int MemoryIndex { get; set; }

        public LineCode(int sindex, int mindex, string line, byte[] bytes)
        {
            this.Line = line;
            this.Bytes = bytes;
            this.SourceIndex = sindex;
            this.MemoryIndex = mindex;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fjv.xCPU.CodeProvider.Commons
{
    public interface ICommonType
    {
        string Mnemonic { get; }
        char[] Separator { get; }
    }
}

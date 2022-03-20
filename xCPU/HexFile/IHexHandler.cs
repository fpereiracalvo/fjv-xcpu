using System;
using System.Collections.Generic;
using System.Text;

namespace Fjv.xCPU.HexFile
{
    public interface IHexHandler
    {
        void Save(string path, byte[] data);
        string GetHexString(byte[] data);
    }
}

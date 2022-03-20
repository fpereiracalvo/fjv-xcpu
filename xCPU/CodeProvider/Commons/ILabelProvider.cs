using System;
using System.Collections.Generic;

namespace Fjv.xCPU.CodeProvider.Commons
{
    public interface ILabelProvider
    {
        List<LabelValue> Get();
        LabelValue Get(string labelname);
        Dictionary<string, LabelValue> GetDictionary();
        string GetString(string labelname);
        void Add(string labelname, LabelValue value);
    }
}

using Fjv.xCPU.CodeProvider.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fjv.xCPU.CodeProvider.Tools
{
    public class LabelHandler
    {
        public static T GetValue<T>(LabelValue label)
        {
            if (typeof(byte).Equals(typeof(T)))
            {
                var value = (T)label.Value;

                return value;
            }

            throw new Exception($"El tipo {typeof(T).ToString()}...");
        }
    }
}

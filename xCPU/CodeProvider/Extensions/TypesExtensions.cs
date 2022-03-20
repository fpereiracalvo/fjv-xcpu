using Fjv.xCPU.CodeProvider.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fjv.xCPU.CodeProvider.Extensions
{
    public static class TypesExtensions
    {
        public static Type Left(this Type[] types)
        {
            return types?.Length >= 1 ? types[0] : typeof(EmptyOperandType);
        }

        public static Type Right(this Type[] types)
        {
            return types?.Length > 1 ? types[1] : typeof(EmptyOperandType);
        }
    }
}

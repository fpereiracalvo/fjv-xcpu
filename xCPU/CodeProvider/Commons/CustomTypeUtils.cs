using Fjv.xCPU.CodeProvider.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fjv.xCPU.CodeProvider.Commons
{
    public class CustomTypeUtils
    {
        public static T Get<T>(string c)
            where T : ICommonType
        {
            var props = typeof(T).GetFields();
            var aux = c.Replace("(", "").Replace(")", "");

            foreach (var item in props)
            {
                if (item.FieldType.Equals(typeof(T)))
                {
                    var value = (T)item.GetValue(typeof(T));

                    if (aux.IndexOf(value.Mnemonic) >= 0)
                    {
                        return value;
                    }
                }
            }

            throw new Exception("The value does't exist.");
        }

        public static Object Get(string c, Type type)
        {
            var props = type.GetFields();
            var aux = c.Replace("(", "").Replace(")", "");

            foreach (var item in props)
            {
                if (item.FieldType.Equals(type))
                {
                    var value = (ICommonType)item.GetValue(type);

                    if (aux.IndexOf(value.Mnemonic) >= 0)
                    {
                        return value;
                    }
                }
            }

            throw new Exception("The value does't exist.");
        }

        public static ICommonType[] GetRegisterFields(Type type)
        {
            var fields = new List<ICommonType>();
            var props = type.GetFields();

            foreach (var item in props)
            {
                fields.Add((ICommonType)item.GetValue(type));
            }

            return fields.ToArray();
        }
    }
}

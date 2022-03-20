using System;
using System.Collections.Generic;
using System.Linq;

namespace Fjv.xCPU.CodeProvider.Commons
{
    public abstract class InstructionTypeBase<T> : ICommonType, ICloneable
    {
        /// <summary>
        /// Representación menemonica del comando de instrucción.
        /// </summary>
        public string Mnemonic { get; set; }
        public bool LeftPointer { get; set; }
        public bool RightPointer { get; set; }
        public Type[] Operand { get; set; }
        public ICommonType[] Constraints { get; set; }
        public IInstructionArgument Arguments { get; set; }
        /// <summary>
        /// Ejecuta conversión de instrucción a un array de T.
        /// </summary>
        public Func<IInstructionArgument, T[]> Instruction { get; set; }
        /// <summary>
        /// Retorna tamaño de la instrución.
        /// </summary>
        public Func<IInstructionArgument, int> Size { get; set; }

        public virtual char[] Separator { get; set; }
        /// <summary>
        /// Patrón Reyex. Personaliza de captura de parámetros de una instrucción si este no tiene operandos definidos.
        /// </summary>
        public virtual string RegexPattern { get; set; }
        public List<string> Replacements { get; set; }
        public string Line { get; set; }

        public virtual object Clone()
        {
            var model = Activator.CreateInstance(this.GetType());

            var properties = this.GetType().GetProperties();
            
            foreach (var item in properties)
            {
                var value = item.GetValue(this);
                var prop_ = model.GetType().GetProperties().SingleOrDefault(s => s.Name == item.Name);

                prop_.SetValue(model, value);
            }

            return model;
        }
    }
}

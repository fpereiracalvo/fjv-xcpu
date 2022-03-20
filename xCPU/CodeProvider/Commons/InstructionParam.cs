using Fjv.xCPU.CodeProvider.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fjv.xCPU.CodeProvider.Commons
{
    public class InstructionArgument : IInstructionArgument
    {
        public string Source { get; set; }

        public string Right { get; set; }
        public string Left { get; set; }

        public Object RightValue { get; set; }
        public Object LeftValue { get; set; }

        public Object Instruction { get; set; }

        public ILabelProvider Labels { get; set; }

        public object Clone()
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

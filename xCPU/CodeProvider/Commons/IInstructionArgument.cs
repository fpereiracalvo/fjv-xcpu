using Fjv.xCPU.CodeProvider.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fjv.xCPU.CodeProvider.Commons
{
    public interface IInstructionArgument : ICloneable
    {
        string Source { get; }

        /// <summary>
        /// Menemonic of right operand.
        /// </summary>
        string Right { get; }
        /// <summary>
        /// Menemonic of left operand.
        /// </summary>
        string Left { get; }

        /// <summary>
        /// Value of right.
        /// </summary>
        Object RightValue { get; }
        /// <summary>
        /// Value of left.
        /// </summary>
        Object LeftValue { get; }

        Object Instruction { get; }

        ILabelProvider Labels { get; }
    }
}

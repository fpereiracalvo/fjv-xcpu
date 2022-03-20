using System;
using Fjv.xCPU.CodeProvider.Commons;

namespace Fjv.xCPU.CodeProvider
{
    public class InstructionResolverProvider<T, TResult> : IInstructionResolverProvider<TResult>
         where T : InstructionTypeBase<TResult>
    {
        InstructionResolver<T, TResult> _instructionSet;

        public InstructionResolverProvider(InstructionResolver<T, TResult> instructionSet)
        {
            _instructionSet = instructionSet;
        }

        public InstructionTypeBase<TResult> GetInstruction(string line)
        {
            return _instructionSet.GetInstruction(line);
        }

        public bool HasInstruction(string line)
        {
            return _instructionSet.HasInstruction(line);
        }

        public bool IsExistCommonTypesMnemonic(string name)
        {
            return _instructionSet.IsExistCommonTypesMnemonic(name);
        }

        public void SetLabelProvider(ILabelProvider labelProvider)
        {
            _instructionSet.SetLabelStorage(labelProvider);
        }
    }
}

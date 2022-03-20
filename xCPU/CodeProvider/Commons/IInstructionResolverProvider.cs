using System;
using Fjv.xCPU.CodeProvider.Commons;

namespace Fjv.xCPU.CodeProvider.Commons
{
    public interface IInstructionResolverProvider<TResult>
    {
        bool IsExistCommonTypesMnemonic(string name);
        bool HasInstruction(string line);
        InstructionTypeBase<TResult> GetInstruction(string line);
        void SetLabelProvider(ILabelProvider labelProvider);
    }
}

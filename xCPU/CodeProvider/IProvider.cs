using Fjv.xCPU.CodeProvider.Commons;

namespace Fjv.xCPU.CodeProvider
{
    public interface IProvider<T, TResult>
        where T : InstructionTypeBase<TResult>
    {
        InstructionResolverProvider<T, TResult> GetInstructionResolverProvider();
    }
}

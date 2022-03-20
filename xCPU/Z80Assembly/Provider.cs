using System;
using Fjv.xCPU.CodeProvider;
using Fjv.xCPU.CodeProvider.Commons;
using Z80Assembly.Instructions;

namespace Z80Assembly
{
    public class Provider : IProvider<AssemblyInstructionType, AssemblyDataTypeResult>
    {
        public InstructionResolverProvider<AssemblyInstructionType, AssemblyDataTypeResult> GetInstructionResolverProvider()
        {
            return new InstructionResolverProvider<AssemblyInstructionType, AssemblyDataTypeResult> (new InstructionResolver<AssemblyInstructionType, AssemblyDataTypeResult>(new Z80AssemblyInstructionSet()));
        }
    }
}

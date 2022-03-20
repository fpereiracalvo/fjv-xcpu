using System;
using Fjv.xCPU.CodeProvider;
using Fjv.xCPU.CodeProvider.Commons;
using Fjv.Z80.Instructions;

namespace Fjv.Z80
{
    public class Provider : IProvider<CpuInstructionType, byte>
    {
        public InstructionResolverProvider<CpuInstructionType, byte> GetInstructionResolverProvider()
        {
            return new InstructionResolverProvider<CpuInstructionType, byte>(new InstructionResolver<CpuInstructionType, byte>(new Z84C00InstructionSet()));
        }
    }
}

using System;

namespace Fjv.xCPU.CodeProvider.Globals
{
    public sealed class Labels : SearchPattern
    {
        public Labels()
            : base (@"\b{label}\b", "{label}")
        { }

        public Labels(string pointerPattern)
            : base (pointerPattern, "{label}")
        { }

        public Labels(string pointerPattern, string replaceablePartPattern)
            : base(pointerPattern, replaceablePartPattern)
        { }
    }
}

using System;

namespace Fjv.xCPU.CodeProvider.Globals
{
    public sealed class Pointers : SearchPattern
    {
        public Pointers()
            : base (@"(?<=\().*(?=\))", ".*")
        { }

        public Pointers(string pointerPattern)
            : base (pointerPattern, ".*")
        { }

        public Pointers(string pointerPattern, string replaceablePartPattern)
            : base (pointerPattern, replaceablePartPattern) 
        { }
    }
}

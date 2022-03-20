using System;

namespace Fjv.xCPU.CodeProvider.Globals
{
    public abstract class SearchPattern
    {
        string _pattern;
        string _replaceablePartPattern;

        public string Pattern => _pattern;
        public string ReplaceablePartPattern => _replaceablePartPattern;

        public SearchPattern(string pointerPattern)
        {
            _pattern = pointerPattern;
            _replaceablePartPattern = "";
        }

        public SearchPattern(string pointerPattern, string replaceablePartPattern)
            : this(pointerPattern)
        {
            _replaceablePartPattern = replaceablePartPattern;
        }

        public virtual string Replace(string value)
        {
            if (string.IsNullOrWhiteSpace(_replaceablePartPattern))
            {
                return _pattern;
            }

            return _pattern.Replace(_replaceablePartPattern, value);
        }
    }
}

using Fjv.xCPU.CodeProvider.Commons;
using Fjv.xCPU.CodeProvider.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fjv.xCPU.CodeProvider
{
    public abstract class InstructionSetBase<T>
        where T : ICommonType
    {
        List<T> _set;
        List<Type> _types;

        Globals.SearchPattern _pointersPattern;
        Globals.SearchPattern _labelsPattern;

        public Globals.SearchPattern Pointers => _pointersPattern;
        public Globals.SearchPattern Labels => _labelsPattern;

        public virtual List<T> Instructions => _set;
        public virtual List<Type> Types => _types;
        public virtual string Name { get; set; }

        public virtual event EventHandler<string> OnBeginLongProcess;
        public virtual event EventHandler<string> OnEndLongProcess;

        public InstructionSetBase()
        {
            _set = new List<T>();
            _types = new List<Type>();

            _pointersPattern = new Globals.Pointers();
            _labelsPattern = new Globals.Labels();
        }

        public InstructionSetBase(Globals.SearchPattern pointersPattern)
            : this()
        {
            _pointersPattern = pointersPattern;
        }

        public InstructionSetBase(Globals.SearchPattern pointersPattern, Globals.SearchPattern labelsPattern)
            : this(pointersPattern)
        {
            _labelsPattern = labelsPattern;
        }

        public virtual InstructionSetBase<T> AddInstruction(T instruction)
        {
            this.Instructions.Add(instruction);

            return this;
        }

        public virtual InstructionSetBase<T> AddInstructions(List<T> instructions)
        {
            this.Instructions.AddRange(instructions);

            return this;
        }

        public virtual InstructionSetBase<T> AddType(Type type)
        {
            this.Types.Add(type);

            return this;
        }

        public virtual Type GetParamType(string value)
        {
            //bytes.
            if (byte.TryParse(value, out byte b))
            {
                return typeof(byte);
            }
            else if (sbyte.TryParse(value, out sbyte sb))
            {
                return typeof(sbyte);
            }
            else if (UInt16.TryParse(value, out UInt16 w))
            {
                return typeof(UInt16);
            }

            //hexadecimal 8 y 16 bits.
            if (value.IsHex())
            {
                return typeof(byte);
            }
            else if (value.Is16BitHex())
            {
                return typeof(UInt16);
            }

            //binary string 8 bits.
            if (value.IsBinaryWord())
            {
                return typeof(UInt16);
            }
            else if (value.IsBinaryByte())
            {
                return typeof(byte);
            }

            //common types.
            if (TryGetICommonType(value, out Type type))
            {
                return type;
            }

            return typeof(EmptyOperandType);
        }

        private bool TryGetICommonType(string value, out Type type)
        {
            var result = GetCommonTypes().Select(s =>
            {
                var str = value.Split(s.Separator).Where(x => x.Length > 0).ToArray().FirstOrDefault()?.Trim();

                var isEqual = s.Mnemonic.Equals(str);

                return new
                {
                    isEqual,
                    type = s.GetType()
                };
            }).Where(s => s.isEqual).SingleOrDefault();

            if (result == null)
            {
                type = null;
                return false;
            }

            type = result?.type;
            return true;
        }

        public virtual bool IsPointer(string v, Type type)
        {
            var types = new Type[] { typeof(byte), typeof(UInt16) };

            if (!types.Contains(type))
            {
                var match = GetCommonTypes().Select(s =>
                {
                    var parts = v.Split(s.Separator).Where(x => x.Length > 0).FirstOrDefault().Trim();

                    var isEqual = s.Mnemonic.Equals(parts);

                    return new {
                        isEqual,
                        hasMatch = IsPointer(s.Mnemonic, v)
                    };
                }).Where(s => s.isEqual && s.hasMatch).SingleOrDefault();

                return match != null;
            }


            return false;
        }

        private bool IsPointer(string mnemonic, string value)
        {
            var pattern = _pointersPattern.Replace(mnemonic);

            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            var matches = regex.Matches(value.Replace(" ", ""));

            return matches.Count == 1;
        }

        public virtual bool IsPointer(string s)
        {
            var input = s.Trim();

            return HasMatch(input);
        }

        public virtual string[] GetMnemonics()
        {
            return this.Instructions.Select(s => s.Mnemonic).Distinct().OrderBy(s => s).ToArray();
        }

        public virtual string[] GetCommonTypesMnemonics()
        {
            var fields = GetCommonTypes();

            return fields.Select(s => s.Mnemonic).Distinct().OrderBy(s => s).ToArray();
        }

        private ICommonType[] GetCommonTypes()
        {
            var fields = new List<ICommonType>();

            Types.Where(s => s.GetInterface(nameof(ICommonType)) != null)
                .ToList()
                .ForEach(s => fields.AddRange(CustomTypeUtils.GetRegisterFields(s)));

            return fields.ToArray();
        }

        private bool HasMatch(string value)
        {
            var regex = new Regex(_pointersPattern.Pattern);

            var match = regex.Match(value);

            return match.Success;
        }
    }
}

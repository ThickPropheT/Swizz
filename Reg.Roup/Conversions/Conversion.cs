using System;
using System.ComponentModel;

namespace Reg.Roup.Conversions
{
    public partial class Conversion
    {
        private readonly GroupValue _value;
        private readonly Func<string?, object?> _convert;

        private Conversion(GroupValue value, Func<string?, object?> convert)
        {
            _value = value;
            _convert = convert;
        }

        public static Conversion Explicit(GroupValue value, Func<string?, object?> convert)
            => new(value, convert);

        public static Conversion Implicit(GroupValue value)
            => new(value, v => TypeDescriptor.GetConverter(value.Member.Type).ConvertTo(v, value.Member.Type));

        public static Conversion None(GroupValue value)
            => new(value, v => v);

        public object? Apply()
            => _convert(_value.Get());
    }
}

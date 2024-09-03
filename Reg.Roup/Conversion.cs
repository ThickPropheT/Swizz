﻿using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace Reg.Roup
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
            => new Conversion(value, convert);

        public static Conversion Implicit(GroupValue value)
            => new Conversion(value, v => TypeDescriptor.GetConverter(value.Member.Type).ConvertTo(v, value.Member.Type));

        public static Conversion None(GroupValue value)
            => new Conversion(value, v => v);

        public object? Apply()
            => _convert(_value.GetValue());
    }
}

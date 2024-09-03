using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Reg.Roup
{
    public partial class Conversion
    {
        public static MatchSelector Extract(SchemaMember member)
            => new(member);

        public class MatchSelector(SchemaMember member)
        {
            private readonly SchemaMember _member = member;

            public Conversion From(MatchContext match)
            {
                var converter = _member.FindConverter();
                var value = match.FindValue(_member);

                if (converter != null)
                {
                    return Conversion.Explicit(value, converter);
                }
                else if (_member.Type != typeof(string))
                {
                    return Conversion.Implicit(value);
                }
                else
                {
                    return Conversion.None(value);
                }
            }

        }
    }
}

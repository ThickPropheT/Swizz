namespace Reg.Roup
{
    public partial class Conversion
    {
        public static MatchSelector Extract(SchemaMember member)
            => new(member);

        public class MatchSelector(SchemaMember member)
        {
            public Conversion From(MatchContext match)
            {
                var converter = member.FindConverter();
                var value = match.FindValue(member);

                if (converter != null)
                {
                    return Conversion.Explicit(value, converter);
                }
                else if (member.Type != typeof(string))
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

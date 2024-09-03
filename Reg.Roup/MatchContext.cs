using System;
using System.Text.RegularExpressions;

namespace Reg.Roup
{
    public class MatchContext(Regex? regex, Match match)
    {
        public void Validate()
        {
            if (!match.Success)
            {
                throw new FormatException(
                    "Regex could not match input string."
                );
            }
        }

        public GroupValue FindValue(SchemaMember member)
        {
            var groupName = member.Name;

            if (regex != null && regex.GroupNumberFromName(groupName) == -1)
            {
                throw new FormatException(
                    $"Regex does not contain a group definition named '{groupName}'."
                );
            }

            return member.FindValueIn(match.Groups);
        }
    }
}

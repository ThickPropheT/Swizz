using System;
using System.Text.RegularExpressions;

namespace Reg.Roup
{
    public class MatchContext
    {
        private readonly Regex? _regex;
        private readonly Match _match;

        public MatchContext(Regex? regex, Match match)
        {
            _regex = regex;
            _match = match;
        }

        public void Validate()
        {
            if (!_match.Success)
            {
                throw new FormatException(
                    "Regex could not match input string."
                );
            }
        }

        public GroupValue FindValue(SchemaMember member)
        {
            var groupName = member.Name;

            if (_regex != null && _regex.GroupNumberFromName(groupName) == -1)
            {
                throw new FormatException(
                    $"Regex does not contain a group definition named '{groupName}'."
                );
            }

            return member.FindValueIn(_match.Groups);
        }
    }
}

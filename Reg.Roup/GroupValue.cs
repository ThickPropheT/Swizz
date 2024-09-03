using System;
using System.Text.RegularExpressions;

namespace Reg.Roup
{
    public class GroupValue
    {
        private readonly Group? _group;

        public SchemaMember Member { get; }

        private GroupValue(SchemaMember member, Group? group)
        {
            Member = member;
            _group = group;
        }

        public static GroupValue FromGroup(SchemaMember member, Group group)
            => new GroupValue(member, group);

        public static GroupValue FromOptional(SchemaMember member)
            => new GroupValue(member, null);

        public string? GetValue() => _group?.Value;
    }
}

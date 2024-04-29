namespace Swizz
{
    public struct SwissVersion
    {
        public ulong Major { get; }
        public ulong Minor { get; }
        public ulong Revision { get; }

        public SwissVersion(ulong major, ulong minor, ulong revision)
        {
            Major = major;
            Minor = minor;
            Revision = revision;
        }

        public static SwissVersion Parse(string version)
        {
            try
            {
                var parts = version
           .ToLower()
           .Replace("v", "")
           .Replace('r', '.')
           .Split('.');

                return new SwissVersion(
                    ulong.Parse(parts[0]),
                    ulong.Parse(parts[1]),
                    ulong.Parse(parts[2]));
            }
            catch (Exception ex)
            {
                throw new FormatException($"The innput string '{version}' was not in a correct format.", ex);
            }
        }

        public static bool TryParse(string version, out SwissVersion result)
        {
            try
            {
                result = Parse(version);
                return true;
            }
            catch
            {
                result = default;
                return false;
            }
        }

        public override string ToString()
            => $"v{Major}.{Minor}r{Revision}";


        public static bool operator >(SwissVersion left, SwissVersion right)
            => left.Major > right.Major
                || left.Minor > right.Minor
                || left.Revision > right.Revision;

        public static bool operator >=(SwissVersion left, SwissVersion right)
            => left.Major >= right.Major
                || left.Minor >= right.Minor
                || left.Revision >= right.Revision;

        public static bool operator <(SwissVersion left, SwissVersion right)
            => left.Major < right.Major
                || left.Minor < right.Minor
                || left.Revision < right.Revision;

        public static bool operator <=(SwissVersion left, SwissVersion right)
            => left.Major <= right.Major
                || left.Minor <= right.Minor
                || left.Revision <= right.Revision;

        public static bool operator ==(SwissVersion left, SwissVersion right)
            => left.Major == right.Major
                && left.Minor == right.Minor
                && left.Revision == right.Revision;

        public static bool operator !=(SwissVersion left, SwissVersion right)
            => left.Major != right.Major
                && left.Minor != right.Minor
                && left.Revision != right.Revision;
    }
}
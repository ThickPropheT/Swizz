using Newtonsoft.Json;

namespace Swizz.Installation
{
    public class InstallationMetadata
    {
        public SwissVersion Version { get; }

        [JsonIgnore]
        public DirectoryInfo Location { get; }

        public InstallationMetadata(DirectoryInfo location)
        {
            Location = location;
        }
    }
}
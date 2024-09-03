using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace Swizz.Installation
{
    public class InstallationSchema
    {
        public const string MetadataFileName = ".swizz";

        public static async Task<InstallationMetadata> ReadMetadataFrom(DirectoryInfo directory)
            => (await ReadMetadataFrom(directory,

                onNotFound: () => throw new FileNotFoundException(
                    $"Directory at '{directory.FullName}' does not contain a Swizz managed installation of Swiss."),

                onBadFormat: file => throw new FormatException(
                    $"Metadata file '{file.FullName}' is not in a valid format.")))!;

        public static async Task<InstallationMetadata?> TryReadMetadataFrom(DirectoryInfo directory)
            => await ReadMetadataFrom(directory, onNotFound: () => null, onBadFormat: _ => null);

        private static async Task<InstallationMetadata?> ReadMetadataFrom(
            DirectoryInfo directory, Func<InstallationMetadata?> onNotFound, Func<FileInfo, InstallationMetadata?> onBadFormat)
        {
            var file = directory.EnumerateFiles(MetadataFileName).FirstOrDefault();

            if (file == null)
                return onNotFound();

            using var reader = new StreamReader(file.OpenRead());
            string json = await reader.ReadToEndAsync();

            var metadata = new InstallationMetadata(directory);

            JsonConvert.PopulateObject(json, metadata);

            if (metadata.Version.IsEmpty)
                return onBadFormat(file);

            return metadata;
        }

        public static async Task WriteMetadataTo(InstallationMetadata metadata, DirectoryInfo directory)
        {
            var json = JsonConvert.SerializeObject(metadata);

            await File.WriteAllTextAsync($"{directory.FullName}/{MetadataFileName}", json);
        }
    }
}

using Newtonsoft.Json;

namespace Swizz
{
    public class InstallationSchema
    {
        public const string MetadataFileName = ".swizz";

        public static async Task<InstallationMetadata> ReadMetadataFrom(DirectoryInfo directory)
        {
            var file = directory.EnumerateFiles(MetadataFileName).FirstOrDefault();

            if (file == null)
                throw new FileNotFoundException(
                    $"Directory at '{directory.FullName}' does not contain a Swizz managed installation of Swiss.");

            using var reader = new StreamReader(file.OpenRead());
            string json = await reader.ReadToEndAsync();

            var metadata = JsonConvert.DeserializeObject<InstallationMetadata>(json);

            if (metadata == null)
                throw new FormatException(
                    $"Metadata file '{file.FullName}' is not in a valid format.");

            return metadata;
        }

        public static async Task WriteMetadataTo(InstallationMetadata metadata, DirectoryInfo directory)
        {
            var json = JsonConvert.SerializeObject(metadata);

            await File.WriteAllTextAsync($"{directory.FullName}/{MetadataFileName}", json);
        }
    }
}

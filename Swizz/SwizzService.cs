namespace Swizz
{
    public class SwizzService
    {
        private readonly DirectoryInfo _targetDirectory;
        private readonly IInstallerSchemaRepository _installerSchemaRepository;

        public SwizzService(DirectoryInfo targetDirectory, IInstallerSchemaRepository installerSchemaRepository)
        {
            _targetDirectory = targetDirectory;
            _installerSchemaRepository = installerSchemaRepository;
        }

        public async Task<SwissVersion> GetVersion()
            => (await GetInstallationMetadata()).Version;

        public async Task Install(string repositoryUrl, bool force)
        {
            // 0. Pull latest from github
            var schema = await _installerSchemaRepository.Pull(repositoryUrl);

            var metadata = await GetInstallationMetadata();

            // 1. Check if update is needed
            if (!force && schema.Version <= metadata.Version)
                return;

            /*
             * TODO
             * 
             * 2. Format disk (optional?)
             * 3. Move files
             * 
             */
        }

        private async Task<InstallationMetadata> GetInstallationMetadata()
            => await InstallationSchema.ReadMetadataFrom(_targetDirectory);
    }
}

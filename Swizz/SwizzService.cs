namespace Swizz
{
    public class SwizzService
    {
        private readonly InstallationMetadata _installationMetadata;
        private readonly IInstallerSchemaRepository _installerSchemaRepository;

        public SwizzService(InstallationMetadata installationMetadata, IInstallerSchemaRepository installerSchemaRepository)
        {
            _installationMetadata = installationMetadata;
            _installerSchemaRepository = installerSchemaRepository;
        }

        public SwissVersion GetVersion()
            => _installationMetadata.Version;

        public async Task Install(string repositoryUrl, bool force)
        {
            // 0. Pull latest from github
            var schema = await _installerSchemaRepository.Pull(repositoryUrl);

            // 1. Check if update is needed
            if (!force && schema.Version <= _installationMetadata.Version)
                return;

            /*
             * TODO
             * 
             * 2. Format disk (optional?)
             * 3. Move files
             * 
             */
        }
    }
}

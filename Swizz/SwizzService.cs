using Swizz.Installation;
using Swizz.Release;
using Swizz.Release.Provider;

namespace Swizz
{
    public class SwizzService
    {
        private readonly IReleasePackageProvider _releaseProvider;

        public SwizzService(IReleasePackageProvider releaseProvider)
        {
            _releaseProvider = releaseProvider;
        }

        public async Task<SwissVersion?> GetVersion(DirectoryInfo targetDirectory)
            => (await InstallationSchema.TryReadMetadataFrom(targetDirectory))?.Version;

        public async Task Install(DirectoryInfo targetDirectory, bool force)
        {
            var currentInstallation = await InstallationSchema.TryReadMetadataFrom(targetDirectory);
            var latestRelease = await _releaseProvider.GetLatest(force);

            var isInstalled = currentInstallation != null;
            var shallInstall = !isInstalled || force;
            var mayInstall = isInstalled && latestRelease.Version <= currentInstallation!.Version;

            if (!shallInstall && !mayInstall)
                return;

            await (!isInstalled
                ? CreateInstallation(targetDirectory, latestRelease)
                : UpdateInstallation(currentInstallation!, latestRelease));
        }

        public async Task CreateInstallation(DirectoryInfo targetDirectory, ReleasePackage releasePackage)
        {

        }

        public async Task UpdateInstallation(InstallationMetadata installation, ReleasePackage releasePackage)
        {

        }

        //public async Task Install(DirectoryInfo targetDirectory, string repositoryUrl, bool force)
        //{
        //    // 0. Pull latest from github
        //    var schema = await _installerSchemaRepository.Pull(repositoryUrl);

        //    // 1. Check if update is needed
        //    if (!force && schema.Version <= _installation.Version)
        //        return;

        //    /*
        //     * TODO
        //     * 
        //     * 2. Format disk (optional?)
        //     * 3. Move files
        //     * 
        //     */
        //}
    }
}

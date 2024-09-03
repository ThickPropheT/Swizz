using System.Management.Automation;
using Swizz.Installation;

namespace Swizz.Installer
{
    public class Git : IInstallerSchemaRepository
    {
        private readonly DirectoryInfo _targetDirectory;

        public Git(DirectoryInfo targetDirectory)
        {
            _targetDirectory = targetDirectory;
        }

        public async Task<InstallerSchema> Pull(string repositoryUrl)
        {
            using (PowerShell powershell = PowerShell.Create())
            {
                powershell.AddScript($"cd {_targetDirectory}");
                powershell.AddScript($"git clone {repositoryUrl}");

                var results = await powershell.InvokeAsync();

                // TODO analyze results?
            }

            var metadata = await InstallationSchema.ReadMetadataFrom(_targetDirectory);

            return new() { Version = metadata.Version };
        }
    }
}
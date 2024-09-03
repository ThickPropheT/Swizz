namespace Swizz.Installer
{
    public interface IInstallerSchemaRepository
    {
        Task<InstallerSchema> Pull(string repositoryUrl);
    }
}
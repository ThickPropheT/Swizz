namespace Swizz
{
    public interface IInstallerSchemaRepository
    {
        Task<InstallerSchema> Pull(string repositoryUrl);
    }
}
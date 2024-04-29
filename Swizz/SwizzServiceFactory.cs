namespace Swizz
{
    public class SwizzServiceFactory
    {
        public SwizzService Create(DirectoryInfo targetDirectory)
            => new(targetDirectory, new Git(targetDirectory));
    }
}

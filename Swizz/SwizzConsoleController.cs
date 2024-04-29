namespace Swizz
{
    public class SwizzConsoleController
    {
        private readonly Func<Task<int>> _evaluate;
        private readonly SwizzServiceFactory _serviceFactory;

        public SwizzConsoleController(Func<Task<int>> evaluate, SwizzServiceFactory serviceFactory)
        {
            _evaluate = evaluate;
            _serviceFactory = serviceFactory;
        }

        public async Task<int> Evaluate() => await _evaluate();

        public async Task PrintVersionAt(DirectoryInfo targetDirectory)
        {
            var version = await _serviceFactory.Create(targetDirectory).GetVersion();
            Console.WriteLine(version);
        }

        public async Task InstallAt(DirectoryInfo targetDirectory, string repositoryUrl, bool force)
        {
            await _serviceFactory.Create(targetDirectory).Install(repositoryUrl, force);
            // TODO get feedback and print result
        }
    }
}

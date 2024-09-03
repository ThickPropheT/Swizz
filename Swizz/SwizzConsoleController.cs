namespace Swizz
{
    // TODO should this be converted to media agnostic controller?
    public class SwizzConsoleController
    {
        private readonly SwizzService _service;

        public SwizzConsoleController(SwizzService service)
        {
            _service = service;
        }

        public async Task PrintVersion(DirectoryInfo targetDirectory)
        {
            var version = await _service.GetVersion(targetDirectory);
            // TODO
            //  can/should System.CommandLine be used to print this?
            //  should the endpoint be converted to return version rather than print it?
            Console.WriteLine(version);
        }

        public async Task InstallAt(DirectoryInfo targetDirectory, bool force)
        {
            await _service.Install(targetDirectory, force);
            // TODO get feedback and print result
        }
    }
}

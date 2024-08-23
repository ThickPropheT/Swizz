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

        public void PrintVersion()
        {
            var version = _service.GetVersion();
            // TODO
            //  can/should System.CommandLine be used to print this?
            //  should the endpoint be converted to return version rather than print it?
            Console.WriteLine(version);
        }

        public async Task InstallAt(string repositoryUrl, bool force)
        {
            await _service.Install(repositoryUrl, force);
            // TODO get feedback and print result
        }
    }
}

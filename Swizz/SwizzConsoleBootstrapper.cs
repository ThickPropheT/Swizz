using System.CommandLine;
using System.Net.Http.Headers;
using Swizz.Release.Provider;
using Swizz.Release.Provider.GitHub;

namespace Swizz
{
    public record SwizzConsoleContext(Func<Task<int>> Evaluate);

    public static class SwizzConsoleBootstrapper
    {
        public static SwizzConsoleContext Bootstrap(string[] args)
        {
            var targetOption = CreateTargetOption();
            var rootCommand = CreateRootCommand(targetOption);

            rootCommand
                .AddVersionCommand(targetOption)
                .AddInstallCommand(targetOption);

            return new (async () => await rootCommand.InvokeAsync(args));
        }

        private static Option<DirectoryInfo> CreateTargetOption()
        {
            var targetOption = new Option<DirectoryInfo>(
                name: "--target",
                description: "The target Swiss-GC installation directory.",
                getDefaultValue: () => new DirectoryInfo("."));

            targetOption.AddAlias("-t");

            return targetOption;
        }

        private static RootCommand CreateRootCommand(Option<DirectoryInfo> targetOption)
        {
            var rootCommand = new RootCommand("Swizz - The Swiss-GC installation wizard.");
            rootCommand.AddGlobalOption(targetOption);

            return rootCommand;
        }

        private static RootCommand AddVersionCommand(this RootCommand rootCommand, Option<DirectoryInfo> targetOption)
        {
            var versionCommand = new Command(
                name: "version",
                description: "Show version information for target Swiss installation.");

            rootCommand.AddCommand(versionCommand);

            versionCommand.SetHandler(
                async dir => await ResolveControllerAndCall(dir, c => c.PrintVersion(dir)),
                targetOption
            );

            return rootCommand;
        }

        private static RootCommand AddInstallCommand(this RootCommand rootCommand, Option<DirectoryInfo> targetOption)
        {
            var repositoryUrlArgument = new Argument<string>("--repositoryUrl", "TODO - 1");

            var forceOption = new Option<bool>("--force", "TODO - 2");
            forceOption.AddAlias("-f");

            var versionOption = new Option<string?>(
                name:  "--version",
                description: "TODO - 3",
                getDefaultValue: () => null);
            
            versionOption.AddAlias("-v");

            var installCommand = new Command("install", "TODO - 4");
            //installCommand.AddArgument(repositoryUrlArgument);
            installCommand.AddOption(forceOption);
            installCommand.AddOption(versionOption);

            rootCommand.AddCommand(installCommand);

            installCommand.SetHandler(
                async (dir, force) => await ResolveControllerAndCall(dir, c => c.InstallAt(dir, force)),
                targetOption, forceOption
            );

            return rootCommand;
        }

        //var configOption = new Option<string?>(
        //    name: "--config",
        //    description: "The Swizz configuration file.");
        //configOption.AddAlias("-c");

        private static async Task ResolveControllerAndCall(DirectoryInfo targetDirectory, Func<SwizzConsoleController, Task> endpoint)
            => await endpoint(ResolveController(targetDirectory));

        private static void ResolveControllerAndCall(DirectoryInfo targetDirectory, Action<SwizzConsoleController> endpoint)
            => endpoint(ResolveController(targetDirectory));

        private static SwizzConsoleController ResolveController(DirectoryInfo targetDirectory)
        {
            // TODO
            //  consider using HttpClientFactory instead of manually creating this.
            //  to do that, you'd need to:
            //  https://stackoverflow.com/questions/52622586/can-i-use-httpclientfactory-in-a-net-core-app-which-is-not-asp-net-core
            var client = new HttpClient();
            //{
            //    BaseAddress = new Uri("https://api.github.com/")
            //};

            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(
            //    new MediaTypeWithQualityHeaderValue("application/json"));

            //var gitHubApi = new GitHubReleaseApi(client);
            var gitHubScraper = new GitHubReleasePageScraper(client, targetDirectory /* TODO should probably find a place in AppData instead */);
            var service = new SwizzService(gitHubScraper);
            return new SwizzConsoleController(service);
        }
    }
}

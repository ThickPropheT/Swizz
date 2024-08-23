using System.CommandLine;
using System.Data;

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
                async t => await ResolveControllerAndCall(t, c => c.PrintVersion()),
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
            installCommand.AddArgument(repositoryUrlArgument);
            installCommand.AddOption(forceOption);
            installCommand.AddOption(versionOption);

            rootCommand.AddCommand(installCommand);

            installCommand.SetHandler(
                async (t, url, force) => await ResolveControllerAndCall(t, c => c.InstallAt(url, force)),
                targetOption, repositoryUrlArgument, forceOption
            );

            return rootCommand;
        }

        //var configOption = new Option<string?>(
        //    name: "--config",
        //    description: "The Swizz configuration file.");
        //configOption.AddAlias("-c");

        private static async Task ResolveControllerAndCall(DirectoryInfo targetDirectory, Func<SwizzConsoleController, Task> endpoint)
            => await endpoint(await ResolveController(targetDirectory));

        private static async Task ResolveControllerAndCall(DirectoryInfo targetDirectory, Action<SwizzConsoleController> endpoint)
            => endpoint(await ResolveController(targetDirectory));

        private static async Task<SwizzConsoleController> ResolveController(DirectoryInfo targetDirectory)
        {
            var metaData = await InstallationSchema.ReadMetadataFrom(targetDirectory);
            var service = new SwizzService(metaData, new Git(targetDirectory));
            return new SwizzConsoleController(service);
        }
    }
}

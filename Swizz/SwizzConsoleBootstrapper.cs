using System.CommandLine;

namespace Swizz
{
    public class SwizzConsoleBootstrapper
    {
        public static SwizzConsoleController Bootstrap(string[] args)
        {
            var targetOption = CreateTargetOption();
            var rootCommand = CreateRootCommand(targetOption);

            var controller = new SwizzConsoleController(
                async () => await rootCommand.InvokeAsync(args),
                new SwizzServiceFactory());

            AddVersionCommand(rootCommand, targetOption, controller);
            AddInstallCommand(rootCommand, targetOption, controller);

            return controller;
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

        private static void AddVersionCommand(RootCommand rootCommand, Option<DirectoryInfo> targetOption, SwizzConsoleController controller)
        {
            var versionCommand = new Command(
                name: "version",
                description: "Show version information for target Swiss installation.");

            rootCommand.AddCommand(versionCommand);

            versionCommand.SetHandler(controller.PrintVersionAt, targetOption);
        }

        private static void AddInstallCommand(RootCommand rootCommand, Option<DirectoryInfo> targetOption, SwizzConsoleController controller)
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

            installCommand.SetHandler(controller.InstallAt, targetOption, repositoryUrlArgument, forceOption);
        }

        //var configOption = new Option<string?>(
        //    name: "--config",
        //    description: "The Swizz configuration file.");
        //configOption.AddAlias("-c");
    }
}

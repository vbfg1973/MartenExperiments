namespace Cli
{
    using CommandLine;
    using Core;
    using Core.Configuration;
    using Core.Marten;
    using Domain;
    using Features;
    using Features.Test;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;

    public static class Program
    {
        private static IConfiguration s_configuration;
        private static IServiceCollection s_serviceCollection;
        private static IServiceProvider s_serviceProvider;

        internal static void Main(string[] args)
        {
            BuildConfiguration();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(s_configuration)
                .WriteTo.Console()
                .CreateLogger();

            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                Log.Logger.Error(e.ExceptionObject as Exception, "UnhandledException");
            };

            ConfigureServices();

            ParseCommandLineArguments(args);
        }

        public static void ParseCommandLineArguments(string[] args)
        {
            Parser.Default
                .ParseArguments<
                    TestOptions
                >(args)
                .WithParsed(options =>
                {
                    var verb = s_serviceProvider.GetService<TestVerb>();

                    verb?.Run(options)
                        .Wait();
                });
        }

        private static void ConfigureServices()
        {
            s_serviceCollection = new ServiceCollection();

            s_serviceCollection.AddLogging(configure => configure.AddSerilog());

            s_serviceCollection
                .ConfigureMartenServices(s_configuration)
                .AddCoreServices()
                .AddDomainServices()
                .AddVerbs()
                ;

            s_serviceProvider = s_serviceCollection.BuildServiceProvider();
        }

        private static void BuildConfiguration()
        {
            ConfigurationBuilder configuration = new();

            s_configuration = configuration.AddJsonFile("appsettings.json", true, true)
                // .AddJsonFile($"appsettings.{environmentName}.json", true, true)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}

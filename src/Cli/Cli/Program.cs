namespace Cli
{
    using System.Net;
    using CommandLine;
    using Core;
    using Domain;
    using Features;
    using Features.LoadSkills;
    using Features.Test;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;
    using Services;

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
                var ex = e.ExceptionObject as Exception;
                Log.Logger.Error(ex, "UnhandledException {ExceptionMessage}", ex!.Message);
            };

            ConfigureServices();

            ParseCommandLineArguments(args);

            Log.CloseAndFlush();
        }

        private static void ParseCommandLineArguments(string[] args)
        {
            Parser.Default
                .ParseArguments<
                    LoadSkillsOptions,
                    TestOptions
                >(args)
                .WithParsed<TestOptions>(options =>
                {
                    var verb = s_serviceProvider.GetService<TestVerb>();

                    verb?.Run(options)
                        .Wait();
                })
                .WithParsed<LoadSkillsOptions>(options =>
                {
                    var verb = s_serviceProvider.GetService<LoadSkillsVerb>();

                    verb?.Run(options)
                        .Wait();
                })
                ;
        }

        private static void ConfigureServices()
        {
            s_serviceCollection = new ServiceCollection();

            s_serviceCollection.AddLogging(configure => configure.AddSerilog());

            s_serviceCollection
                .AddCoreServices()
                .AddDomainServices(s_configuration)
                .AddVerbs()
                .AddHttpClient<ApiClient>(client => client.BaseAddress = new Uri("http://localhost:51770/"))
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

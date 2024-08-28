namespace Core.Marten
{
    using Configuration;
    using global::Marten;
    using global::Marten.Events.Daemon.Resiliency;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Npgsql;
    using Weasel.Core;

    public class MartenConfig
    {
        private const string DefaultSchema = "public";

        public bool UseMetadata = true;

        public string ConnectionString { get; set; } = default!;

        public string WriteModelSchema { get; set; } = DefaultSchema;
        public string ReadModelSchema { get; set; } = DefaultSchema;

        public bool ShouldRecreateDatabase { get; set; } = false;

        public DaemonMode DaemonMode { get; set; } = DaemonMode.Solo;
    }

    public static class MartenConfigExtensions
    {
        private const string DefaultConfigKey = "EventStore";

        public static IServiceCollection ConfigureMartenServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            var martenConfig = configuration.GetRequiredConfig<MartenConfig>(DefaultConfigKey);

            services
                .AddMarten(sp =>
                {
                    var dataSource = sp.GetService<NpgsqlDataSource>();
                    if (dataSource == null)
                    {
                        return SetStoreOptions(martenConfig);
                    }

                    martenConfig.ConnectionString = dataSource.ConnectionString;
                    Console.WriteLine(dataSource.ConnectionString);

                    return SetStoreOptions(martenConfig);
                })
                .UseLightweightSessions()
                .ApplyAllDatabaseChangesOnStartup();

            return services;
        }

        private static StoreOptions SetStoreOptions(MartenConfig martenConfig)
        {
            var options = new StoreOptions();

            options.Connection(martenConfig.ConnectionString);
            options.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate;

            options.Events.DatabaseSchemaName = martenConfig.WriteModelSchema;
            options.DatabaseSchemaName = martenConfig.ReadModelSchema;

            options.UseNewtonsoftForSerialization(
                EnumStorage.AsString,
                nonPublicMembersStorage: NonPublicMembersStorage.All
            );

            options.Projections.Errors.SkipApplyErrors = false;
            options.Projections.Errors.SkipSerializationErrors = false;
            options.Projections.Errors.SkipUnknownEvents = false;

            options.Events.MetadataConfig.CausationIdEnabled = true;
            options.Events.MetadataConfig.CorrelationIdEnabled = true;
            options.Events.MetadataConfig.HeadersEnabled = true;

            return options;
        }
    }
}

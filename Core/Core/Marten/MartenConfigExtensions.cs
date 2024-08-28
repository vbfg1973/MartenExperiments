namespace Core.Marten
{
    using Configuration;
    using global::Marten;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Npgsql;
    using Weasel.Core;

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
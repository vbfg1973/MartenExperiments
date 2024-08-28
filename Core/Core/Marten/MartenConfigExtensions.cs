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

        public static MartenServiceCollectionExtensions.MartenConfigurationExpression ConfigureMartenServices(
            this IServiceCollection services,
            IConfiguration configuration,
            Action<StoreOptions>? configureOptions = null)
        {
            var martenConfig = configuration.GetRequiredConfig<MartenConfig>(DefaultConfigKey);

            var configurationExpression = services
                .AddMarten(sp =>
                {
                    var dataSource = sp.GetService<NpgsqlDataSource>();
                    if (dataSource == null)
                    {
                        return SetStoreOptions(martenConfig, configureOptions);
                    }

                    martenConfig.ConnectionString = dataSource.ConnectionString;
                    Console.WriteLine(dataSource.ConnectionString);

                    return SetStoreOptions(martenConfig);
                })
                .UseLightweightSessions()
                .ApplyAllDatabaseChangesOnStartup();

            return configurationExpression;
        }

        private static StoreOptions SetStoreOptions(MartenConfig martenConfig,
            Action<StoreOptions>? configureOptions = null)
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

            Console.WriteLine($"Configure Options Status: {configureOptions != null}");

            configureOptions?.Invoke(options);

            return options;
        }
    }
}

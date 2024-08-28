namespace Core.Marten
{
    using global::Marten.Events.Daemon.Resiliency;

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
}

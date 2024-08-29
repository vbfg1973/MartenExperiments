namespace Core.Serialization.Newtonsoft
{
    using global::Newtonsoft.Json;
    using global::Newtonsoft.Json.Converters;

    public static class SerializationExtensions
    {
        public static JsonSerializerSettings WithDefaults(this JsonSerializerSettings settings)
        {
            settings.WithNonDefaultConstructorContractResolver()
                .Converters.Add(new StringEnumConverter());

            return settings;
        }

        public static JsonSerializerSettings WithNonDefaultConstructorContractResolver(
            this JsonSerializerSettings settings)
        {
            settings.ContractResolver = new NonDefaultConstructorContractResolver();
            return settings;
        }
    }
}

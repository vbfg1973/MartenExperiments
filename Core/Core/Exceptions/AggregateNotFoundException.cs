namespace Core.Exceptions
{
    public class AggregateNotFoundException: Exception
    {
        private AggregateNotFoundException(string typeName, string id): base($"{typeName} with id '{id}' was not found")
        {
        }

        public static AggregateNotFoundException For<T>(Guid id)
        {
            return For<T>(id.ToString());
        }

        private static AggregateNotFoundException For<T>(string id)
        {
            return new AggregateNotFoundException(typeof(T).Name, id);
        }
    }
}

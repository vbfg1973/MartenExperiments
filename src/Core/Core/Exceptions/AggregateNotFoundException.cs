namespace Core.Exceptions
{
    public class AggregateNotFoundException: Exception
    {
        private AggregateNotFoundException(string typeName, Guid id): base($"{typeName} with id '{id}' was not found")
        {
        }

        private AggregateNotFoundException(string typeName, string referenceName, string referenceValue): base($"{typeName} with reference '{referenceValue}' was not found")
        {
        }

        public static AggregateNotFoundException For<T>(Guid id)
        {
            return new AggregateNotFoundException(typeof(T).Name, id);
        }

        public static AggregateNotFoundException ForReference<T>(string referenceName, string referenceValue)
        {
            return new AggregateNotFoundException(typeof(T).Name, referenceName, referenceValue);
        }
    }
}

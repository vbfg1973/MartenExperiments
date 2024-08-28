namespace Core.Events
{
    public record EventMetadata(
        Guid EventId,
        ulong StreamPosition,
        ulong LogPosition
    );
}

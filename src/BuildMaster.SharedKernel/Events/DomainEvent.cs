namespace BuildMaster.SharedKernel.Events;

public class DomainEvent : IDomainEvent
{
    public Guid EventId { get; } = Guid.NewGuid();

    public DateTime OcurredOnUtc { get; } = DateTime.UtcNow;
}

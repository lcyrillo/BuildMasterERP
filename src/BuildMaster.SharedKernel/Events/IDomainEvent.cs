namespace BuildMaster.SharedKernel.Events;

public interface IDomainEvent
{
    Guid EventId { get; }
    DateTime OcurredOnUtc { get; }
}

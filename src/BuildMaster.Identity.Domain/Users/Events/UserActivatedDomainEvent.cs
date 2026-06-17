using BuildMaster.SharedKernel.Events;

namespace BuildMaster.Identity.Domain.Users.Events;

public sealed class UserActivatedDomainEvent : DomainEvent
{
    public Guid UserId { get; set; }
    public UserActivatedDomainEvent(Guid userId)
    {
        UserId = userId;
    }
}

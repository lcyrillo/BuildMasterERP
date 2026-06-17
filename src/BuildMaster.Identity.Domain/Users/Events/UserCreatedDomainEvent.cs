using BuildMaster.SharedKernel.Events;

namespace BuildMaster.Identity.Domain.Users.Events;

public sealed class UserCreatedDomainEvent : DomainEvent
{
    public Guid UserId { get; set; }
    public string Email { get; set; }

    public UserCreatedDomainEvent(
        Guid userId,
        string email)
    {
        UserId = userId;
        Email = email;
    }
}

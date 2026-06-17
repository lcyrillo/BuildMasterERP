using BuildMaster.SharedKernel.Events;

namespace BuildMaster.Identity.Domain.Users.Events;

public class UserEmailChangedDomainEvent : DomainEvent
{
    public Guid UserId { get; }
    public string OldEmail { get; }
    public string NewEmail { get; set; }

    public UserEmailChangedDomainEvent(
        Guid userId,
        string oldEmail,
        string newEmail)
    {
        UserId = userId;
        OldEmail = oldEmail;
        NewEmail = newEmail;
    }
}

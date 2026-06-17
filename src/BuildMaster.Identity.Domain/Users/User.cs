using BuildMaster.Identity.Domain.Users.Events;
using BuildMaster.Identity.Domain.Users.ValueObjects;
using BuildMaster.SharedKernel.Entities;
using BuildMaster.SharedKernel.Results;

namespace BuildMaster.Identity.Domain.Users;

public sealed class User : AggregateRoot
{
    public PersonName Name { get; private set; }
    public Email Email { get; private set; }
    public PasswordHash PasswordHash { get; private set; }
    public bool Active { get; private set; }

    private User()
    {
        Name = null!;
        Email = null!;
        PasswordHash = null!;
    }

    private User(
        PersonName name,
        Email email,
        PasswordHash passwordHash)
    {
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        Active = true;

        RaiseDomainEvent(
            new UserCreatedDomainEvent(
                Id,
                Email.Value));
    }

    public Result Deactivate()
    {
        if (!Active)
            return Result.Failure(UserErrors.AlreadyInactive);

        Active = false;

        return Result.Success();
    }

    public Result Activate()
    {
        if (Active)
            return Result.Failure(UserErrors.AlreadyActive);

        Active = true;

        RaiseDomainEvent(
            new UserActivatedDomainEvent(Id));

        return Result.Success();
    }

    public static Result<User> Create(
        string name,
        string email,
        string passwordHash)
    {
        var nameResult = PersonName.Create(name);

        if (nameResult.IsFailure)
            return Result<User>.Failure(nameResult.Error);

        var emailResult = Email.Create(email);

        if (emailResult.IsFailure)
            return Result<User>.Failure(
                emailResult.Error);

        var passwordHashResult = PasswordHash.Create(passwordHash);

        if (passwordHashResult.IsFailure)
            return Result<User>.Failure(
                passwordHashResult.Error);

        var user = new User(
            nameResult.Value,
            emailResult.Value,
            passwordHashResult.Value);

        return Result<User>.Success(user);
    }

    public Result ChangeEmail(string newEmail)
    {
        var emailResult = Email.Create(newEmail);

        if (emailResult.IsFailure)
            return Result.Failure(
                emailResult.Error);

        if (Email.Value == emailResult.Value.Value)
            return Result.Success();

        var oldEmail = Email.Value;

        Email = emailResult.Value;

        RaiseDomainEvent(
            new UserEmailChangedDomainEvent(
                Id,
                oldEmail,
                Email.Value));

        return Result.Success();
    }
}

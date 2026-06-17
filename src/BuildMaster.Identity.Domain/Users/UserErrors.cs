using BuildMaster.SharedKernel.Errors;

namespace BuildMaster.Identity.Domain.Users;

public static class UserErrors
{
    public static readonly Error NameRequired =
        new(
            "Users.NameRequired",
            "User name is required.");

    public static readonly Error EmailRequired =
        new(
            "Users.EmailRequired",
            "User email is required.");

    public static readonly Error PasswordRequired =
        new(
            "User.PasswordRequired",
            "User password is required.");

    public static readonly Error AlreadyActive =
        new(
            "User.AlreadyActive",
            "User is already active.");

    public static readonly Error AlreadyInactive =
        new(
            "User.AlreadyInactive",
            "User is already inactive.");

    public static readonly Error InvalidEmail =
        new(
            "User.InvalidEmail",
            "Email format is invalid.");

    public static readonly Error InvalidName =
        new(
            "User.InvalidName",
            "User name is invalid.");

    public static readonly Error EmailAlreadyExists =
        new(
            "User.EmailAlreadyExists",
            "Email already exists");
}

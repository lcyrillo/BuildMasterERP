using BuildMaster.SharedKernel.Results;

namespace BuildMaster.Identity.Domain.Users.ValueObjects;

public sealed class PersonName
{
    public string Value { get; }

    private PersonName(string value) => Value = value;

    public static Result<PersonName> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
            return Result<PersonName>.Failure(
                UserErrors.NameRequired);

        value = value.Trim();

        if (value.Length < 3)
            return Result<PersonName>.Failure(
                UserErrors.InvalidName);

        if (value.Length > 200)
            return Result<PersonName>.Failure(
                UserErrors.InvalidName);

        return Result<PersonName>.Success(
            new PersonName(value));
    }

    public override string ToString()
    {
        return Value;
    }
}

using BuildMaster.SharedKernel.Results;

namespace BuildMaster.Identity.Domain.Users.ValueObjects;

public class PasswordHash
{
    public string Value { get; }

    private PasswordHash(string value) 
    { 
        Value = value;
    }

    public static Result<PasswordHash> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<PasswordHash>.Failure(
                UserErrors.PasswordRequired);

        return Result<PasswordHash>.Success(
            new PasswordHash(value));
    }

    public override string ToString()
    {
        return Value;
    }
}

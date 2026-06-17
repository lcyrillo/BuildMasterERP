using BuildMaster.SharedKernel.Results;
using System.Text.RegularExpressions;

namespace BuildMaster.Identity.Domain.Users.ValueObjects;

public sealed class Email
{
    private static readonly Regex EmailRegex =
        new(
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.Compiled);

    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static Result<Email> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<Email>.Failure(
                UserErrors.EmailRequired);

        if (!EmailRegex.IsMatch(value))
            return Result<Email>.Failure(
                UserErrors.InvalidEmail);

        return Result<Email>.Success(
            new Email(value.Trim().ToLowerInvariant()));
    }

    public override string ToString()
    {
        return Value;
    }
}

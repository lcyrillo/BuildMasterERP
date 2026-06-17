using BuildMaster.Identity.Application.Abstractions;

namespace BuildMaster.Identity.Infrastructure.Security;

public class BcryptPasswordHasher
    : IPasswordHasher
{
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}

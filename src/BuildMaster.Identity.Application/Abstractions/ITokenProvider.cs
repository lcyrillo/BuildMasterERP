using BuildMaster.Identity.Domain.Users;

namespace BuildMaster.Identity.Application.Abstractions;

public interface ITokenProvider
{
    string Generate(User user);
}

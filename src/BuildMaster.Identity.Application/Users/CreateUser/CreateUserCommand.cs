using BuildMaster.SharedKernel.Results;
using MediatR;

namespace BuildMaster.Identity.Application.Users.CreateUser;

public sealed record CreateUserCommand(
    string Name,
    string Email,
    string Password)
    : IRequest<Result<Guid>>;

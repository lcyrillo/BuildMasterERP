using BuildMaster.SharedKernel.Results;
using MediatR;

namespace BuildMaster.Identity.Application.Authentication.Login;

public sealed record LoginCommand(
    string Email,
    string Password)
    : IRequest<Result<string>>;

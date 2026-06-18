using BuildMaster.Identity.Application.Abstractions;
using BuildMaster.Identity.Domain.Users;
using BuildMaster.SharedKernel.Results;
using MediatR;

namespace BuildMaster.Identity.Application.Authentication.Login;

public sealed class LoginCommandHandler
    : IRequestHandler<LoginCommand, Result<string>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenProvider _tokenProvider;

    public LoginCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        ITokenProvider tokenProvider)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _tokenProvider = tokenProvider;
    }

    public async Task<Result<string>> Handle(
        LoginCommand request, 
        CancellationToken cancellationToken)
    {
        var user =
            await _userRepository.GetByEmailAsync(
                request.Email, 
                cancellationToken);

        if (user is null)
            return Result<string>.Failure(
                UserErrors.InvalidCredentials);

        var isPasswordValid =
            _passwordHasher.Verify(
                request.Password,
                user.PasswordHash.Value);

        if (!isPasswordValid)
            return Result<string>.Failure(
                UserErrors.InvalidCredentials);

        var token =
            _tokenProvider.Generate(user);

        return Result<string>.Success(token);
    }
}

using BuildMaster.Identity.Application.Abstractions;
using BuildMaster.Identity.Domain.Users;
using BuildMaster.SharedKernel.Results;
using BuildMaster.SharedKernel.Abstractions;
using MediatR;

namespace BuildMaster.Identity.Application.Users.CreateUser;

public sealed class CreateUserCommandHandler
    : IRequestHandler<CreateUserCommand, Result<Guid>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(
        CreateUserCommand command,
        CancellationToken cancellationToken = default)
    {
        var existingUser = 
            await _userRepository.GetByEmailAsync(
                command.Email, 
                cancellationToken);

        if (existingUser is not null)
            return Result<Guid>.Failure(
                UserErrors.EmailAlreadyExists);

        var passwordHash =
            _passwordHasher.Hash(
                command.Password);

        var userResult = User.Create(
            command.Name,
            command.Email,
            passwordHash);

        if (userResult.IsFailure)
            return Result<Guid>.Failure(
                userResult.Error);

        await _userRepository.AddAsync(
            userResult.Value,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return Result<Guid>.Success(
            userResult.Value.Id);
    }

}

using BuildMaster.Identity.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace BuildMaster.Identity.Infrastructure.Persistence.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly IdentityDbContext _context;

    public UserRepository(
        IdentityDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        User user, 
        CancellationToken cancellationToken = default)
    {
        await _context.Users.AddAsync(
            user, 
            cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(
        string email, 
        CancellationToken cancellationToken = default)
    {
        var normalizedEmail =
            email.Trim().ToLowerInvariant();

        return await _context.Users
            .FirstOrDefaultAsync(
                x => x.Email.Value == email.ToLower(),
                cancellationToken);
    }

    public async Task<User?> GetByIdAsync(
        Guid id, 
        CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .FirstOrDefaultAsync(
                x => x.Id ==  id,
                cancellationToken);
    }
}

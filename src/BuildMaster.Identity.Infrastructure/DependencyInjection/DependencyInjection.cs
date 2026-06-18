using BuildMaster.Identity.Application.Abstractions;
using BuildMaster.Identity.Domain.Users;
using BuildMaster.Identity.Infrastructure.Persistence;
using BuildMaster.Identity.Infrastructure.Persistence.Repositories;
using BuildMaster.Identity.Infrastructure.Security;
using BuildMaster.SharedKernel.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildMaster.Identity.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentityInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<IdentityDbContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString(
                    "IdentityDatabase"));
        });

        //JWT
        services.Configure<JwtOptions>(
            configuration.GetSection(
                JwtOptions.SectionName));

        services.AddScoped<ITokenProvider, JwtTokenProvider>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BuildMaster.Identity.Infrastructure.Persistence;

public sealed class IdentityDbContextFactory
    : IDesignTimeDbContextFactory<IdentityDbContext>
{
    public IdentityDbContext CreateDbContext(
        string[] args)
    {
        var optionsBuilder =
            new DbContextOptionsBuilder<IdentityDbContext>();

        optionsBuilder.UseSqlServer(
             "Server=localhost,1433;Database=BuildMaster.Identity;User Id=sa;Password=BuildMaster123!;TrustServerCertificate=True");

        return new IdentityDbContext(
            optionsBuilder.Options);
    }
}

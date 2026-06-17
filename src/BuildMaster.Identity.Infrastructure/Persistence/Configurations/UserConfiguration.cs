using BuildMaster.Identity.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuildMaster.Identity.Infrastructure.Persistence.Configurations;

public sealed class UserConfiguration
    : IEntityTypeConfiguration<User>
{
    public void Configure(
        EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);

        builder.OwnsOne(
            x => x.Name,
            name =>
            {
                name.Property(x => x.Value)
                    .HasColumnName("Name")
                    .HasMaxLength(200)
                    .IsRequired();
            });

        builder.OwnsOne(
            x => x.Email,
            email =>
            {
                email.Property(x => x.Value)
                     .HasColumnName("Email")
                     .HasMaxLength(320)
                     .IsRequired();

                email.HasIndex(x => x.Value)
                     .IsUnique();
            });

        builder.OwnsOne(
            x => x.PasswordHash,
            password =>
            {
                password.Property(x => x.Value)
                        .HasColumnName("PasswordHash")
                        .IsRequired();
            });

        builder.Property(x => x.Active)
               .IsRequired();

        //builder.HasIndex("Email")
        //       .IsUnique();

    }
}

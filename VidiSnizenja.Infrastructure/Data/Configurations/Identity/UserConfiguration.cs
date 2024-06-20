using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VidiSnizenja.Domain.Entities;

namespace VidiSnizenja.Infrastructure.Data.Configurations.Identity;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(user => user.Email).IsRequired();

        builder.Property(pi => pi.FirstName).IsRequired();
        builder.Property(pi => pi.LastName).IsRequired();
        builder.Property(pi => pi.Birthdate).IsRequired();

        builder.Property(user => user.Email).IsRequired();

        builder.HasMany(user => user.Claims)
            .WithOne(claim => claim.User)
            .HasForeignKey(userClaim => userClaim.UserId)
            .IsRequired();

        builder.HasMany(user => user.Logins)
            .WithOne(login => login.User)
            .HasForeignKey(userLogin => userLogin.UserId)
            .IsRequired();

        builder.HasMany(user => user.Roles)
            .WithOne(role => role.User)
            .HasForeignKey(userRole => userRole.UserId)
            .IsRequired();

        builder.HasMany(user => user.Tokens)
            .WithOne(token => token.User)
            .HasForeignKey(userToken => userToken.UserId)
            .IsRequired();

        builder.HasMany(user => user.RefreshTokens)
            .WithOne(token => token.User)
            .HasForeignKey(userToken => userToken.UserId)
            .IsRequired();

        builder.HasQueryFilter(u => !u.IsDeleted);
    }
}

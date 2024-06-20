using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VidiSnizenja.Domain.Entities;

namespace VidiSnizenja.Infrastructure.Data.Configurations.Identity;

public sealed class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.ToTable("UserTokens");

        builder.HasOne(userToken => userToken.User)
            .WithMany(User => User.Tokens)
            .HasForeignKey(userToken => userToken.UserId)
            .IsRequired();
    }
}
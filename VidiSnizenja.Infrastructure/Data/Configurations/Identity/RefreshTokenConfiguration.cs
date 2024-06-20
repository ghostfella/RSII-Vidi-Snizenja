using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VidiSnizenja.Domain.Entities;

namespace VidiSnizenja.Infrastructure.Data.Configurations.Identity;

public sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");
        builder.HasKey(token => token.Id);

        builder.Property(token => token.CreatedOnUtc).IsRequired();
        builder.Property(token => token.Expires).IsRequired();
        builder.Property(token => token.IsValid).IsRequired().HasDefaultValue(false);

        builder.HasOne(token => token.User)
            .WithMany(user => user.RefreshTokens)
            .HasForeignKey(token => token.UserId)
            .IsRequired();
    }
}
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VidiSnizenja.Domain.Entities;

namespace VidiSnizenja.Infrastructure.Data.Configurations.Identity;

public sealed class UserClaimConfiguration : IEntityTypeConfiguration<UserClaim>
{
    public void Configure(EntityTypeBuilder<UserClaim> builder)
    {
        builder.ToTable("UserClaims");

        builder.HasKey(userClaim => userClaim.Id);

        builder.HasOne(userClaim => userClaim.User)
            .WithMany(user => user.Claims)
            .HasForeignKey(userClaim => userClaim.UserId)
            .IsRequired();
    }
}
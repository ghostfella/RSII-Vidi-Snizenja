using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VidiSnizenja.Domain.Entities;

namespace VidiSnizenja.Infrastructure.Data.Configurations.Identity;

public sealed class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaim>
{
    public void Configure(EntityTypeBuilder<RoleClaim> builder)
    {
        builder.ToTable("RoleClaims");
        builder.HasOne(roleClaim => roleClaim.Role)
            .WithMany(role => role.Claims)
            .HasForeignKey(roleClaim => roleClaim.RoleId)
            .IsRequired();
    }
}

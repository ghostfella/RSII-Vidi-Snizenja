using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VidiSnizenja.Domain.Entities;

namespace VidiSnizenja.Infrastructure.Data.Configurations.Identity
{
    public class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.ToTable("UserLogins");

            builder.HasOne(userLogin => userLogin.User)
                .WithMany(user => user.Logins)
                .HasForeignKey(userLogin => userLogin.UserId)
                .IsRequired();
        }
    }
}
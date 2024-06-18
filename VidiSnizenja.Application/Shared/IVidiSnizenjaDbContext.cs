using Microsoft.EntityFrameworkCore;

namespace VidiSnizenja.Application.Shared;

public interface IVidiSnizenjaDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserClaim> UserClaims { get; set; }
    public DbSet<UserLogin> UserLogins { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<UserToken> UserTokens { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<RoleClaim> RoleClaims { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Article> Articles { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

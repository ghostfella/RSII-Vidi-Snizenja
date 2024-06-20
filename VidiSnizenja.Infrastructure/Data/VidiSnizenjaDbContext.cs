using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VidiSnizenja.Application.Shared;
using VidiSnizenja.Domain.Entities;

namespace VidiSnizenja.Infrastructure.Data;

public class VidiSnizenjaDbContext : IdentityDbContext<User, Role, string, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>, IDbContext
{
    public VidiSnizenjaDbContext(DbContextOptions<VidiSnizenjaDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<UserClaim> UserClaims { get; set; }
    public DbSet<UserLogin> UserLogins { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<UserToken> UserTokens { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Article> Articles { get; set; }
    public DbSet<BrandArticle> BrandArticles { get; set; }
    public DbSet<ArticleType> ArticleTypes { get; set; }
    public DbSet<Brand> Brands { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
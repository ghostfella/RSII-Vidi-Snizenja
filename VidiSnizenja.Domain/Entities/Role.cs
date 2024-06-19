namespace VidiSnizenja.Domain.Entities;

public class Role : IdentityRole
{
    public ICollection<UserRole> Roles { get; set; }
    public ICollection<RoleClaim> Claims { get; set; }
}
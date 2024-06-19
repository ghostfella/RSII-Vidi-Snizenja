namespace VidiSnizenja.Domain.Entities;

public class UserClaim : IdentityUserClaim<string>
{
    public User User { get; set; }
}
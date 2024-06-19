namespace VidiSnizenja.Domain.Entities;

public class UserToken : IdentityUserToken<string>
{
    public User User { get; set; }
}

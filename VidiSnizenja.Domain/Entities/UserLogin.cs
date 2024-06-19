namespace VidiSnizenja.Domain.Entities;

public class UserLogin : IdentityUserLogin<string>
{
    public User User { get; set; }
}
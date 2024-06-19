using VidiSnizenja.Domain.Common;

namespace VidiSnizenja.Domain.Entities;

public sealed class RefreshToken : Entity<Guid>
{
    internal RefreshToken(Guid id, string userId, DateTime expires) : base(id)
    {
        UserId = userId;
        Expires = expires;
        CreatedOnUtc = DateTime.UtcNow;
        IsValid = true;
    }

    #region Properties

    public DateTime CreatedOnUtc { get; private set; }
    public DateTime Expires { get; private set; }
    public bool IsValid { get; private set; }

    #endregion Properties

    #region Navigation

    public User User { get; private set; }
    public string UserId { get; private set; }

    #endregion Navigation

    #region Methods

    public void MarkAsInvalid()
    {
        IsValid = false;
    }

    #endregion Methods
}

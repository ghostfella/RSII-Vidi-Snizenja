using VidiSnizenja.Domain.Common;

namespace VidiSnizenja.Domain.Entities
{
    public sealed class User : IdentityUser, ISoftDelete, IAuditableEntity
    {
        public User() { }

        private User(string firstName, string lastName, DateTime birthdate, string username, string email) : base(username)
        {
            FirstName = firstName;
            LastName = lastName;
            Birthdate = birthdate;
            Email = email;
            IsDeleted = false;
        }

        #region Properties

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }

        public IReadOnlyCollection<UserRole> Roles => _roles.ToList();

        #endregion Properties     

        #region Fields

        private readonly List<RefreshToken> _refreshTokens = new List<RefreshToken>();
        private readonly List<UserClaim> _claims = new List<UserClaim>();
        private readonly List<UserRole> _roles = new List<UserRole>();
        private readonly List<UserLogin> _logins = new List<UserLogin>();
        private readonly List<UserToken> _tokens = new List<UserToken>();

        #endregion Fields

        #region Navigation

        public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens;
        public IReadOnlyCollection<UserClaim> Claims => _claims;
        public ICollection<UserLogin> Logins => _logins;
        public ICollection<UserToken> Tokens => _tokens;
        public ICollection<Article> Articles { get; set; }

        #endregion Navigation

        #region Audit

        public string CreatedBy { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOnUtc { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedOnUtc { get; set; }
        public bool IsDeleted { get; set; }

        #endregion Audit

    }
}
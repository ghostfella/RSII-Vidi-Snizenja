using VidiSnizenja.Domain.Common;
using VidiSnizenja.Domain.Enums;

namespace VidiSnizenja.Domain.Entities;

public sealed class Article(Guid Id) : Entity<Guid>(Id), IAuditableEntity
{
    public string Name { get; set; } = null!;
    public double Price { get; set; } = 0;
    public string? Description { get; set; }
    public bool Discount { get; set; }
    public Availability AvailabilityStatus { get; set; }

    public string CreatedBy { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedOnUtc { get; set; }
    public string? DeletedBy { get; set; }
    public DateTime? DeletedOnUtc { get; set; }

    public Guid ArticleTypeId { get; set; }
    public ArticleType ArticleType { get; set; }

    public string UserId { get; set; }
    public User User { get; set; }

    public ICollection<BrandArticle> Articles { get; set; }
    public ICollection<Reservation> Reservations { get; set; }
}

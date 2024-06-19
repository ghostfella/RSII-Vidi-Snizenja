using VidiSnizenja.Domain.Common;

namespace VidiSnizenja.Domain.Entities;

public sealed class BrandArticle(Guid Id) : Entity<Guid>(Id), IAuditableEntity
{
    public Guid ArticleId { get; set; }
    public Article Article { get; set; }

    public Guid BrandId { get; set; }
    public Brand Brand { get; set; }

    public string CreatedBy { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedOnUtc { get; set; }
    public string? DeletedBy { get; set; }
    public DateTime? DeletedOnUtc { get; set; }
}
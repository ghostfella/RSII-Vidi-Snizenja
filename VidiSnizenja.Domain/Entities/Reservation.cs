using VidiSnizenja.Domain.Common;
using VidiSnizenja.Domain.Enums;

namespace VidiSnizenja.Domain.Entities;

public sealed class Reservation(Guid Id) : Entity<Guid>(Id), IAuditableEntity
{
    public DateTime Date { get; set; }
    public ReservationStatus Status { get; set; }

    public Article Articles { get; set; }
    public Guid ArticleId { get; set; }

    public string CreatedBy { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedOnUtc { get; set; }
    public string? DeletedBy { get; set; }
    public DateTime? DeletedOnUtc { get; set; }
}
namespace VidiSnizenja.Domain.Common;

public interface IAuditableEntity
{
    string CreatedBy { get; set; }
    DateTime CreatedOnUtc { get; set; }
    string? LastModifiedBy { get; set; }
    DateTime? LastModifiedOnUtc { get; set; }
    string? DeletedBy { get; set; }
    DateTime? DeletedOnUtc { get; set; }
}
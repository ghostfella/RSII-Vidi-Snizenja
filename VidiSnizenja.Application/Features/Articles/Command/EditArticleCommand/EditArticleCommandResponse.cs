namespace VidiSnizenja.Application.Features.Articles.Command.EditArticleCommand;

public sealed record EditArticleCommandResponse(string Name, ArticleType ArticleTypeId, double Price, string? Description, bool Discount, AvailabilityType Availability);
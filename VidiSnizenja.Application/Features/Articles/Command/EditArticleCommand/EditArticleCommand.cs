using MediatR;
using VidiSnizenja.Shared.Result;

namespace VidiSnizenja.Application.Features.Articles.Command.EditArticleCommand;

public sealed record EditArticleCommand(Guid Id, string Name, ArticleType ArticleTypeId, double Price, string? Description, bool Discount, AvailabilityType Availability) : IRequest<Result<EditArticleCommandResponse>>;
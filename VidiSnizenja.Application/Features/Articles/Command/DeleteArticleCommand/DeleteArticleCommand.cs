using MediatR;
using VidiSnizenja.Shared.Result;

namespace VidiSnizenja.Application.Features.Articles.Command.DeleteArticleCommand;

public sealed record DeleteArticleCommand(Guid Id) : IRequest<Result<DeleteArticleCommandResponse>>;
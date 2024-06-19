using MediatR;
using VidiSnizenja.Application.Shared;
using VidiSnizenja.Shared.Result;

namespace VidiSnizenja.Application.Features.Articles.Command.EditArticleCommand;

public sealed class EditArticleCommandHandler : IRequestHandler<EditArticleCommand, Result<EditArticleCommandResponse>>
{
    private readonly IDbContext _dbContext;
    private readonly ICurrentUser _currentUser;
    public EditArticleCommandHandler(IDbContext dbContext, ICurrentUser currentUser)
    {
        ArgumentNullException.ThrowIfNull(dbContext);
        ArgumentNullException.ThrowIfNull(currentUser);

        _dbContext = dbContext;
        _currentUser = currentUser;
    }

    public async Task<Result<EditArticleCommandResponse>> Handle(EditArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await _dbContext.Articles
            .Where(p => p.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (article is null || article.UserId != _currentUser.Id)
        {
            return Result.Fail<EditArticleCommandResponse>("Article not found!");
        }

        article.Name = request.Name;
        article.ArticleTypeId = request.ArticleTypeId;
        article.Price = request.Price;
        article.Description = request.Description;
        article.Discount = request.Discount;
        article.Availability = request.Availability;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new Result<EditArticleCommandResponse>(new EditArticleCommandResponse(article.Name, article.ArticleTypeId, article.Price, article.Description, article.Discount, Domain.Enums.ArticleTypeId);
    }
}
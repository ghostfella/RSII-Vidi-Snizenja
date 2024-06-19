using MediatR;
using VidiSnizenja.Application.Shared;
using VidiSnizenja.Shared.Result;

namespace VidiSnizenja.Application.Features.Articles.Command.CreateArticleCommand;

public sealed record CreatePropertyCommandHandler : IRequestHandler<CreateArticleCommand, Result<CreateArticleCommandResponse>>
{
    private readonly IDbContext _dbContext;
    private readonly ICurrentUser _currentUser;

    public CreatePropertyCommandHandler(IDbContext dbContext, ICurrentUser currentUser)
    {
        ArgumentNullException.ThrowIfNull(dbContext);
        ArgumentNullException.ThrowIfNull(currentUser);

        _dbContext = dbContext;
        _currentUser = currentUser;
    }

    public async Task<Result<CreateArticleCommandResponse>> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        var article = new Domain.Entities.Article(Guid.NewGuid())
        {
            Name = request.Name,
            ArticleTypeId = request.ArticleTypeId,
            Price = request.Price,
            Description = request.Description,
            Discount = request.Discount,
            Availability = request.Availability
        };

        await _dbContext.Articles.AddAsync(article, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new Result<CreateArticleCommandResponse>(new CreateArticleCommandResponse(article.Id));
    }
}
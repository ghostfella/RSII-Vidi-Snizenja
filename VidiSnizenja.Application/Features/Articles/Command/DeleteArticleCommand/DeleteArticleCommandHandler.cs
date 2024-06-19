using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VidiSnizenja.Application.Shared;
using VidiSnizenja.Shared.Result;

namespace VidiSnizenja.Application.Features.Articles.Command.DeleteArticleCommand;

public sealed class DeletePropertyCommandHandler : IRequestHandler<DeleteArticleCommand, Result<DeleteArticleCommandResponse>>
{
    private readonly IDbContext _dbContext;
    private readonly ICurrentUser _currentUser;

    public DeletePropertyCommandHandler(IDbContext dbContext, ICurrentUser currentUser)
    {
        ArgumentNullException.ThrowIfNull(dbContext);
        ArgumentNullException.ThrowIfNull(currentUser);

        _dbContext = dbContext;
        _currentUser = currentUser;
    }

    public async Task<Result<DeleteArticleCommandResponse>> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await _dbContext
            .Articles
            .Include(p => p.Reservations)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (article is null || article.UserId != _currentUser.Id || _currentUser.IsInRole(AppRoles.USER))
        {
            return Result.Fail<DeleteArticleCommandResponse>("Article not found");
        }

        if (article.Reservations.Any(r => r.Status == Domain.Enums.ReservationStatus.Pending))
        {
            return Result.Fail<DeleteArticleCommandResponse>(" has active reservations!");
        }

        if (article.Status != Domain.Enums.PropertyStatus.Available && property.Status != Domain.Enums.PropertyStatus.Vacant && property.Status != Domain.Enums.PropertyStatus.OffMarket)
        {
            return Result.Fail<DeleteArticleCommandResponse>("Property is still active!");
        }

        _dbContext.Articles.Remove(article);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Ok(new DeleteArticlesCommandResponse(property.Id));
    }
}

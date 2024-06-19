using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VidiSnizenja.Application.Shared;
using VidiSnizenja.Shared.Result;

namespace VidiSnizenja.Application.Features.Articles.Query.GetAllArticles
{
    public sealed record GetAllArticlesQueryHandler : IRequestHandler<GetAllArticlesQuery, Result<IEnumerable<GetAllArticlesQueryResponse>>>
    {
        private readonly IDbContext _dbContext;

        public GetAllArticlesQueryHandler(IDbContext dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext);

            _dbContext = dbContext;
        }

        public async Task<Result<IEnumerable<GetAllArticlesQueryResponse>>> Handle(GetAllArticlesQuery request, CancellationToken cancellationToken)
        {
            var articles = await _dbContext
                .Articles
                .AsNoTracking()
                .Include(a => a.User)
                .Include(a => a.ArticleType)
                .Where(a => a.DeletedOnUtc != null)
                .Select(a => new GetAllArticlesQueryResponse(
                    a.Name,
                    a.MinPrice,
                    a.MaxPrice,
                    a.ArticleTypeId))
                .ToListAsync(cancellationToken);

            return new Result<IEnumerable<GetAllArticlesQueryResponse>>(articles);
        }
    }
}

using System.Linq.Expressions;

namespace VidiSnizenja.Application.Extensions;

internal static class Extensions
{
    internal static IQueryable<TEntity> WhereIf<TEntity>(this IQueryable<TEntity> query, bool condition, Expression<Func<TEntity, bool>> func)
    {
        if (condition)
        {
            query = query.Where(func);
        }

        return query;
    }
}

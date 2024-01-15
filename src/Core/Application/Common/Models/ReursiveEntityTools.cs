using System.Linq.Expressions;

namespace FSH.WebApi.Application.Common.Models;
public static class ReursiveEntityTools<TEntity, TDto>
    where TEntity : AuditableEntity
    where TDto : IDto , new()
{
    public static Expression<Func<TEntity,TDto>> GetAncestors( TEntity entity,TDto dto , int maxDepth, int currentDepth = 0)
    {
        ++currentDepth;
        Expression<Func<TEntity, TDto>> result = e => new TDto()
        {
        };

        return result;

    }
}

using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FSH.WebApi.Infrastructure.Common.Extensions;
public static class GetIQueryableEntityByNameExtensions
{
    public static IQueryable Query(this DbContext context, string entityName) =>
        context.Query(context.Model.FindEntityType(entityName).ClrType);

    static readonly MethodInfo SetMethod =
        typeof(DbContext).GetMethod(nameof(DbContext.Set), 1, Array.Empty<Type>()) ??
        throw new Exception($"Type not found: DbContext.Set");

    public static IQueryable Query(this DbContext context, Type entityType) =>
        (IQueryable)SetMethod.MakeGenericMethod(entityType)?.Invoke(context, null) ??
        throw new Exception($"Type not found: {entityType.FullName}");

}

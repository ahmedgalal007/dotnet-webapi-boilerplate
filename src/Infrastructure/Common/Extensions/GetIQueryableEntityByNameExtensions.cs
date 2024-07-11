using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Infrastructure.Common.Extensions;
public static class GetIQueryableEntityByNameExtensions
{
    public static IQueryable Query(this DbContext context, string entityName) =>
          context.Query(entityName, context.Model.FindEntityType(entityName).ClrType);

    static readonly MethodInfo SetMethod =
        typeof(DbContext).GetMethod(nameof(DbContext.Set), 1, new[] { typeof(string) }) ??
        throw new Exception($"Type not found: DbContext.Set");

    public static IQueryable Query(this DbContext context, string entityName, Type entityType) =>
        (IQueryable)SetMethod.MakeGenericMethod(entityType)?.Invoke(context, new[] { entityName }) ??
        throw new Exception($"Type not found: {entityType.FullName}");
}

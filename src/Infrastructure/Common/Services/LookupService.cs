using Fluid.Parser;
using FSH.WebApi.Application.Common.Interfaces;
using FSH.WebApi.Domain.Article;
using FSH.WebApi.Domain.Keywords;
using FSH.WebApi.Infrastructure.Common.Extensions;
using FSH.WebApi.Infrastructure.Identity;
using FSH.WebApi.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Infrastructure.Common.Services;
public class LookupService : ILookupService
{
    private ApplicationDbContext _context { get; }
    public LookupService(ApplicationDbContext context)
    {
        _context = context;
    }

    public static List<Lookup> Lookups = new()
    {
        new() { EName = "Category", EType = typeof(Category), TitleFieldName = "Name" },
        new() { EName = "Keywords", EType = typeof(Keyword)},
        new() { EName = "UserName", EType = typeof(ApplicationUser), TitleFieldName = "UserName"},
        new() { EName = "UserEmail", EType = typeof(ApplicationUser), TitleFieldName = "Email"},
        new() { EName = "Role", EType = typeof(ApplicationRole)},
        new() { EName = "RoleClaims", EType = typeof(ApplicationRoleClaim)}
    };

    public async Task<List<KeyValuePair<TID, string>>> Search<TID>(string entityName, string query, bool queryGetAll = false, TID? parentId = default)
    {
        List<KeyValuePair<TID, string>> result = new List<KeyValuePair<TID, string>>();

        if (!Lookups.Any(e => e.EName == entityName)) return result;
        Lookup lookup = Lookups.Find(e => e.EName == entityName)!;

        var qry = _context.Query(lookup.EName, lookup.EType);
        // var qry = _context.Query(lookup.EName);

        if (string.IsNullOrEmpty(query) && !queryGetAll)
        {
            return result;
        }
        else
        {
            qry = qry.Where($"{lookup.TitleFieldName} like '%{query}%' ");
        }

        return await ParseResult<TID>(qry, lookup);
    }

    private static async Task<List<KeyValuePair<TID, string>>> ParseResult<TID>(IQueryable qry, Lookup lookup)
    {
        List<KeyValuePair<TID, string>> result = new List<KeyValuePair<TID, string>>();
        foreach (dynamic row in await qry.ToDynamicListAsync())
        {
            var key = row[lookup.IDFieldName];
            var value = row[lookup.TitleFieldName];
            if (key != null && value != null) result.Add(new KeyValuePair<TID, string>(key, value));
        }

        return result;
    }
}

public class Lookup
{
    internal string EName { get; set; } = string.Empty;
    internal Type EType { get; set; } = typeof(object);
    internal string IDFieldName { get; set; } = "Id";
    internal string TitleFieldName { get; set; } = "Title";
}

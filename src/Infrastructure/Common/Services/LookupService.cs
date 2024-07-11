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

    public static List<Lookup> Lookups = new List<Lookup> {
        new Lookup{ EName = "Category", EType= typeof(Category), TitleFieldName = "Name" },
        new Lookup{ EName = "Keyword", EType= typeof(Keyword)},
        new Lookup{ EName = "UserName", EType= typeof(ApplicationUser), TitleFieldName="UserName"},
        new Lookup{ EName = "UserEmail", EType= typeof(ApplicationUser), TitleFieldName="Email"},
        new Lookup{ EName = "Role", EType= typeof(ApplicationRole)},
        new Lookup{ EName = "RoleClaims", EType= typeof(ApplicationRoleClaim)}
    };

    public List<KeyValuePair<TID, string>> Search<TID>(string entityName, string query, bool queryGetAll=false, TID? parentId = default)
    {
        List<KeyValuePair<TID, string>> result = new List<KeyValuePair<TID, string>>();

        if (!Lookups.Any(e => e.EName == entityName)) return result;
        Lookup lookup = Lookups.FirstOrDefault(e => e.EName == entityName)!;

        var _qry = _context.Query(lookup.EName, lookup.EType);

        if (string.IsNullOrEmpty(query) && ! queryGetAll)
        {
            return result;
        }
        else
        {
            _qry = _qry.Where($"{lookup.TitleFieldName} like %{query}% ");
        }
        result = ParseResult<TID>(_qry, lookup);
        return result;
    }

    private List<KeyValuePair<TID, string>> ParseResult<TID>(IQueryable qry, Lookup lookup)
    {
        List<KeyValuePair<TID, string>> result = new List<KeyValuePair<TID, string>>();
        foreach (dynamic row in qry.ToDynamicList()) {
            var _key = row[lookup.IDFieldName];
            var _value = row[lookup.TitleFieldName];
            if (_key != null && _value != null) result.Add(new KeyValuePair<TID, string>(_key, _value));
        }
        return result;
    }


}

public class Lookup
{
    internal string EName { get; set; } = string.Empty;
    internal Type EType { get; set; }
    internal string IDFieldName { get; set; } = "Id";
    internal string TitleFieldName { get; set; } = "Title";
}

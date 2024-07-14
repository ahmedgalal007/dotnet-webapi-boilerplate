using FSH.WebApi.Application.Common.Interfaces;
using FSH.WebApi.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Infrastructure.Common.Services;
public class LookupGenericService : ILookupService
{
    private ApplicationDbContext _context { get; }
    public LookupGenericService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<KeyValuePair<Guid, string>>> Search(string entityName, string query, bool queryGetAll = false, string lang = "ar-EG", Guid? parentId = default)
    {
        List<KeyValuePair<Guid, string>> result = new();
        System.Linq.Expressions.Expression<Func<Domain.Article.Category, KeyValuePair<DefaultIdType, string>>> categorySelectClause = e =>
                                        new KeyValuePair<Guid, string>(
                                            e.Id,
                                            e.Locals.FirstOrDefault(e => e.CulturCode == lang).Name
                                        );
        System.Linq.Expressions.Expression<Func<Domain.Keywords.Keyword, KeyValuePair<DefaultIdType, string>>> keywordSelectClause = e =>
                                        new KeyValuePair<Guid, string>(
                                            e.Id,
                                            e.Locals.FirstOrDefault(e => e.CulturCode == lang).Title
                                        );
        return entityName switch
        {
            "Category" => await _context.Categories
                                .Include(e => e.Locals)
                                .Where(c => c.Locals
                                    .Any(lc => lc.Name
                                        .Contains(query) && lc.CulturCode == lang
                                    )
                                )
                                .Select(categorySelectClause).ToListAsync(),
            "Keyword" => _context.Keywords
                                .Include(e => e.Locals)
                                .Where(c => c.Locals
                                    .Count(lc => lc.Title
                                        .Contains(query)
                                    ) > 0
                                )
                                .Select(keywordSelectClause).ToList(),
            _ => await Task.FromResult(result),
        };
    }

}
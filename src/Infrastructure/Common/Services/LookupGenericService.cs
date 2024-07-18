using Finbuckle.MultiTenant;
using FSH.WebApi.Application.Common.Interfaces;
using FSH.WebApi.Application.Common.Persistence;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace FSH.WebApi.Infrastructure.Common.Services;
public class LookupGenericService : ILookupService
{
    private readonly IDapperRepository _repository;
    private readonly ITenantInfo _tenantInfo;
    public LookupGenericService(IDapperRepository repository, ITenantInfo tenantInfo)
        => (_repository, _tenantInfo) = (repository, tenantInfo);

    public async Task<List<LookupResult>> Search(string entityName, string query, bool queryGetAll = false, string lang = "ar-EG", Guid? parentId = default)
    {
        string categorySQL = GenerateSQL(schema: "Article", entityName: "Category", childEntityName: "LocalizedCategory", localizedFieldId: "CategoryId", IsLocalized: true, TitleFieldName: "Name", CulturCode: lang);
        string keywordSQL = GenerateSQL(schema: "Article", entityName: "Keyword", childEntityName: "LocalizedKeyword", localizedFieldId: "KeywordId",  IsLocalized: true, TitleFieldName: "Title", CulturCode: lang);

        var sqlQueryParams = new
        {
            Query = query,
            CultureCode = lang,

            // tenant = _tenantInfo?.Identifier ?? MultitenancyConstants.Root.Id,
            tenant = _tenantInfo?.Identifier ?? "root",
        };

        async Task<List<LookupResult>> Apply(string sql, object queryParams) => (await _repository.QueryAsync<LookupResult>(
                        sql: sql, queryParams, cancellationToken: new CancellationToken())).ToList();

        return entityName switch
        {
            "Category" => await Apply(categorySQL, sqlQueryParams),
            "Keyword" => await Apply(keywordSQL, sqlQueryParams),
            _ => new List<LookupResult>()
        };
    }

    private string GenerateSQL(
                               string entityName,
                               string childEntityName,
                               string localizedFieldId,
                               string schema = "",
                               bool IsLocalized = false,
                               string? ParentFieldName = null,
                               string LocalsFieldName = "Locals",
                               string TitleFieldName = "Title",
                               string IdFieldName = "Id",
                               string CulturCode = "ar-EG")
    {
        string
            refP = "C.",
            refC = IsLocalized ? "L." : refP,
            jointClause = IsLocalized ? " LEFT JOIN " + (string.IsNullOrWhiteSpace(schema) ? childEntityName : schema + "." + childEntityName) + " L  ON " + refP + IdFieldName + " = " + refC + localizedFieldId : string.Empty,
            localizeFilter = IsLocalized ? " AND " + refC + "CulturCode = @CultureCode" : string.Empty,
            selectClause = $" SELECT {refP + IdFieldName} as Id , {refC + TitleFieldName} as Title";

        return selectClause +
                $" FROM {(!string.IsNullOrWhiteSpace(schema) ? schema + "." + entityName : entityName)} C" +
                $"{jointClause}" +
                $" WHERE {refC + TitleFieldName} Like N'%' + @Query + '%'" +
                $"{localizeFilter}" +
                $"{(string.IsNullOrWhiteSpace(ParentFieldName) ? string.Empty : " AND C." + ParentFieldName + " = @ParentId ")}" +
                $" AND C.\"TenantId\" = @tenant";
    }
}

public class LookupFilter
{
    public string FieldName { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public EnumFilterComparer Comparer { get; set; } = EnumFilterComparer.Equals;
}

public class EnumFilterComparer
{
    private EnumFilterComparer(string value) { Value = value; }

    public string Value { get; private set; }

    public static EnumFilterComparer Equals { get { return new EnumFilterComparer("= {0}"); } }
    public static EnumFilterComparer GreaterThan { get { return new EnumFilterComparer("> {0}"); } }
    public static EnumFilterComparer GreaterThanOrEquales { get { return new EnumFilterComparer(">= {0}"); } }
    public static EnumFilterComparer LessThan { get { return new EnumFilterComparer("< {0}"); } }
    public static EnumFilterComparer LessThanOrEquales { get { return new EnumFilterComparer("<= {0}"); } }
    public static EnumFilterComparer Contains { get { return new EnumFilterComparer("Like N'%' + {0} + '%'"); } }
    public static EnumFilterComparer StartsWith { get { return new EnumFilterComparer("Like N'{0}' + '%'"); } }
    public static EnumFilterComparer EndsWith { get { return new EnumFilterComparer("Like N'%' + {0}"); } }
    public static EnumFilterComparer NotEquals { get { return new EnumFilterComparer("!=  + {0}"); } }
    public static EnumFilterComparer IsNull { get { return new EnumFilterComparer("IS NULL"); } }

    public override string ToString()
    {
        return Value.Replace("N'", string.Empty).Replace("'", string.Empty).Replace("", string.Empty).Replace("", string.Empty).Replace("", string.Empty);
    }

    public string GenerateFilter(string FieldName, string filterValue)
    {
        return FieldName + " " + string.Format(Value, filterValue);
    }
}

#region Trash

// // List<KeyValuePair<Guid, string>> result = new();
// // string strSQL = $"SELECT * FROM Article.\"Category\" WHERE Slug Like N'%' + @Query + '%' AND \"TenantId\" = @tenant ";
// string strSQL = $" SELECT C.Id as Id , L.Name as Title " +
//                 $" FROM Article.\"Category\" C " +
//                 $" LEFT JOIN Article.\"LocalizedCategory\" L  ON C.Id = L.CategoryId" +
//                 $" WHERE L.\"Name\" Like N'%' + @Query + '%' " +
//                 $" AND L.CulturCode = @CultureCode " +
//                 $" AND C.\"TenantId\" = @tenant ";
// System.Linq.Expressions.Expression<Func<Domain.Article.Category, KeyValuePair<DefaultIdType, string>>> categorySelectClause = e =>
//                                 new KeyValuePair<Guid, string>(
//                                     e.Id,
//                                     e.Locals.FirstOrDefault(e => e.CulturCode == lang).Name
//                                 );
// System.Linq.Expressions.Expression<Func<Domain.Keywords.Keyword, KeyValuePair<DefaultIdType, string>>> keywordSelectClause = e =>
//                                 new KeyValuePair<Guid, string>(
//                                     e.Id,
//                                     e.Locals.FirstOrDefault(e => e.CulturCode == lang).Title
//                                 );
//
// "Category" => await _context.Categories
//                     .Include(e => e.Locals)
//                     .Where(c => c.Locals
//                         .Any(lc => lc.Name
//                             .Contains(query) && lc.CulturCode == lang
//                         )
//                     )
//                     .Select(categorySelectClause).ToListAsync(),
// "Keyword" => _context.Keywords
//                     .Include(e => e.Locals)
//                     .Where(c => c.Locals
//                         .Count(lc => lc.Title
//                             .Contains(query)
//                         ) > 0
//                     )
//                     .Select(keywordSelectClause).ToList(),
//  _ => await Task.FromResult(result),

#endregion
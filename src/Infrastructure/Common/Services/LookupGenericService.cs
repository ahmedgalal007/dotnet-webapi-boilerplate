using DocumentFormat.OpenXml.InkML;
using Elsa.Expressions.Models;
using Elsa.Workflows.Models;
using Finbuckle.MultiTenant;
using FSH.WebApi.Application.Common.Interfaces;
using FSH.WebApi.Application.Common.Persistence;
using FSH.WebApi.Domain.Article;
using FSH.WebApi.Domain.Common.Contracts;
using FSH.WebApi.Infrastructure.Persistence.Context;
using FSH.WebApi.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FSH.WebApi.Infrastructure.Common.Services;
public class LookupGenericService : ILookupService
{
    private readonly IDapperRepository _repository;
    private readonly ITenantInfo _tenantInfo;
    public LookupGenericService(IDapperRepository repository, ITenantInfo tenantInfo)
        => (_repository, _tenantInfo) = (repository, tenantInfo);

    public async Task<List<LookupResult>> Search(string entityName, string query, bool queryGetAll = false, string lang = "ar-EG", Guid? parentId = default)
    {
        string categorySQL = GenerateSQL(entityName: "Category", childEntityName:"LocalizedCategory", localizedFieldId: "CategoryId", schema: "Article", IsLocalized: true, TitleFieldName: "Name", CulturCode: lang);
        string keywordSQL = GenerateSQL(entityName: "Keyword", childEntityName: "LocalizedKeyword", localizedFieldId: "KeywordId", schema: "Article", IsLocalized: true, TitleFieldName: "Title", CulturCode: lang);

        return entityName switch
        {
            "Category" => (await _repository.QueryAsync<LookupResult>(
                categorySQL,
                new
                {
                    Query = query,
                    CultureCode = lang,
                    tenant = _tenantInfo?.Identifier ?? "root",
                },
                cancellationToken: new CancellationToken()
            )).ToList(),
            "Keyword" => (await _repository.QueryAsync<LookupResult>(
                keywordSQL,
                new
                {
                    Query = query,
                    CultureCode = lang,
                    tenant = _tenantInfo?.Identifier ?? "root",
                },
                cancellationToken: new CancellationToken()
            )).ToList(),
            _ => new List<LookupResult>()
        };
    }

    private string GenerateSQL(string entityName, string childEntityName, string localizedFieldId, string schema = "", bool IsLocalized = false , string? ParentFieldName = null, string LocalsFieldName="Locals", string TitleFieldName = "Title", string IdFieldName = "Id",string CulturCode="ar-EG") =>
        $" SELECT C.{IdFieldName} as Id , {(IsLocalized ? "L." : "C.")}{TitleFieldName} as Title" +
        $" FROM {(string.IsNullOrWhiteSpace(schema) ? entityName : schema + "." + entityName)} C" +
        $"{(IsLocalized ? " LEFT JOIN " + (string.IsNullOrWhiteSpace(schema)? childEntityName : schema + "." + childEntityName) + " L  ON C." + IdFieldName + " = L." + localizedFieldId + " " : string.Empty) }" +
        $" WHERE {(IsLocalized ? "L." : "C.")}{TitleFieldName} Like N'%' + @Query + '%'" +
        $"{(IsLocalized ? " AND L.CulturCode = @CultureCode" : string.Empty)}" +
        $"{(string.IsNullOrWhiteSpace(ParentFieldName) ? string.Empty : " AND C." + ParentFieldName + " = @ParentId ")}" +
        $" AND C.\"TenantId\" = @tenant";

}

public class LookupFilter 
{
    public string FieldName { get; set; }
    public string Value { get; set; }
}

public enum EnumLookupFilterExp
{
    Equals = 0,
    GreaterThan = 1,
    LessThan = 2,
    Contains = 3,
    NotEquals = 4,
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
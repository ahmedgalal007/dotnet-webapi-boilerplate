using Ardalis.Specification;
using FSH.WebApi.Domain.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Article.News.specs;
internal class LocalNewsByNewsIdSpec : Specification<LocalizedNews>
{
    public LocalNewsByNewsIdSpec(Guid newsId, string? cultureCode) {
        Query.Where(e => e.NewsId == newsId);

        if(!string.IsNullOrWhiteSpace(cultureCode))
            Query.Where(e => e.culturCode == cultureCode);
    }
}

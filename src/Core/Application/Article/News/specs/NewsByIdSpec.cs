using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Article.News.specs;

internal class NewsByIdSpec : Specification<Domain.Article.News, NewsDto>, ISingleResultSpecification
{
    public NewsByIdSpec(Guid id, string? cultureCode) =>
        Query
          .Select(x => NewsDto.MapFrom(x, cultureCode))
          .Where(b => b.Id == id).Include(x => x.Locals);
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Article.News.specs;

public class NewsByIdListSpec : Specification<Domain.Article.News, NewsDto>, ISingleResultSpecification
{
    public NewsByIdListSpec(List<Guid> ids, string? cultureCode) => Query
          .Select(x => NewsDto.MapFrom(x, cultureCode))
          .Where(b => ids.Contains( b.Id)).Include(x => x.Locals);
}
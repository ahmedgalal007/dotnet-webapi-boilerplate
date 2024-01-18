using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Article.News.specs;

public class LocalizedNewsBySearchSpec : EntitiesByPaginationFilterSpec<Domain.Article.LocalizedNews, NewsDto>
{
    // IStringLocalizer<GetNewsRequestHandler> _localizer;
    public LocalizedNewsBySearchSpec(SearchLocalizedNewsRequest request, IReadRepository<Domain.Article.News> newsRepo, IStringLocalizer<GetNewsRequestHandler> localizer) : base(request)
    {
        // _localizer = localizer;
        Query.OrderBy(c => c.Id, !request.HasOrderBy());

        // .Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);

        if (!string.IsNullOrWhiteSpace(request.CulturCode))
            Query.Where(e => e.CulturCode == request.CulturCode);

        Query.Select(e => GetNewsItem(e.NewsId, request.CulturCode, newsRepo, localizer));
    }

    private static NewsDto GetNewsItem(Guid id, string? cultureCode, IReadRepository<Domain.Article.News> repo, IStringLocalizer<GetNewsRequestHandler> _t) =>
        repo.FirstOrDefaultAsync(new NewsByIdSpec(id, cultureCode)).Result
        ?? throw new NotFoundException(_t["News with ID: {0} Not Found.", id]);
}

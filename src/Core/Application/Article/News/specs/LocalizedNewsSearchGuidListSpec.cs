using FSH.WebApi.Domain.Article;

namespace FSH.WebApi.Application.Article.News.specs;
internal class LocalizedNewsSearchGuidListSpec : EntitiesByPaginationFilterSpec<LocalizedNews, BaseIdDto<Guid>>
{
    public LocalizedNewsSearchGuidListSpec(SearchLocalizedNewsByListRequest request)
        : base(request)
    {
        Query.OrderBy(c => c.Id, !request.HasOrderBy());

        // .Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);

        if (!string.IsNullOrWhiteSpace(request.CulturCode))
            Query.Where(e => e.CulturCode == request.CulturCode);

        Query.Select(e => new BaseIdDto<Guid> { Id = e.NewsId } );
    }
}

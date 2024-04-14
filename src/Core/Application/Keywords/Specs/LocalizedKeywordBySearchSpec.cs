using FSH.WebApi.Application.Article.News.specs;
using FSH.WebApi.Application.Article.News;
using FSH.WebApi.Domain.Keywords;

namespace FSH.WebApi.Application.Keywords.Specs;

public class LocalizedKeywordBySearchSpec : EntitiesByPaginationFilterSpec<Keyword, KeywordDto>
{
    public LocalizedKeywordBySearchSpec(SearchKeywordRequest request, IReadRepository<Keyword> keywordRepo, IStringLocalizer<GetKeywordRequestHandler> localizer)
        : base(request)
    {
        Query
            .Include(e => e.Locals)
            .OrderBy(c => c.Id, !request.HasOrderBy());
        Query.Select(e => GetKeywordItem(e.Id, request.CultureCode, keywordRepo, localizer));
    }

    private static KeywordDto GetKeywordItem(Guid id, string? cultureCode, IReadRepository<Keyword> repo, IStringLocalizer<GetKeywordRequestHandler> _t) =>
        repo.FirstOrDefaultAsync(new KeywordByIdSpec(id)).Result
        ?? throw new NotFoundException(_t["Keyword with ID: {0} Not Found.", id]);
}
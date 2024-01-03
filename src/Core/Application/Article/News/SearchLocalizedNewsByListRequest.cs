using FSH.WebApi.Application.Article.News.specs;
using FSH.WebApi.Domain.Article;

namespace FSH.WebApi.Application.Article.News;

public class SearchLocalizedNewsByListRequest : PaginationFilter, IRequest<PaginationResponse<NewsDto>>
{
    public string? CulturCode { get; set; }
}

public class SearchLocalizedNewsByListRequestHandler : IRequestHandler<SearchLocalizedNewsByListRequest, PaginationResponse<NewsDto>>
{
    private readonly IReadRepository<LocalizedNews> _localRepository;
    private readonly IReadRepository<Domain.Article.News> _newsRepository;

    public SearchLocalizedNewsByListRequestHandler(
        IReadRepository<LocalizedNews> repository,
        IReadRepository<Domain.Article.News> newsRepository)
    {
        _localRepository = repository;
        _newsRepository = newsRepository;
    }

    public async Task<PaginationResponse<NewsDto>> Handle(SearchLocalizedNewsByListRequest request, CancellationToken cancellationToken)
    {
        var spec = new LocalizedNewsSearchGuidListSpec(request);
        var idListPagination = await _localRepository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
        List<Guid> idList = idListPagination.Data.Select(e => e.Id).ToList() ?? new List<Guid>();
        var newsSpec = new NewsByIdListSpec(idList, request.CulturCode);
        return await _newsRepository.PaginatedListAsync(newsSpec, request.PageNumber, request.PageSize, cancellationToken);
    }
}
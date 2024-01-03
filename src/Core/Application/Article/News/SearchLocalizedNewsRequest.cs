using FSH.WebApi.Application.Article.News.specs;
using FSH.WebApi.Domain.Article;

namespace FSH.WebApi.Application.Article.News;

public class SearchLocalizedNewsRequest : PaginationFilter, IRequest<PaginationResponse<NewsDto>>
{
    public string? CulturCode { get; set; }
}

public class SearchLocalizedNewsRequestHandler : IRequestHandler<SearchLocalizedNewsRequest, PaginationResponse<NewsDto>>
{
    private readonly IReadRepository<LocalizedNews> _repository;
    private readonly IReadRepository<Domain.Article.News> _newsRepository;
    IStringLocalizer<GetNewsRequestHandler> _localizer;
    public SearchLocalizedNewsRequestHandler(
        IReadRepository<LocalizedNews> repository,
        IReadRepository<Domain.Article.News> newsRepository,
        IStringLocalizer<GetNewsRequestHandler> localizer)
    {
        _repository = repository;
        _newsRepository = newsRepository;
        _localizer = localizer;
    }

    public async Task<PaginationResponse<NewsDto>> Handle(SearchLocalizedNewsRequest request, CancellationToken cancellationToken)
    {
        var spec = new LocalizedNewsBySearchSpec(request, _newsRepository, _localizer);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}
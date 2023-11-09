
using FSH.WebApi.Domain.Article;

namespace FSH.WebApi.Application.Article.News;

public class SearchNewsRequest : PaginationFilter, IRequest<PaginationResponse<NewsDto>>
{
}

public class NewsBySearchRequestSpec : EntitiesByPaginationFilterSpec<Domain.Article.News, NewsDto>
{
    public NewsBySearchRequestSpec(SearchNewsRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Id, !request.HasOrderBy());
}

public class SearchNewsRequestHandler : IRequestHandler<SearchNewsRequest, PaginationResponse<NewsDto>>
{
    private readonly IReadRepository<Domain.Article.News> _repository;

    public SearchNewsRequestHandler(IReadRepository<Domain.Article.News> repository) => _repository = repository;

    public async Task<PaginationResponse<NewsDto>> Handle(SearchNewsRequest request, CancellationToken cancellationToken)
    {
        var spec = new NewsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}
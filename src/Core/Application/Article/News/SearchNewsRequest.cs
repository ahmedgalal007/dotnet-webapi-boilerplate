
using FSH.WebApi.Domain.Article;

namespace FSH.WebApi.Application.Article.News;

public class SearchNewsRequest : PaginationFilter, IRequest<PaginationResponse<NewsDto>>
{
    public string? CultureCode { get; set; }
}

public class NewsBySearchRequestSpec : EntitiesByPaginationFilterSpec<Domain.Article.News, NewsDto>
{
    public NewsBySearchRequestSpec(SearchNewsRequest request): base(request)
    {
        Query
        .Include(e => e.Locals)

        // .Where(e => e.Locals.Any(e => e.Title.Contains(request.Keyword) || e.Description.Contains(request.Keyword)))
        .Search(e => e.Locals.First(e => e.culturCode == (request.CultureCode ?? "en")).Title, "%" + request.Keyword + "%", 1)
        .OrderBy(c => c.Id, !request.HasOrderBy());

        Query.Select(e => NewsDto.MapFrom(e, request.CultureCode));

    }
}

public class SearchNewsRequestHandler : IRequestHandler<SearchNewsRequest, PaginationResponse<NewsDto>>
{
    private readonly IReadRepository<Domain.Article.News> _repository;
    // private readonly IReadRepository<Domain.Article.LocalizedNews> _localrepository;

    public SearchNewsRequestHandler(IReadRepository<Domain.Article.News> repository) => _repository = repository;

    public async Task<PaginationResponse<NewsDto>> Handle(SearchNewsRequest request, CancellationToken cancellationToken)
    {
        var spec = new NewsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
        // var result = await _localrepository.ListAsync(spec, cancellationToken);
        // return result;
    }
}
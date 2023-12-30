
//using FSH.WebApi.Domain.Article;

//namespace FSH.WebApi.Application.Article.News;

//public class SearchLocalizedNewsRequest : PaginationFilter, IRequest<PaginationResponse<NewsDto>>
//{
//    public string? CulturCode { get; set; }
//}

//public class LocalizedNewsBySearchRequestSpec : EntitiesByPaginationFilterSpec<Domain.Article.LocalizedNews, NewsDto>
//{
//    public LocalizedNewsBySearchRequestSpec(SearchLocalizedNewsRequest request) : base(request)
//    {
//        Query.OrderBy(c => c.Id, !request.HasOrderBy())
//            .Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);

//        if (!string.IsNullOrWhiteSpace(request.CulturCode))
//            Query.Where(e => e.culturCode == request.CulturCode);

//        Query.Select(e => NewsDto.MapFrom(e.News, request.CulturCode));
//    }
//}

//public class SearchLocalizedNewsRequestHandler : IRequestHandler<SearchLocalizedNewsRequest, PaginationResponse<NewsDto>>
//{
//    private readonly IReadRepository<LocalizedNews> _repository;

//    public SearchLocalizedNewsRequestHandler(IReadRepository<LocalizedNews> repository) => _repository = repository;

//    public async Task<PaginationResponse<NewsDto>> Handle(SearchLocalizedNewsRequest request, CancellationToken cancellationToken)
//    {
//        var spec = new LocalizedNewsBySearchRequestSpec(request);
//        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
//    }


//}
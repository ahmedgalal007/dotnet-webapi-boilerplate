
using FSH.WebApi.Domain.Article;

namespace FSH.WebApi.Application.Article.Categories;

public class SearchCategoriesRequest : PaginationFilter, IRequest<PaginationResponse<CategoryDto>>
{
}

public class CategoriesBySearchRequestSpec : EntitiesByPaginationFilterSpec<Category, CategoryDto>
{
    public CategoriesBySearchRequestSpec(SearchCategoriesRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Slug, !request.HasOrderBy());
}

public class SearchCategoriesRequestHandler : IRequestHandler<SearchCategoriesRequest, PaginationResponse<CategoryDto>>
{
    private readonly IReadRepository<Category> _repository;

    public SearchCategoriesRequestHandler(IReadRepository<Category> repository) => _repository = repository;

    public async Task<PaginationResponse<CategoryDto>> Handle(SearchCategoriesRequest request, CancellationToken cancellationToken)
    {
        var spec = new CategoriesBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}
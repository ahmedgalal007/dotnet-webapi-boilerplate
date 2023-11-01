using FSH.WebApi.Domain.Article;

namespace FSH.WebApi.Application.Article.Categories;

public class GetCategoryRequest : IRequest<CategoryDto>
{
    public Guid Id { get; set; }

    public GetCategoryRequest(Guid id) => Id = id;
}

public class CategoryByIdSpec : Specification<Brand, CategoryDto>, ISingleResultSpecification
{
    public CategoryByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetCategoryRequestHandler : IRequestHandler<GetCategoryRequest, CategoryDto>
{
    private readonly IRepository<Category> _repository;
    private readonly IStringLocalizer _t;

    public GetCategoryRequestHandler(IRepository<Category> repository, IStringLocalizer<GetCategoryRequestHandler> localizer) => (_repository, _t) = (repository, localizer);

    public async Task<CategoryDto> Handle(GetCategoryRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<Category, CategoryDto>)new CategoryByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Category {0} Not Found.", request.Id]);
}
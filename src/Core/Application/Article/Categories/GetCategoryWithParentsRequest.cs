using FSH.WebApi.Application.Article.Categories.Specs;
using FSH.WebApi.Domain.Article;

namespace FSH.WebApi.Application.Article.Categories;

public class GetCategoryWithParentsRequest : IRequest<CategoryDto>
{
    public Guid Id { get; set; }

    public GetCategoryWithParentsRequest(Guid id) => Id = id;
}

public class GetCategoryWithParentsRequestHandler : IRequestHandler<GetCategoryWithParentsRequest, CategoryDto>
{
    private readonly IRepository<Category> _repository;
    private readonly IStringLocalizer _t;

    public GetCategoryWithParentsRequestHandler(IRepository<Category> repository, IStringLocalizer<GetCategoryRequestHandler> localizer) => (_repository, _t) = (repository, localizer);

    public async Task<CategoryDto> Handle(GetCategoryWithParentsRequest request, CancellationToken cancellationToken)
    {
        var category = await _repository.FirstOrDefaultAsync(
            new GetCategoryByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Category {0} Not Found.", request.Id]);

        CategoryDto Last = category;
        while(Last != null && Last.ParentId != null)
        {
            var tmp = await _repository.FirstOrDefaultAsync(new GetCategoryByIdSpec((Guid)Last.ParentId), cancellationToken);
            if (tmp != null)
                Last.Parent = tmp;
            Last = tmp;
        }

        return category;
    }
}
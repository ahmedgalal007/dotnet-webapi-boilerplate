using FSH.WebApi.Application.Article.Categories;
using FSH.WebApi.Application.Article.Specs;
using FSH.WebApi.Application.Common.Localization;
using FSH.WebApi.Application.Keywords;
using FSH.WebApi.Domain.Article;

namespace FSH.WebApi.Application.Article;
public class UpdateCategoryRequest : BaseCategoryRequest<LocalizedCategoryDto> // LocalizedRequest<Guid, LocalizedKeywordDto>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Color { get; set; }

}

public class UpdateCategoryRequestValidation : CustomValidator<UpdateCategoryRequest>
{
}

public class UpdateCategoryRequestHandler : IRequestHandler<UpdateCategoryRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Category> _repository;
    private readonly IStringLocalizer _t;
    public UpdateCategoryRequestHandler(IRepositoryWithEvents<Category> repository, IStringLocalizer<UpdateCategoryRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<DefaultIdType> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = await _repository.FirstOrDefaultAsync(new CategoryByIdWithLocalsSpec(request.Id), cancellationToken);

        _ = category
        ?? throw new NotFoundException(_t["category {0} Not Found.", request.Id]);

        List<LocalizedCategory> locals = request.Locals.Select(e => LocalizedCategory.Create(e.Id,e.CulturCode, e.Name, e.Description)).ToList();

        category.Update(request.DefaultCultureCode!, request.Name, request.Description, request.Color, locals);

        await _repository.UpdateAsync(category, cancellationToken);

        return request.Id;
    }
}
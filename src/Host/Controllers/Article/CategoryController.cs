using FSH.WebApi.Application.Article;
using FSH.WebApi.Application.Article.Categories;
using FSH.WebApi.Application.Keywords;

namespace FSH.WebApi.Host.Controllers.Article;

public class CategoriesController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Categories)]
    [OpenApiOperation("Search brands using available filters.", "")]
    public Task<PaginationResponse<CategoryDto>> SearchAsync(SearchCategoriesRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Categories)]
    [OpenApiOperation("Get brand details.", "")]
    public Task<CategoryDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetCategoryRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Keywords)]
    [OpenApiOperation("Create a new Category.", "")]
    public async Task<Guid> CreateAsync(CreateCategoryRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Keywords)]
    [OpenApiOperation("Update a Category.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateCategoryRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Keywords)]
    [OpenApiOperation("Delete a Category.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteCategoryRequest(id));
    }

}
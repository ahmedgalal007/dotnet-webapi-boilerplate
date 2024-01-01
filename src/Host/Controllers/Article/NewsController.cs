using FSH.WebApi.Application.Article.News;
using FSH.WebApi.Application.Catalog.Products;
using Microsoft.AspNetCore.Mvc;

namespace FSH.WebApi.Host.Controllers.Article;
public class NewsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.News)]
    [OpenApiOperation("Search News using available filters.", "")]
    public Task<PaginationResponse<NewsDto>> SearchAsync(SearchNewsRequest request)
    {
        return Mediator.Send(request);
    }

    //[HttpPost("searchlocal")]
    //[MustHavePermission(FSHAction.Search, FSHResource.News)]
    //[OpenApiOperation("Search News using available filters.", "")]
    //public Task<PaginationResponse<NewsDto>> SearchLocalAsync(SearchLocalizedNewsRequest request)
    //{
    //    return Mediator.Send(request);
    //}

    [HttpGet("{id:guid}/{cultureCode?}")]
    [MustHavePermission(FSHAction.View, FSHResource.News)]
    [OpenApiOperation("Get News details.", "")]
    public Task<NewsDto> GetAsync(Guid id, string? cultureCode = "")
    {
        return Mediator.Send(new GetNewsRequest(id, cultureCode));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.News)]
    [OpenApiOperation("Create a new product.", "")]
    public Task<Guid> CreateAsync(CreateProductRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.News)]
    [OpenApiOperation("Update a product.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateProductRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.News)]
    [OpenApiOperation("Delete a product.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteProductRequest(id));
    }
}

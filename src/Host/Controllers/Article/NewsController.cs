using FSH.WebApi.Application.Article.News;
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

    [HttpPost("searchlocal")]
    [MustHavePermission(FSHAction.Search, FSHResource.News)]
    [OpenApiOperation("Search News using available filters.", "")]
    public Task<PaginationResponse<NewsDto>> SearchLocalAsync(SearchLocalizedNewsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPost("searchlocalizednewsbylist")]
    [MustHavePermission(FSHAction.Search, FSHResource.News)]
    [OpenApiOperation("Search localized news using available filters.", "")]
    public Task<PaginationResponse<NewsDto>> SearchLocalizedNewsByList(SearchLocalizedNewsByListRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}/{cultureCode?}")]
    [MustHavePermission(FSHAction.View, FSHResource.News)]
    [OpenApiOperation("Get News details.", "")]
    public Task<NewsDto> GetAsync(Guid id, string? cultureCode = "")
    {
        return Mediator.Send(new GetNewsRequest(id, cultureCode));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.News)]
    [OpenApiOperation("Create News Item.", "")]
    public Task<Guid> CreateAsync(CreateNewsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.News)]
    [OpenApiOperation("Update News Item.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateNewsRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.News)]
    [OpenApiOperation("Delete a news item.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteNewsRequest(id));
    }
}

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

    [HttpGet("{id:guid}/{cultureCode?}")]
    [MustHavePermission(FSHAction.View, FSHResource.News)]
    [OpenApiOperation("Get News details.", "")]
    public Task<NewsDto> GetAsync(Guid id, string? cultureCode = "")
    {
        return Mediator.Send(new GetNewsRequest(id, cultureCode));
    }
}

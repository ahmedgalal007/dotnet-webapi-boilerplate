using FSH.WebApi.Application.Catalog.Brands;
using FSH.WebApi.Application.Keywords;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FSH.WebApi.Host.Controllers.Keyword;
public class KeywordController : VersionedApiController
{
    [HttpPost("search")]
[MustHavePermission(FSHAction.Search, FSHResource.Keywords)]
[OpenApiOperation("Search Keywords using available filters.", "")]
public Task<PaginationResponse<KeywordDto>> SearchAsync(SearchKeywordRequest request)
{
    return Mediator.Send(request);
}

[HttpGet("{id:guid}")]
[MustHavePermission(FSHAction.View, FSHResource.Keywords)]
[OpenApiOperation("Get Keywords details.", "")]
public Task<KeywordDto> GetAsync(Guid id)
{
    return Mediator.Send(new GetKeywordRequest(id));
}

[HttpPost]
[MustHavePermission(FSHAction.Create, FSHResource.Keywords)]
[OpenApiOperation("Create a new Keywords.", "")]
public Task<Guid> CreateAsync(CreateKeywordRequest request)
{
    return Mediator.Send(request);
}

[HttpPut("{id:guid}")]
[MustHavePermission(FSHAction.Update, FSHResource.Keywords)]
[OpenApiOperation("Update a Keywords.", "")]
public async Task<ActionResult<Guid>> UpdateAsync(UpdateKeywordRequest request, Guid id)
{
    return id != request.Id
        ? BadRequest()
        : Ok(await Mediator.Send(request));
}

[HttpDelete("{id:guid}")]
[MustHavePermission(FSHAction.Delete, FSHResource.Keywords)]
[OpenApiOperation("Delete a Keywords.", "")]
public Task<Guid> DeleteAsync(Guid id)
{
    return Mediator.Send(new DeleteKeywordRequest(id));
}

}

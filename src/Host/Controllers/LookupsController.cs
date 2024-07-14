using FSH.WebApi.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FSH.WebApi.Host.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LookupsController : VersionedApiController
{
    private readonly ILookupService _lookupService;

    public LookupsController(ILookupService lookupService)
    {
        _lookupService = lookupService;
    }

    [HttpPost("search")]
    [AllowAnonymous]
    // [MustHavePermission(FSHAction.Search, FSHResource.Brands)]
    [OpenApiOperation("Search brands using available filters.", "")]
    public Task<List<KeyValuePair<Guid,string>>> SearchAsync(LookupRequest request)
    {
        return _lookupService.Search(request.EntityName, request.Query);
    }
}

public class LookupRequest
{
    public string EntityName { get; set; }
    public string Query { get; set; }
}

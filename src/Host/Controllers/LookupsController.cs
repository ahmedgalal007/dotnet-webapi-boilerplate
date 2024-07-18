using FSH.WebApi.Application.Common.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FSH.WebApi.Host.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LookupsController : ControllerBase
{
    private readonly ILookupService _lookupService;

    public LookupsController(ILookupService lookupService)
    {
        _lookupService = lookupService;
    }

    [HttpPost("get-lookup")]
    [AllowAnonymous]
    // [MustHavePermission(FSHAction.Search, FSHResource.Brands)]
    [OpenApiOperation("Search brands using available filters.", "")]
    public async Task<List<LookupResponse>> SearchAsync(LookupRequest request)
    {
        var result = await _lookupService.Search(request.EntityName, request.Query);
        return result.Adapt<List<LookupResponse>>();
    }
}

public class LookupResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
}
public class LookupRequest
{
    public string EntityName { get; set; }
    public string Query { get; set; }
}

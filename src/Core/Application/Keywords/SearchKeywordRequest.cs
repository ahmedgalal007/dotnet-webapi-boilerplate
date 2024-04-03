using FSH.WebApi.Application.Catalog.Brands;
using FSH.WebApi.Application.Common.Persistence;
using FSH.WebApi.Application.Keywords.Specs;
using FSH.WebApi.Domain.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Keywords;
public class SearchKeywordRequest : PaginationFilter, IRequest<PaginationResponse<KeywordDto>>
{
    public string? CultureCode { get; set; }
}

public class SearchKeywordRequestValidation : CustomValidator<SearchKeywordRequest>
{
}
public class SearchKeywordRequestHandler : IRequestHandler<SearchKeywordRequest, PaginationResponse<KeywordDto>>
{
    private readonly IReadRepository<Keyword> _repository;
    public SearchKeywordRequestHandler(IReadRepository<Keyword> repository) => _repository = repository;
    public async Task<PaginationResponse<KeywordDto>> Handle(SearchKeywordRequest request, CancellationToken cancellationToken)
    {
        var spec = new KeywordBySearchSpec(request);
        var result = await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
        return result;
    }
}


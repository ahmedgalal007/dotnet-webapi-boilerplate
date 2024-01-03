using FSH.WebApi.Application.Article.News.specs;

namespace FSH.WebApi.Application.Article.News;

public class GetNewsByIdListRequest : PaginationFilter, IRequest<PaginationResponse<NewsDto>>
{
    public List<Guid> IdList { get; set; }
    public string? CultureCode { get; set; }
    public GetNewsByIdListRequest(List<Guid> idList, string? cultureCode)
    {
      IdList = idList;
      CultureCode = cultureCode;
    }
}

// TODO Implementaion of the handler has been Commented because it will be used internally only in SearchLocalizedNewsByListRequestHandler

/*
public class GetNewsByIdListRequestHandler : IRequestHandler<GetNewsByIdListRequest, PaginationResponse<NewsDto>>
{
  private readonly IRepository<Domain.Article.News> _repository;
  private readonly IStringLocalizer _t;

  public GetNewsByIdListRequestHandler(IRepository<Domain.Article.News> repository, IStringLocalizer<GetNewsByIdListRequestHandler> localizer) => (_repository, _t) = (repository, localizer);

  public async Task<PaginationResponse<NewsDto>> Handle(GetNewsByIdListRequest request, CancellationToken cancellationToken) =>
        await _repository.PaginatedListAsync(new NewsByIdListSpec(request.IdList, request.CultureCode),request.PageNumber, request.PageSize, cancellationToken)
        ?? throw new NotFoundException(_t["No Matched News Found from the IdList."]);
}
*/
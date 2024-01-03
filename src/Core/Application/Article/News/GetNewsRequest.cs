using FSH.WebApi.Application.Article.News.specs;

namespace FSH.WebApi.Application.Article.News;

public class GetNewsRequest : IRequest<NewsDto>
{
    public Guid Id { get; set; }
    public string? CultureCode { get; set; }
    public GetNewsRequest(Guid id, string? cultureCode)
    {
      Id = id;
      CultureCode = cultureCode;
    }
}

public class GetNewsRequestHandler : IRequestHandler<GetNewsRequest, NewsDto>
{
  private readonly IRepository<Domain.Article.News> _repository;
  private readonly IStringLocalizer _t;

  public GetNewsRequestHandler(IRepository<Domain.Article.News> repository, IStringLocalizer<GetNewsRequestHandler> localizer) => (_repository, _t) = (repository, localizer);

  public async Task<NewsDto> Handle(GetNewsRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(new NewsByIdSpec(request.Id, request.CultureCode), cancellationToken)
        ?? throw new NotFoundException(_t["News {0} Not Found.", request.Id]);

        // Domain.Article.News? result = await _repository.GetByIdAsync(request.Id);
        // if (result == null) throw new NotFoundException(_t["News {0} Not Found.", request.Id]);

        // LocalizedNews? local = result.Locals.FirstOrDefault(x => x.culturCode == result.DefaultCulturCode);
        // return new NewsDto()
        // {
        //   Id = result.Id,
        //   Slug = result.slug,
        //   MainImage = result.MainImage,
        //   CultureCode = local.culturCode,
        //   Title = local.Title,
        //   SubTitle = local.SocialTitle,
        //   SeoTitle = local.SocialTitle,
        //   SocialTitle = local.SocialTitle,
        //   Description = local.Description,
        //   Body = local.Body
        // };
}
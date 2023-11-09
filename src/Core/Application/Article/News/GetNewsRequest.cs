namespace FSH.WebApi.Application.Article.News;

public class GetNewsRequest : IRequest<NewsDto>
{
    public Guid Id { get; set; }

    public GetNewsRequest(Guid id) => Id = id;
}

public class NewsByIdSpec : Specification<Domain.Article.News>, ISingleResultSpecification
{
    public NewsByIdSpec(Guid id) => Query.Where(b => b.Id == id);
}

public class GetNewsRequestHandler : IRequestHandler<GetNewsRequest, NewsDto>
{
    private readonly IRepository<Domain.Article.News> _repository;
    private readonly IStringLocalizer _t;

    public GetNewsRequestHandler(IRepository<Domain.Article.News> repository, IStringLocalizer<GetNewsRequestHandler> localizer) => (_repository, _t) = (repository, localizer);

    public async Task<NewsDto> Handle(GetNewsRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<Domain.Article.News, NewsDto>)new NewsByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["News {0} Not Found.", request.Id]);
}
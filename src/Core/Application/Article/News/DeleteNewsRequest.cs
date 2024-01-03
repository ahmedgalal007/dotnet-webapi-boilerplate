using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Article.News;

public class DeleteNewsRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteNewsRequest(Guid id) => Id = id;
}

public class DeleteNewsRequestHandler : IRequestHandler<DeleteNewsRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Domain.Article.News> _newsRepo;
    private readonly IStringLocalizer _t;

    public DeleteNewsRequestHandler(IRepositoryWithEvents<Domain.Article.News> brandRepo, IReadRepository<Product> productRepo, IStringLocalizer<DeleteNewsRequestHandler> localizer) =>
        (_newsRepo, _t) = (brandRepo, localizer);

    public async Task<Guid> Handle(DeleteNewsRequest request, CancellationToken cancellationToken)
    {
        //if (await _productRepo.AnyAsync(new ProductsByBrandSpec(request.Id), cancellationToken))
        //{
        //    throw new ConflictException(_t["News cannot be deleted as it's being used."]);
        //}

        var news = await _newsRepo.GetByIdAsync(request.Id, cancellationToken);

        _ = news ?? throw new NotFoundException(_t["News {0} Not Found."]);
        news.DomainEvents.Add(EntityDeletedEvent.WithEntity(news));
        await _newsRepo.DeleteAsync(news, cancellationToken);

        return request.Id;
    }
}
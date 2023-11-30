﻿using FSH.WebApi.Application.Catalog.Products;
using FSH.WebApi.Domain.Article;

namespace FSH.WebApi.Application.Catalog.Brands;

public class DeleteNewsRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteNewsRequest(Guid id) => Id = id;
}

public class DeleteNewsRequestHandler : IRequestHandler<DeleteNewsRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<News> _newsRepo;
    private readonly IReadRepository<Product> _productRepo;
    private readonly IStringLocalizer _t;

    public DeleteNewsRequestHandler(IRepositoryWithEvents<News> brandRepo, IReadRepository<Product> productRepo, IStringLocalizer<DeleteNewsRequestHandler> localizer) =>
        (_newsRepo, _productRepo, _t) = (brandRepo, productRepo, localizer);

    public async Task<Guid> Handle(DeleteNewsRequest request, CancellationToken cancellationToken)
    {
        if (await _productRepo.AnyAsync(new ProductsByBrandSpec(request.Id), cancellationToken))
        {
            throw new ConflictException(_t["News cannot be deleted as it's being used."]);
        }

        var news = await _newsRepo.GetByIdAsync(request.Id, cancellationToken);

        _ = news ?? throw new NotFoundException(_t["News {0} Not Found."]);

        await _newsRepo.DeleteAsync(news, cancellationToken);

        return request.Id;
    }
}
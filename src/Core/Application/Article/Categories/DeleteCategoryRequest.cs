using FSH.WebApi.Application.Article.News;
using FSH.WebApi.Domain.Article;
using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Article;
public class DeleteCategoryRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteCategoryRequest(Guid id) => Id = id;
}

public class DeleteCategoryRequestValidation : CustomValidator<DeleteCategoryRequest>
{
}
public class DeleteCategoryRequestHandler : IRequestHandler<DeleteCategoryRequest, Guid>
{

    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Category> _categoryRepo;
    private readonly IStringLocalizer _t;

    public DeleteCategoryRequestHandler(IRepositoryWithEvents<Category> CategoryRepo, IStringLocalizer<DeleteCategoryRequestHandler> localizer) =>
        (_categoryRepo, _t) = (CategoryRepo, localizer);
    public async Task<DefaultIdType> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepo.GetByIdAsync(request.Id, cancellationToken);

        _ = category ?? throw new NotFoundException(_t["News {0} Not Found."]);
        category.DomainEvents.Add(EntityDeletedEvent.WithEntity(category));
        await _categoryRepo.DeleteAsync(category, cancellationToken);

        return request.Id;
    }
}


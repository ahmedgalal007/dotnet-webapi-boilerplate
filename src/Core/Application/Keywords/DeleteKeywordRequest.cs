using FSH.WebApi.Application.Article.News;
using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Keywords;
public class DeleteKeywordRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteKeywordRequest(Guid id) => Id = id;
}

public class DeleteKeywordRequestValidation : CustomValidator<DeleteKeywordRequest>
{
}
public class DeleteKeywordRequestHandler : IRequestHandler<DeleteKeywordRequest, Guid>
{

    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Keyword> _keywordRepo;
    private readonly IStringLocalizer _t;

    public DeleteKeywordRequestHandler(IRepositoryWithEvents<Keyword> keywordRepo, IStringLocalizer<DeleteNewsRequestHandler> localizer) =>
        (_keywordRepo, _t) = (keywordRepo, localizer);
    public async Task<DefaultIdType> Handle(DeleteKeywordRequest request, CancellationToken cancellationToken)
    {
        var keyword = await _keywordRepo.GetByIdAsync(request.Id, cancellationToken);

        _ = keyword ?? throw new NotFoundException(_t["News {0} Not Found."]);
        keyword.DomainEvents.Add(EntityDeletedEvent.WithEntity(keyword));
        await _keywordRepo.DeleteAsync(keyword, cancellationToken);

        return request.Id;
    }
}


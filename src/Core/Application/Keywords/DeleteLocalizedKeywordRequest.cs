using FSH.WebApi.Application.Article.News;
using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Keywords;
public class DeleteLocalizedKeywordRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public Guid KeywordId { get; set; }
    public string CultureCode { get; set; } = string.Empty;

    public DeleteLocalizedKeywordRequest(Guid keywordId, Guid id)
    {
        (KeywordId, Id) = (keywordId, id);
    }
}

public class DeleteLocalizedKeywordRequestValidation : CustomValidator<DeleteLocalizedKeywordRequest>
{
}

public class DeleteLocalizedKeywordRequestHandler : IRequestHandler<DeleteLocalizedKeywordRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents

    private readonly IRepositoryWithEvents<Keyword> _keywordRepo;
    private readonly IStringLocalizer _t;

    public DeleteLocalizedKeywordRequestHandler(IRepositoryWithEvents<Keyword> keywordRepo, IStringLocalizer<DeleteLocalizedKeywordRequestHandler> localizer) =>
        (_keywordRepo, _t) = (keywordRepo, localizer);
    public async Task<DefaultIdType> Handle(DeleteLocalizedKeywordRequest request, CancellationToken cancellationToken)
    {
        var keyword = await _keywordRepo.GetByIdAsync(request.KeywordId, cancellationToken);

        _ = keyword ?? throw new NotFoundException(_t["The Keywoed With Id: {0} Not Found.", request.KeywordId]);
        var deletedLocal = keyword.DeleteLocal(request.Id, request.CultureCode);
        keyword.DomainEvents.Add(EntityDeletedEvent.WithEntity(deletedLocal));

        // await _keywordRepo.DeleteAsync(keyword, cancellationToken);

        return request.Id;
    }
}
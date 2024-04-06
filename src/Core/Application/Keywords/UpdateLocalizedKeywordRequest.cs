using FSH.WebApi.Application.Article.News;
using FSH.WebApi.Application.Common.Persistence;
using FSH.WebApi.Domain.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Keywords;
public class UpdateLocalizedKeywordRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public Guid KeywordId { get; private set; }
    public string CulturCode { get; private set; } = string.Empty;
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public bool Enabled { get; private set; } = false;
    public bool IsDefault { get; private set; } = false;
    

}

public class UpdateLocalizedKeywordRequestValidation : CustomValidator<UpdateLocalizedKeywordRequest>
{
}
public class UpdateLocalizedKeywordRequestHandler : IRequestHandler<UpdateLocalizedKeywordRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Keyword> _repository;
    private readonly IStringLocalizer _t;
    public UpdateLocalizedKeywordRequestHandler(IRepositoryWithEvents<Keyword> repository, IStringLocalizer<UpdateLocalizedKeywordRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<DefaultIdType> Handle(UpdateLocalizedKeywordRequest request, CancellationToken cancellationToken)
    {
        var keyword = await _repository.GetByIdAsync(request.KeywordId, cancellationToken);

        _ = keyword
        ?? throw new NotFoundException(_t["Keyword {0} Not Found.", request.KeywordId]);

        var local = keyword.Locals.First(e => e.Id == request.Id);

        if (local == null) throw new Exception("Keywords Local Not Found");
        local.Update(request.Title, request.Description, request.Enabled, request.IsDefault);

        await _repository.UpdateAsync(keyword, cancellationToken);

        return local.Id;
    }

}


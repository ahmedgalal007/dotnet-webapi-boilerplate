using FSH.WebApi.Application.Article.News;
using FSH.WebApi.Application.Common.Persistence;
using FSH.WebApi.Domain.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Keywords;
public class UpdateKeywordRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Keyword { get; set; }
    public string Description { get; set; }
    public string CultureCode { get; set; } = "ar";
}

public class UpdateKeywordRequestValidation : CustomValidator<UpdateKeywordRequest>
{
}
public class UpdateKeywordRequestHandler : IRequestHandler<UpdateKeywordRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Keyword> _repository;
    private readonly IStringLocalizer _t;
    public UpdateKeywordRequestHandler(IRepositoryWithEvents<Keyword> repository, IStringLocalizer<UpdateKeywordRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<DefaultIdType> Handle(UpdateKeywordRequest request, CancellationToken cancellationToken)
    {
        var keyword = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = keyword
        ?? throw new NotFoundException(_t["Keyword {0} Not Found.", request.Id]);

        keyword.Update(request.CultureCode, request.Keyword, request.Description, "");

        await _repository.UpdateAsync(keyword, cancellationToken);

        return request.Id;
    }
}


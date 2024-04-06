using FSH.WebApi.Application.Common.Localization;
using FSH.WebApi.Domain.Common.Contracts;
using FSH.WebApi.Domain.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Keywords;
public class CreateKeywordRequest : LocalizedRequest<Guid, LocalizedKeyword>
{
    // public Guid Id { get; set; }
    public string? Color { get; set; }
    public bool? IsCreativeWork { get; set; } = false;
    public bool? IsEvent { get; set; } = false;
    public bool? IsOrganization { get; set; } = false;
    public bool? IsPerson { get; set; } = false;
    public bool? IsPlace { get; set; } = false;
    public bool? IsProduct { get; set; } = false;

}

public class CreateKeywordRequestValidation : CustomValidator<CreateKeywordRequest>
{
}
    public class CreateKeywordRequestHandler : IRequestHandler<CreateKeywordRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Keyword> _repository;
    private readonly IStringLocalizer _t;

    public CreateKeywordRequestHandler(IRepositoryWithEvents<Keyword> repository, IStringLocalizer<CreateKeywordRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);
    public async Task<DefaultIdType> Handle(CreateKeywordRequest request, CancellationToken cancellationToken)
    {
        var keyword = Keyword.Create(request.DefaultCultureCode, request.Languages, request.Locals, request.IsCreativeWork, request.IsEvent, request.IsOrganization, request.IsPerson, request.IsPlace, request.IsProduct,null);

        await _repository.AddAsync(keyword, cancellationToken);

        return keyword.Id;
    }
}

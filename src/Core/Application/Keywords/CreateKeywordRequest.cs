using FSH.WebApi.Domain.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Keywords;
public class CreateKeywordRequest : IRequest<Guid>
{
    // public Guid Id { get; set; }
    public string? DefaultCultureCode { get; set; } = "ar";
    public string? Color { get; set; }

    public bool? IsCreativeWork { get; set; } = false;
    public bool? IsEvent { get; set; } = false;
    public bool? IsOrganization { get; set; } = false;
    public bool? IsPerson { get; set; } = false;
    public bool? IsPlace { get; set; } = false;
    public bool? IsProduct { get; set; } = false;
    public ICollection<LocalizedKeyword>? Locals { get; set; } = new List<LocalizedKeyword>();

}

public class CreateKeywordRequestValidation : CustomValidator<CreateKeywordRequest>
{
}
    public class CreateKeywordRequestHandler : IRequestHandler<CreateKeywordRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Keyword> _repository;
    public CreateKeywordRequestHandler(IRepositoryWithEvents<Keyword> repository) => _repository = repository;
    public async Task<DefaultIdType> Handle(CreateKeywordRequest request, CancellationToken cancellationToken)
    {
        var keyword = Keyword.Create(request.DefaultCultureCode, request.Locals, request.IsCreativeWork, request.IsEvent, request.IsOrganization, request.IsPerson, request.IsPlace, request.IsProduct,null);

        await _repository.AddAsync(keyword, cancellationToken);

        return keyword.Id;
    }
}

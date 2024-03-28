using FSH.WebApi.Domain.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Keywords;
public class CreateKeywordRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Keyword { get; set; }
    public string Description { get; set; }
    public string CultureCode { get; set; } = "ar";

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
        var keyword = Keyword.Create(request.CultureCode, request.Keyword, request.Description,null);

        await _repository.AddAsync(keyword, cancellationToken);

        return keyword.Id;
    }
}

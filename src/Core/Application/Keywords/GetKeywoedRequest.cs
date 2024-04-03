using FSH.WebApi.Application.Keywords.Specs;
using FSH.WebApi.Domain.Keywords;

namespace FSH.WebApi.Application.Keywords;

public class GetKeywordRequest : IRequest<KeywordDto>
{
    public Guid Id { get; set; }

    public GetKeywordRequest(Guid id) => Id = id;
}

public class GetKeywordRequestHandler : IRequestHandler<GetKeywordRequest, KeywordDto>
{
    private readonly IRepository<Keyword> _repository;
    private readonly IStringLocalizer _t;

    public GetKeywordRequestHandler(IRepository<Keyword> repository, IStringLocalizer<GetKeywordRequestHandler> localizer) => (_repository, _t) = (repository, localizer);

    public async Task<KeywordDto> Handle(GetKeywordRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<Keyword, KeywordDto>)new KeywordByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Keyword {0} Not Found.", request.Id]);
}
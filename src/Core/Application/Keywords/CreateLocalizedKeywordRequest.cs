//using FSH.WebApi.Domain.Keywords;

//namespace FSH.WebApi.Application.Keywords;
//public class CreateLocalizedKeywordRequest : IRequest<Guid>
//{
//    // public Guid Id { get; set; }
//    public Guid KeywordId { get; private set; }
//    public string CulturCode { get; private set; } = string.Empty;
//    public string Title { get; private set; } = string.Empty;
//    public string Description { get; private set; } = string.Empty;
//    public bool Enabled { get; private set; } = false;
//    public bool IsDefault { get; private set; } = false;

//}

//public class CreateLocalizedKeywordRequestValidation : CustomValidator<CreateLocalizedKeywordRequest>
//{
//}
//    public class CreateLocalizedKeywordRequestHandler : IRequestHandler<CreateLocalizedKeywordRequest, Guid>
//{
//    // Add Domain Events automatically by using IRepositoryWithEvents
//    private readonly IRepositoryWithEvents<Keyword> _repository;
//    private readonly IStringLocalizer _t;
//    public CreateLocalizedKeywordRequestHandler(IRepositoryWithEvents<Keyword> repository, IStringLocalizer<CreateLocalizedKeywordRequestHandler> localizer) =>
//        (_repository, _t) = (repository, localizer);
//    public async Task<DefaultIdType> Handle(CreateLocalizedKeywordRequest request, CancellationToken cancellationToken)
//    {
//        var keyword = await _repository.GetByIdAsync(request.KeywordId, cancellationToken);

//        _ = keyword
//        ?? throw new NotFoundException(_t["Keyword {0} Not Found.", request.KeywordId]);

//        var local = keyword.AddOrUpdateLocal(request.CulturCode, request.Title, request.Description, request.Enabled, request.IsDefault);

//        await _repository.UpdateAsync(keyword, cancellationToken);

//        return local.Id;
//    }
//}

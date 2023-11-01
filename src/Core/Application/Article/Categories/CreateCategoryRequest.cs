using FSH.WebApi.Domain.Article;

namespace FSH.WebApi.Application.Article.Categories;
public class CreateCategoryRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Color { get; set; } = "#ffffff";
    public string? CultureCode { get; set; } = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
}

public class CreateCategoryRequestValidator : CustomValidator<CreateCategoryRequest>
{
    public CreateCategoryRequestValidator(IReadRepository<Category> repository, IStringLocalizer<CreateCategoryRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await repository.FirstOrDefaultAsync(new CategoryByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["Category {0} already Exists.", name]);
}


public class CreateCategoryRequestHandler : IRequestHandler<CreateCategoryRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Category> _repository;

    public CreateCategoryRequestHandler(IRepositoryWithEvents<Category> repository) => _repository = repository;

    public async Task<Guid> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        string lang = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        var category = new Category(request.Name, request.Description, request.Color, request.CultureCode ?? lang);

        await _repository.AddAsync(category, cancellationToken);

        return category.Id;
    }
}
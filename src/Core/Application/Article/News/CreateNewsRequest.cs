using FSH.WebApi.Domain.Article;
using System.ComponentModel.DataAnnotations;

namespace FSH.WebApi.Application.Article.News;
public class CreateNewsRequest : IRequest<Guid>
{
    public string? CultureCode { get; set; } = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public string? SubTitle { get; set; }
    public string? SEOTitle { get; set; }
    public string? SocialTitle { get; set; }
    public string? Body { get; set; }
}

public class CreateNewsRequestValidator : CustomValidator<CreateNewsRequest>
{
    public CreateNewsRequestValidator(IReadRepository<Domain.Article.News> repository, IStringLocalizer<CreateNewsRequestValidator> T) =>
        RuleFor(p => p.Title)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (title, ct) => await repository.FirstOrDefaultAsync(new NewsByTitleSpec(title), ct) is null)
                .WithMessage((_, name) => T["News {0} already Exists.", name]);
}


public class CreateNewsRequestHandler : IRequestHandler<CreateNewsRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Domain.Article.News> _repository;

    public CreateNewsRequestHandler(IRepositoryWithEvents<Domain.Article.News> repository) => _repository = repository;

    public async Task<Guid> Handle(CreateNewsRequest request, CancellationToken cancellationToken)
    {
        string lang = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        var news = new Domain.Article.News(request.Title,request.Description,request.Body,request.SubTitle,request.SEOTitle,request.SocialTitle,request.CultureCode?? lang);
        await _repository.AddAsync(news, cancellationToken);

        return news.Id;
    }
}
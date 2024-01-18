using FSH.WebApi.Application.Article.News.specs;
using FSH.WebApi.Application.Common.SEO;
using FSH.WebApi.Domain.Article;
using FSH.WebApi.Domain.Catalog;
using FSH.WebApi.Domain.Common.Events;
using System.ComponentModel.DataAnnotations;

namespace FSH.WebApi.Application.Article.News;
public class CreateNewsRequest : IRequest<Guid>
{
    // public Guid? Id { get; set; } = default!;
    public string? CultureCode { get; set; } = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
    public string Title { get; set; } = default!;
    public string? Slug { get; set; }
    public string? Description { get; set; }
    public string? SubTitle { get; set; }
    public string? SEOTitle { get; set; }
    public string? SocialTitle { get; set; }
    public string? Body { get; set; }
    public Guid? GategoryId { get; set; }

    public FileUploadRequest? MainImage { get; set; }
}

//public class CreateNewsRequestValidator : CustomValidator<CreateNewsRequest>
//{
//    public CreateNewsRequestValidator(IReadRepository<Domain.Article.News> repository, IStringLocalizer<CreateNewsRequestValidator> T) =>
//        RuleFor(p => p.Title)
//            .NotEmpty()
//            .MaximumLength(75)
//            .MustAsync(async (title, ct) => await repository.FirstOrDefaultAsync(new NewsByTitleSpec(title), ct) is null)
//                .WithMessage((_, name) => T["News {0} already Exists.", name]);
//}

public class CreateNewsRequestHandler : IRequestHandler<CreateNewsRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Domain.Article.News> _repository;
    private readonly ISeoUtilitiesService _seoUtilitiesService;
    private readonly IFileStorageService _file;

    public CreateNewsRequestHandler(IRepositoryWithEvents<Domain.Article.News> repository, ISeoUtilitiesService seoUtilitiesService, IFileStorageService file) =>
        (_repository, _seoUtilitiesService, _file) = (repository, seoUtilitiesService, file);

    public async Task<Guid> Handle(CreateNewsRequest request, CancellationToken cancellationToken)
    {
        string lang = request.CultureCode ?? Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        // string newsImagePath = await _file.UploadAsync<Product>(request.MainImage, FileType.Image, cancellationToken);
        string slug = request.Slug ?? _seoUtilitiesService.Slugify(request.Title);

        //var news = new Domain.Article.News(request.Title, slug, request.Description, request.Body, request.SubTitle, request.SEOTitle, request.SocialTitle, lang, newsImagePath);
        var news = Domain.Article.News.Create(request.Title, slug, request.Description, request.Body, request.SubTitle, request.SEOTitle, request.SocialTitle, lang, "", (Guid)request.GategoryId);
        news.DomainEvents.Add(EntityCreatedEvent.WithEntity(news));
        await _repository.AddAsync(news, cancellationToken);

        return news.Id;
    }
}
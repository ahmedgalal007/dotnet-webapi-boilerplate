using FSH.WebApi.Application.Article.News.specs;
using FSH.WebApi.Application.Catalog.Products;
using FSH.WebApi.Domain.Article;
using FSH.WebApi.Domain.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Article.News;
public class UpdateNewsRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string? CultureCode { get; set; } = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public string? SubTitle { get; set; }
    public string? SEOTitle { get; set; }
    public string? SocialTitle { get; set; }
    public string? Body { get; set; }
    public Guid? CategoryId { get; set; }

    public bool DeleteCurrentImage { get; set; } = false;
    public FileUploadRequest? MainImage { get; set; }
}

public class UpdateNewsRequestHandler : IRequestHandler<UpdateNewsRequest, Guid>
{
    private readonly IRepository<Domain.Article.News> _repository;
    private readonly IRepository<Domain.Article.LocalizedNews> _localRepository;
    private readonly IStringLocalizer _t;
    private readonly IFileStorageService _file;

    public UpdateNewsRequestHandler(IRepository<Domain.Article.News> repository, IRepository<LocalizedNews> localRepository, IStringLocalizer<UpdateNewsRequestHandler> localizer, IFileStorageService file) =>
        (_repository, _localRepository, _t, _file) = (repository, localRepository, localizer, file);

    public async Task<Guid> Handle(UpdateNewsRequest request, CancellationToken cancellationToken)
    {
        var news = await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(_t["news item {0} Not Found.", request.Id]);

        // Get all localnews because request.CultureCode == null 
        List<LocalizedNews>? locales = await _localRepository.ListAsync(new LocalNewsByNewsIdSpec(request.Id, null), cancellationToken);
        news.Locals = locales;
        // Remove old image if flag is set
        if (request.DeleteCurrentImage)
        {
            string? currentProductImagePath = news.MainImage;
            if (!string.IsNullOrEmpty(currentProductImagePath))
            {
                string root = Directory.GetCurrentDirectory();
                _file.Remove(Path.Combine(root, currentProductImagePath));
            }

            news = news.ClearImagePath();
        }

        string? newsImagePath = request.MainImage is not null
            ? await _file.UploadAsync<Product>(request.MainImage, FileType.Image, cancellationToken)
            : null;

        var updatedNews = news.Update(request.Title, request.Description, request.Body, request.SubTitle, request.SEOTitle,request.SocialTitle, request.CultureCode, newsImagePath, (Guid)request.CategoryId!);

        // Add Domain Events to be raised after the commit
        news.DomainEvents.Add(EntityUpdatedEvent.WithEntity(news));

        await _repository.UpdateAsync(updatedNews, cancellationToken);

        return request.Id;
    }
}
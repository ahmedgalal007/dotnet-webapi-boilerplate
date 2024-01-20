using FSH.WebApi.Domain.Catalog;
using FSH.WebApi.Domain.Common.Localizations;
using FSH.WebApi.Domain.Editors;
using FSH.WebApi.Domain.Keywords;
using FSH.WebApi.Domain.Medias;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace FSH.WebApi.Domain.Article;

public class News : LocalizedEntity<LocalizedNews>, IAggregateRoot
{
    public string? Slug { get; set; } = string.Empty;
    public string? MainImage { get; set; }
    public Guid CategoryId { get; set; }
    public virtual IEnumerable<Editor>? Editors { get; set; } = Enumerable.Empty<Editor>();
    public virtual IEnumerable<Keyword>? Keywords { get; set; } = new List<Keyword>();
    public virtual IEnumerable<Album>? Albums { get; set; } = new List<Album>();
    public virtual IEnumerable<Media>? Medias { get; set; } = new List<Media>();
    private News()
    {
    }

    protected override LocalizedNews CreateLocal(string cultureCode)
    {
        return LocalizedNews.Create(Id, cultureCode, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
    }

    public static News Create(string cultureCode, string title, string slug, string? description, string? body, string? subTitle, string? seoTitle, string? socialTitle, string? mainImagePath, Guid categoryId)
    {
        // if (string.IsNullOrWhiteSpace(cultureCode))
        // cultureCode = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower();
        News instance = new News()
        {
            DefaultCulturCode = cultureCode,
            Slug = slug,
            CategoryId = categoryId
        };
        instance.AddOrUpdateLocal(cultureCode, title, description, body, subTitle, seoTitle, socialTitle, mainImagePath);
        return instance;

    }

    public News Update(string cultureCode, string? title, string? description, string? body, string? subTitle, string? seoTitle, string? socialTitle, string? mainImagePath, Guid categoryId)
    {
        if (categoryId != Guid.Empty && CategoryId.Equals(categoryId) is not true) CategoryId = categoryId;
        LocalizedNews local = AddOrUpdateLocal(cultureCode, title, description, body, subTitle, seoTitle, socialTitle, mainImagePath);
        return this;
    }

    public LocalizedNews AddOrUpdateLocal(string cultureCode, string? title, string? description, string? body, string? subTitle, string? seoTitle, string? socialTitle, string? mainImagePath)
    {
        LocalizedNews? localizedNews = GetLocal(cultureCode)
            ?? LocalizedNews.Create(this.Id, cultureCode, title, description, subTitle, seoTitle, socialTitle, body);

        localizedNews.Update(title, description, subTitle, seoTitle, socialTitle, body);

        // if (title is not null && localizedNews.Title.Equals(title) is not true) localizedNews.Title = title;
        // if (description is not null && localizedNews.Description.Equals(description) is not true) localizedNews.Description = description;
        // if (subTitle is not null && localizedNews.SubTitle.Equals(subTitle) is not true) localizedNews.SubTitle = subTitle;
        // if (seoTitle is not null && localizedNews.SEOTitle.Equals(seoTitle) is not true) localizedNews.SEOTitle = seoTitle;
        // if (socialTitle is not null && localizedNews.SocialTitle.Equals(socialTitle) is not true) localizedNews.SocialTitle = socialTitle;
        // if (body is not null && localizedNews.Body.Equals(body) is not true) localizedNews.Body = body;

        if (!string.IsNullOrWhiteSpace(mainImagePath) && !MainImage.Equals(mainImagePath)) MainImage = mainImagePath;

        return localizedNews;
    }

    public News ClearImagePath()
    {
        MainImage = string.Empty;
        return this;
    }

    

    /*
    public Guid TitleId { get; set; }
    public Guid DescriptionId { get; set; }

    public Guid? SubTitleId { get; set; }
    public Guid? SEOTitleId { get; set; }
    public Guid? SocialTitleId { get; set; }
    public Guid? BodyId { get; set; }

    // [ForeignKey(nameof(TitleId))]

    public virtual LocalizationSet Title { get; set; } = new LocalizationSet();
    // [ForeignKey(nameof(DescriptionId))]
    public virtual LocalizationSet Description { get; set; } = new LocalizationSet();

    public virtual LocalizationSet? SubTitle { get; set; }
    public virtual LocalizationSet? SEOTitle { get; set; }
    public virtual LocalizationSet? SocialTitle { get; set; }
    public virtual LocalizationSet? Body { get; set; }

    public virtual ICollection<LocalizedNews>? LocalizedNews { get; set; }

    public News(string title, string description, string? body, string? subTitle, string? seoTitle, string? socialTitle, string? cultureCode)
    {
        if(string.IsNullOrWhiteSpace(cultureCode))
            cultureCode = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower();

        DefaultCulture = cultureCode;
        // this.slug = this.Id.ToString();
        this.Update(title, description, body, subTitle, seoTitle, socialTitle, cultureCode);
    }

    public News Update(string? title, string? description, string? body, string? subTitle, string? seoTitle, string? socialTitle, string? cultureCode)
    {
        if (string.IsNullOrWhiteSpace(cultureCode))
            cultureCode = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower();

        if (title is not null && Title?.HasTraslationEquale(cultureCode,title) is not true)
            this.Title.AddOrUpdate( cultureCode,title);

        if (description is not null && Description?.HasTraslationEquale(cultureCode, description) is not true)
            this.Description.AddOrUpdate(cultureCode, description);
        if (subTitle is not null && SubTitle?.HasTraslationEquale(cultureCode, subTitle) is not true)
            this.SubTitle.AddOrUpdate(cultureCode, subTitle);
        if (seoTitle is not null && SEOTitle?.HasTraslationEquale(cultureCode, seoTitle) is not true)
            this.SEOTitle.AddOrUpdate(cultureCode, seoTitle);
        if (socialTitle is not null && SocialTitle?.HasTraslationEquale(cultureCode, socialTitle) is not true)
            this.SocialTitle.AddOrUpdate(cultureCode, socialTitle);
        if (body is not null && Body?.HasTraslationEquale(cultureCode, body) is not true)
            this.Body.AddOrUpdate(cultureCode, body);

        return this;
    }
    */
}

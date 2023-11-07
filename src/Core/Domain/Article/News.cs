using FSH.WebApi.Domain.Common.Localizations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace FSH.WebApi.Domain.Article;

public class News : AuditableEntity, IAggregateRoot
{
    [MaxLength(150)]
    public string slug { get; set; }

    [MaxLength(250)]
    public string MainImage { get; set; }

    [MaxLength(2)]
    public string DefaultCulture { get; set; }
    public Guid TitleId { get; set; }
    public Guid DescriptionId { get; set; }

    public Guid? SubTitleId { get; set; }
    public Guid? SEOTitleId { get; set; }
    public Guid? SocialTitleId { get; set; }
    public Guid? BodyId { get; set; }

    // [ForeignKey(nameof(TitleId))]

    public virtual LocalizationSet Title { get; set; } // = new LocalizationSet();
    // [ForeignKey(nameof(DescriptionId))]
    public virtual LocalizationSet Description { get; set; } = new LocalizationSet();

    public virtual LocalizationSet? SubTitle { get; set; }
    public virtual LocalizationSet? SEOTitle { get; set; }
    public virtual LocalizationSet? SocialTitle { get; set; }
    public virtual LocalizationSet? Body { get; set; }

    public News(){}
    public News(string title, string description, string? body, string? subTitle, string? seoTitle, string? socialTitle, string? cultureCode)
    {
        if(string.IsNullOrWhiteSpace(cultureCode))
            cultureCode = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower();

        this.slug = this.Id.ToString();
        this.Update(title, description, body, subTitle, seoTitle, socialTitle, cultureCode);
    }

    public News Update(string? title, string? description, string? body, string? subTitle, string? seoTitle, string? socialTitle, string? cultureCode)
    {
        if (string.IsNullOrWhiteSpace(cultureCode))
            cultureCode = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower();

        if(title is not null && Title?.HasTraslationEquale(cultureCode,title) is not true)
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
}

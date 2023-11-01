using FSH.WebApi.Domain.Common.Localizations;
using System.ComponentModel.DataAnnotations;

namespace FSH.WebApi.Domain.Article;

public class News : AuditableEntity, IAggregateRoot
{
    [MaxLength(150)]
    public string slug { get; set; }

    [MaxLength(250)]
    public string MainImage { get; set; }

    public virtual LocalizationSet Title { get; set; } = new LocalizationSet();
    public virtual LocalizationSet Description { get; set; } = new LocalizationSet();

    public virtual LocalizationSet? SubTitle { get; set; }
    public virtual LocalizationSet? SEOTitle { get; set; }
    public virtual LocalizationSet? SocialTitle { get; set; }
    public virtual LocalizationSet? Body { get; set; }
    


    public News(){}
    public News(string cultureCode, string title, string? body)
    {
        this.Title.Localizations.Add(
            new Localization()
            {
                CultureCode = cultureCode,
                Value = title
            });
        this.slug = this.Id.ToString();
    }
}

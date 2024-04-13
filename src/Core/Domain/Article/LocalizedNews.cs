using FSH.WebApi.Domain.Medias.Videos;
using FSH.WebApi.Shared.Localizations;
using MassTransit;

namespace FSH.WebApi.Domain.Article;
public class LocalizedNews : AuditableLocalizedEntity , IAggregateRoot
{
    private LocalizedNews()
    {
    }

    // static ILocalizableEntity ILocalizableEntity.Create => throw new NotImplementedException();

    public Guid NewsId { get; set; }

    // public virtual News News { get; set; }
    public string CulturCode { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? SubTitle { get; set; } = string.Empty;
    public string? SEOTitle { get; set; } = string.Empty;
    public string? SocialTitle { get; set; } = string.Empty;
    public string? Body { get; set; } = string.Empty;

    public static LocalizedNews Create(Guid newsId, string cultureCode, string title, string description, string? subTitle, string? seoTitle, string? socialTitle, string? body)
    {
        return new LocalizedNews
        {
            NewsId = newsId,
            CulturCode = cultureCode,
            Title = title,
            Description = description,
            SubTitle = subTitle,
            SEOTitle = seoTitle,
            SocialTitle = socialTitle,
            Body = body
        };
    }

    public override AuditableLocalizedEntity<DefaultIdType> Create(string cultureCode, bool enabled = false, bool isDefault = false)
    {
        return new LocalizedNews()
        {
            CulturCode = cultureCode,
            Enabled = enabled,
            IsDefault = isDefault,
        };
    }

    public LocalizedNews Update(string? title, string? description, string? subTitle, string? seoTitle, string? socialTitle, string? body)
    {
        if (title is not null && Title.Equals(title) is not true) Title = title;
        if (description is not null && Description.Equals(description) is not true) Description = description;
        if (subTitle is not null && SubTitle.Equals(subTitle) is not true) SubTitle = subTitle;
        if (seoTitle is not null && SEOTitle.Equals(seoTitle) is not true) SEOTitle = seoTitle;
        if (socialTitle is not null && SocialTitle.Equals(socialTitle) is not true) SocialTitle = socialTitle;
        if (body is not null && Body.Equals(body) is not true) Body = body;

        return this;
    }
}

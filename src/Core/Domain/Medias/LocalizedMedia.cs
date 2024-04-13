using FSH.WebApi.Domain.Article;
using FSH.WebApi.Domain.Medias.Videos;
using FSH.WebApi.Shared.Localizations;

namespace FSH.WebApi.Domain.Medias;
public class LocalizedMedia : AuditableLocalizedEntity
{
    protected LocalizedMedia()
    {
    }

    public DefaultIdType MediaId { get; set; }
    public string CulturCode { get; set; } = string.Empty;
    public string TypeName { get; set; }
    public string? Title { get; set; } = string.Empty;
    public string? Alt { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;

    public static LocalizedMedia Create()
    {
        return new LocalizedMedia();
    }

    public override AuditableLocalizedEntity<DefaultIdType> Create(string cultureCode, bool enabled = false, bool isDefault = false)
    {
        return new LocalizedMedia()
        {
            CulturCode = cultureCode,
            Enabled = enabled,
            IsDefault = isDefault,
        };
    }

    // public virtual static LocalizedMedia Create(string cultureCode, string title, string alt, string description)
    // {
    //     LocalizedMedia media = new LocalizedMedia {
    //         CulturCode = cultureCode,
    //         Title = title,
    //         Alt = alt,
    //         Description = description
    //     };
    //     return media;
    // }
    // public LocalizedMedia Update(string cultureCode,)
    // {
    //     LocalizedMedia media = new LocalizedMedia
    //     {
    //     };
    //     return media;
    // }
}

using FSH.WebApi.Shared.Localizations;

namespace FSH.WebApi.Domain.Keywords;
public sealed class LocalizedKeyword : AuditableLocalizedEntity
{
    private LocalizedKeyword()
    {
    }

    public Guid KeywordId { get; private set; }
    // public string CulturCode { get; set; } = string.Empty;
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; } = string.Empty;

    public static LocalizedKeyword Create(string cultureCode, string title, string? description, bool? enabled, bool? isDefault, Guid? id = null) =>
        new()
        {
            CulturCode = cultureCode,
            Title = title,
            Description = description,
            Enabled = enabled ?? false,
            IsDefault = isDefault ?? false,
            Id = id ?? Guid.Empty,
        };


    public override AuditableLocalizedEntity<DefaultIdType> Create(String cultureCode, bool enabled = false, bool isDefault = false)
    {
        return new LocalizedKeyword()
        {
            CulturCode = cultureCode,
            Enabled = enabled,
            IsDefault = isDefault,
        };
    }

    public LocalizedKeyword Update(string? title, string? description, bool? enabled, bool? isDefault)
    {
        if (title is not null && Title.Equals(title) is not true) Title = title;
        if (description is not null && Description.Equals(description) is not true) Description = description;
        if (enabled is not null && Enabled.Equals(enabled) is not true) Enabled = enabled ?? false;
        if (isDefault is not null && IsDefault.Equals(isDefault) is not true) IsDefault = isDefault ?? false;
        return this;
    }
}
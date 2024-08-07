﻿using FSH.WebApi.Domain.Editors;
using FSH.WebApi.Shared.Localizations;

namespace FSH.WebApi.Domain.Article;
public class LocalizedCategory : AuditableLocalizedEntity
{
    private LocalizedCategory() {}
    public Guid CategoryId { get; private set; }

    // TODO: public virtual Category Category { get; set; }

    public string CulturCode { get; set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;


    public static LocalizedCategory Create(Guid categoryId, string cultureCode, string name, string description, bool enabled=true, bool isDefault = false)
    {
        return new LocalizedCategory()
        {
            CategoryId = categoryId,
            CulturCode = cultureCode,
            Name = name,
            Description = description,
            Enabled = enabled,
            IsDefault = isDefault
        };
    }

    public override AuditableLocalizedEntity<DefaultIdType> Create(string cultureCode, bool enabled = false, bool isDefault = false)
    {
        return new LocalizedCategory()
        {
            CulturCode = cultureCode,
            Enabled = enabled,
            IsDefault = isDefault,
        };
    }

    public LocalizedCategory Update(string? name, string? description,bool enabled=true, bool isDefault=false)
    {
        if (name is not null && Name.Equals(name) is not true) Name = name;
        if (description is not null && Description.Equals(description) is not true) Description = description;
        if (Enabled.Equals(enabled) is not true) Enabled = enabled;
        if (IsDefault.Equals(isDefault) is not true) IsDefault = isDefault;
        return this;
    }

}

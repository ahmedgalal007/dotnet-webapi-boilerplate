using FSH.WebApi.Domain.Common.Localizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Keywords;
public class LocalizedKeyword : AuditableEntity, ILocalizableEntity
{
    private LocalizedKeyword()
    {
    }

    public Guid KeywordId { get; private set; }
    public string CulturCode { get; private set; } = string.Empty;
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public bool Enabled { get; private set; } = false;
    public bool IsDefault { get; private set; } = false;


    public static LocalizedKeyword Create(string cultureCode, string title, string description, bool? enabled, bool? isDefault) =>
        new LocalizedKeyword() {
            CulturCode = cultureCode,
            Title = title,
            Description = description,
            Enabled = enabled ?? false,
            IsDefault = isDefault ?? false,
        };
    public LocalizedKeyword Update(string? title, string? description, bool? enabled, bool? isDefault)
    {
        if (title is not null && Title.Equals(title) is not true) Title = title;
        if (description is not null && Description.Equals(description) is not true) Description = description;
        if (enabled is not null && Enabled.Equals(enabled) is not true) Enabled = enabled ?? false;
        if (isDefault is not null && IsDefault.Equals(isDefault) is not true) IsDefault = isDefault ?? false;
        return this;
    }
}
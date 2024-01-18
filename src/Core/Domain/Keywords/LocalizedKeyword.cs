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

    public static LocalizedKeyword Create(string cultureCode, string title, string description) =>
        new LocalizedKeyword() {
            CulturCode = cultureCode,
            Title = title,
            Description = description
        };
    public LocalizedKeyword Update(string? title, string? description)
    {
        if (title is not null && Title.Equals(title) is not true) Title = title;
        if (description is not null && Description.Equals(description) is not true) Description = description;
        return this;
    }
}
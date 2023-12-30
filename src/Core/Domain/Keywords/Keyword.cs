using FSH.WebApi.Domain.Common.Localizations;
using System;
using System.ComponentModel.DataAnnotations;

namespace FSH.WebApi.Domain.Keywords;

public class Keyword : LocalizedEntity<LocalizedKeyword>, IAggregateRoot
{

    public Keyword() { }
    public Keyword(string name, string? description, string? color, string? cultureCode)
    {
        if (string.IsNullOrWhiteSpace(cultureCode))
            cultureCode = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower();

        AddOrUpdateLocal(name,/* description,*/ cultureCode);
        this.Slug = name.TrimStart().TrimEnd().Replace(" ", "-");
        this.Color = color;
    }

    public string Slug { get; set; }
    public string? Color { get; set; }

    public Keyword Update(string? name, /*string? description,*/ string? color, string? cultureCode)
    {
        if (string.IsNullOrWhiteSpace(cultureCode))
            cultureCode = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower();
        if (color is not null && Color.Equals(color) is not true) Color = color;
        AddOrUpdateLocal(name, /*description,*/ cultureCode);
        return this;
    }

    public LocalizedKeyword AddOrUpdateLocal(string? name, /*string? description,*/ string? cultureCode)
    {
        LocalizedKeyword localizedKeyword = LocalFactory(cultureCode);

        if (name is not null && localizedKeyword.Name.Equals(name) is not true) localizedKeyword.Name = name;
        // if (description is not null && localizedKeyword.Description.Equals(description) is not true) localizedKeyword.Description = description;

        return localizedKeyword;
    }
}

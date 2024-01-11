using FSH.WebApi.Domain.Common.Localizations;
using System;
using System.ComponentModel.DataAnnotations;

namespace FSH.WebApi.Domain.Keywords;

public class Keyword : LocalizedEntity<LocalizedKeyword>, IAggregateRoot
{

    public Keyword() { }
    public Keyword (string name, string? cultureCode, string? description, string? color)
    {
        if (string.IsNullOrWhiteSpace(cultureCode))
            cultureCode = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower();

        AddOrUpdateLocal(name, description, cultureCode);
        Slug = name.TrimStart().TrimEnd().Replace(" ", "-");
        Color = color;
    }

    public string? Slug { get; set; }
    public string? Color { get; set; }

    public bool? IsCreativeWork { get; set; } = false;
    public bool? IsEvent { get; set; } = false;
    public bool? IsOrganization { get; set; } = false;
    public bool? IsPerson { get; set; } = false;
    public bool? IsPlace { get; set; } = false;
    public bool? IsProduct { get; set; } = false;

    public Keyword Update(string? title, string? description, string? color, string? cultureCode)
    {
        if (string.IsNullOrWhiteSpace(cultureCode))
            cultureCode = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower();
        if (color is not null && Color.Equals(color) is not true) Color = color;
        AddOrUpdateLocal(title, description, cultureCode);
        return this;
    }

    public LocalizedKeyword AddOrUpdateLocal(string? title, string? description, string? cultureCode)
    {
        LocalizedKeyword localizedKeyword = LocalFactory(cultureCode);

        if (title is not null && localizedKeyword.Title.Equals(title) is not true) localizedKeyword.Title = title;
        if (description is not null && localizedKeyword.Description.Equals(description) is not true) localizedKeyword.Description = description;

        return localizedKeyword;
    }
}

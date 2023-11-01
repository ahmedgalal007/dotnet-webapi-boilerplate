using FSH.WebApi.Domain.Common.Localizations;
using System;
using System.ComponentModel.DataAnnotations;

namespace FSH.WebApi.Domain.Article;

public class Category : AuditableEntity, IAggregateRoot
{
    [MaxLength(50)]
    public string Slug { get; set; }
    [MaxLength(10)]
    public string? Color { get; set; }
    public int? ParentId { get; set; }


    public virtual Category? Parent { get; set; }
    public virtual LocalizationSet? Name { get; set; }
    public virtual LocalizationSet? Description { get; set; }

    public Category(){}
    public Category(string name, string? description, string? color, string cultureCode = "ar")
    {
        this.Name.Localizations.Add(new Localization()
        {
            CultureCode = cultureCode,
            Value = name
        });
        this.Description.Localizations.Add(new Localization()
        {
            CultureCode = cultureCode,
            Value = description ?? string.Empty
        });
        this.Slug = name.TrimStart().TrimEnd().Replace(" ", "-");
        this.Color = color;
    }

    public Category Update(string? cultureCode, string? name, string? description, string? color)
    {
        if (name is not null && Name.Localizations.Any(e => e.CultureCode == cultureCode) is not true)
        {
            Name.Localizations.Add(new Localization()
            {
                CultureCode = cultureCode!,
                Value = name
            });
        }

        if (name is not null && Name.Localizations.Where(e => e.CultureCode == cultureCode).FirstOrDefault().Value.Equals(name) is not true) Name.Localizations.Where(e => e.CultureCode == cultureCode).FirstOrDefault().Value = name;
        if (description is not null && Description.Localizations.Where(e => e.CultureCode == cultureCode).FirstOrDefault().Value.Equals(description) is not true) Description.Localizations.Where(e => e.CultureCode == cultureCode).FirstOrDefault().Value = description;
        if (color is not null && Color?.Equals(color) is not true) Color = color;
        return this;
    }
}

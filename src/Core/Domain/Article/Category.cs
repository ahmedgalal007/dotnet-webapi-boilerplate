using FSH.WebApi.Domain.Common.Localizations;
using System;
using System.ComponentModel.DataAnnotations;

namespace FSH.WebApi.Domain.Article;

public class Category : LocalizedEntity<LocalizedCategory>, IAggregateRoot
{
    [MaxLength(50)]
    public string Slug { get; set; }
    [MaxLength(10)]
    public string? Color { get; set; }
    public int? ParentId { get; set; }


    public virtual Category? Parent { get; set; }

    public Category(){}
    public Category(string name, string? description, string? color, string? cultureCode)
    {
        if (string.IsNullOrWhiteSpace(cultureCode))
            cultureCode = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower();

        AddOrUpdateLocal(name, description, cultureCode);
        this.Slug = name.TrimStart().TrimEnd().Replace(" ", "-");
        this.Color = color;
    }

    public Category Update(string? name, string? description, string? color, string? cultureCode)
    {
        if (string.IsNullOrWhiteSpace(cultureCode))
            cultureCode = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower();
        AddOrUpdateLocal(name, description, cultureCode);
        return this;
    }

    public LocalizedCategory AddOrUpdateLocal(string? name, string? description, string? cultureCode)
    {
        LocalizedCategory localizedCategory = LocalFactory(cultureCode);

        if (name is not null && localizedCategory.Name.Equals(name) is not true) localizedCategory.Name = name;
        if (description is not null && localizedCategory.Description.Equals(description) is not true) localizedCategory.Description = description;


        return localizedCategory;
    }
}

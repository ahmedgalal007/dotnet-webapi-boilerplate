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
    public Category(string name, string? description, string? color, string? cultureCode)
    {
        if (string.IsNullOrWhiteSpace(cultureCode))
            cultureCode = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower();
        Name.AddOrUpdate(cultureCode, name);
        Description.AddOrUpdate(cultureCode, description);
        this.Slug = name.TrimStart().TrimEnd().Replace(" ", "-");
        this.Color = color;
    }

    public Category Update(string? cultureCode, string? name, string? description, string? color)
    {
        if (string.IsNullOrWhiteSpace(cultureCode))
            cultureCode = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower();
        if (name is not null && Name.HasTraslationEquale(cultureCode, name) is not true) Name.AddOrUpdate(cultureCode, name);
        if (description is not null && Description.HasTraslationEquale( cultureCode, description) is not true) Description.AddOrUpdate(cultureCode, description);
        if (color is not null && Color?.Equals(color) is not true) Color = color;
        return this;
    }
}

using FSH.WebApi.Domain.Article;
using FSH.WebApi.Domain.Medias;
using System;
using System.Drawing;

namespace FSH.WebApi.Domain.Keywords;

public class Keyword : LocalizedEntity<LocalizedKeyword>, IAggregateRoot
{

    private Keyword()
    {
    }

    public static Keyword Create(string cultureCode, string name, string? description, string? color)
    {
        Keyword instance = new Keyword
        {
            Slug = name.TrimStart().TrimEnd().Replace(" ", "-"),
            Color = color,
        };
        instance.AddOrUpdateLocal(cultureCode, name, description);
        return instance;
    }

    public string? Slug { get; set; }
    public string? Color { get; set; }

    public bool? IsCreativeWork { get; set; } = false;
    public bool? IsEvent { get; set; } = false;
    public bool? IsOrganization { get; set; } = false;
    public bool? IsPerson { get; set; } = false;
    public bool? IsPlace { get; set; } = false;
    public bool? IsProduct { get; set; } = false;
    public virtual IEnumerable<News>? News { get; set; } = default;
    public virtual IEnumerable<Media>? Medias { get; set; } = default;
    public virtual IEnumerable<Album>? Albums { get; set; }
    public virtual IEnumerable<KeywordSchema>? Schemas { get; set; }

    public Keyword Update(string cultureCode, string? title, string? description, string? color)
    {
        if (color is not null && Color.Equals(color) is not true) Color = color;
        AddOrUpdateLocal(cultureCode, title, description);
        return this;
    }

    public LocalizedKeyword AddOrUpdateLocal(string cultureCode, string? title, string? description)
    {
        LocalizedKeyword localizedKeyword = GetLocal(cultureCode)
            ?? LocalizedKeyword.Create(cultureCode, title, description);
        // localizedKeyword.Update(title, description);
        return localizedKeyword;
    }

    protected override LocalizedKeyword CreateLocal(string cultureCode)
    {
        return LocalizedKeyword.Create(cultureCode, string.Empty, string.Empty);
    }
}

using FSH.WebApi.Domain.Article;
using FSH.WebApi.Domain.Medias;
using FSH.WebApi.Domain.Schemas.Things;
using System;
using System.Drawing;

namespace FSH.WebApi.Domain.Keywords;

public class Keyword : LocalizedEntity<LocalizedKeyword>, IAggregateRoot
{

    private Keyword()
    {
    }

    public static Keyword Create(string cultureCode,string languages, ICollection<LocalizedKeyword>? locals, bool? isCreativeWork, bool? isEvent, bool? isOrganization, bool? isPerson, bool? isPlace, bool? isProduct, string? color="")
    {
        LocalizedKeyword? current = locals?.FirstOrDefault(e => e.CulturCode == cultureCode);
        Keyword instance = new Keyword
        {
            Slug = current!=null? current?.Title.TrimStart().TrimEnd().Replace(" ", "-") : "",
            DefaultCulturCode = cultureCode,
            Languages = languages,
            IsCreativeWork = isCreativeWork,
            IsEvent = isEvent,
            IsOrganization = isOrganization,
            IsPerson = isPerson,
            IsPlace = isPlace,
            IsProduct = isProduct,
            Color = color,
            Locals = locals ?? new List<LocalizedKeyword>(),
        };

        // instance.AddOrUpdateLocal(cultureCode, title, description);
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

    // public virtual IEnumerable<KeywordSchema>? Schemas { get; set; }

    public Keyword Update(string cultureCode, string languages, ICollection<LocalizedKeyword>? locals, bool? isCreativeWork, bool? isEvent, bool? isOrganization, bool? isPerson, bool? isPlace, bool? isProduct, string? color = "")
    {
        LocalizedKeyword? current = locals?.FirstOrDefault(e => e.CulturCode == cultureCode);

        if (languages is not null && Languages.Equals(languages) is not true) Languages = languages;
        if (isCreativeWork is not null && IsCreativeWork.Equals(isCreativeWork) is not true) IsCreativeWork = isCreativeWork;
        if (isEvent is not null && IsEvent.Equals(isEvent) is not true) IsEvent = isEvent;
        if (isOrganization is not null && IsOrganization.Equals(isOrganization) is not true) IsOrganization = isOrganization;
        if (isPerson is not null && IsPerson.Equals(isPerson) is not true) IsPerson = isPerson;
        if (isPlace is not null && IsPlace.Equals(isPlace) is not true) IsPlace = isPlace;
        if (isProduct is not null && IsProduct.Equals(isProduct) is not true) IsProduct = isProduct;
        if (color is not null && Color.Equals(color) is not true) Color = color;
        // if (locals is not null && Locals.Equals(locals) is not true) Locals = locals;

        // AddOrUpdateLocal(cultureCode, title, description);
        return this;
    }

    public LocalizedKeyword AddOrUpdateLocal(string cultureCode, string? title, string? description, bool? enabled, bool? isDefault)
    {
        LocalizedKeyword localizedKeyword = GetLocal(cultureCode)
            ?? LocalizedKeyword.Create(cultureCode, title, description, enabled, isDefault);

        localizedKeyword.Update(title, description, enabled, isDefault);

        AddLanguage(cultureCode);

        return localizedKeyword;
    }

    //protected LocalizedKeyword CreateLocal<Dto>(String cultureCode, Dto? dto)
    //{
    //    return LocalizedKeyword.Create(cultureCode,string.Empty, string.Empty, false, false);
    //}


    //protected override LocalizedKeyword CreateLocal<>(string cultureCode)
    //{
    //    return LocalizedKeyword.Create(cultureCode, string.Empty, string.Empty, false, false);
    //}


}

﻿using FSH.WebApi.Domain.Article;
using FSH.WebApi.Domain.Medias;
using FSH.WebApi.Domain.Schemas.Things;
using System;
using System.Drawing;

namespace FSH.WebApi.Domain.Keywords;

public class Keyword : LocalizedEntity<LocalizedKeyword>, IAggregateRoot
{
    public Keyword()
    {
    }

    public string? Slug { get; set; } = string.Empty;
    public string? Color { get; set; } = string.Empty;

    public bool? IsCreativeWork { get; set; } = false;
    public bool? IsEvent { get; set; } = false;
    public bool? IsOrganization { get; set; } = false;
    public bool? IsPerson { get; set; } = false;
    public bool? IsPlace { get; set; } = false;
    public bool? IsProduct { get; set; } = false;
    public virtual IEnumerable<News>? News { get; set; } = new List<News>();
    public virtual IEnumerable<Media>? Medias { get; set; } = new List<Media>();
    public virtual IEnumerable<Album>? Albums { get; set; } = new List<Album>();

    // public virtual IEnumerable<KeywordSchema>? Schemas { get; set; }

    public static Keyword Create(string cultureCode, string languages, ICollection<LocalizedKeyword>? locals, bool? isCreativeWork, bool? isEvent, bool? isOrganization, bool? isPerson, bool? isPlace, bool? isProduct, string? color = "")
    {
        LocalizedKeyword? current = locals?.FirstOrDefault(e => e.CulturCode == cultureCode);
        Keyword instance = new Keyword
        {
            Slug = current != null ? current?.Title.TrimStart().TrimEnd().Replace(" ", "-") : string.Empty,
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
        UpdateLocals(locals);
        // if (locals is not null && Locals.Equals(locals) is not true) Locals = locals;
        // AddOrUpdateLocal(cultureCode, title, description);
        return this;
    }

    public override void UpdateLocals(ICollection<LocalizedKeyword> list)
    {
        base.UpdateLocals(list);
        foreach (var item in list.Where(e => e.Id != null || e.Id != default))
        {
            // AddOrUpdateLocal(item);

            UpdateLocalOfKeyword(item);
        }
    }

    public Keyword AddOrUpdateLocal(string cultureCode, string title, string? description, bool? enabled, bool? isDefault)
    {
        LocalizedKeyword localizedKeyword = GetLocal(cultureCode) ?? LocalizedKeyword.Create(cultureCode, title, description, enabled, isDefault);

        AddOrUpdateLocal(localizedKeyword);

        return this;
    }

    public Keyword AddOrUpdateLocal(LocalizedKeyword local)
    {
        bool isCreate = local.Id == Guid.Empty || local.Id == default || GetLocal(local.CulturCode) == null;
        if (isCreate)
        {
            AddLocalToKeyword(local);
        }
        else
        {
            UpdateLocalOfKeyword(local);
        }

        return this;
    }

    public Keyword AddLocalToKeyword(LocalizedKeyword local)
    {
        if (local.Id == Guid.Empty || local.Id == default || GetLocal(local.CulturCode) == null)
        {
            Locals.Add(local);
            AddLanguage(local.CulturCode);

            return this;
        }

        throw new Exception(string.Format("Translation {0} already exists.", local.CulturCode));
    }

    public Keyword UpdateLocalOfKeyword(LocalizedKeyword local)
    {
        LocalizedKeyword? localizedKeyword = GetLocal(local.CulturCode) ?? throw new Exception(string.Format("Teanslation {0} not existing try to Add it.", local.CulturCode));
        localizedKeyword.Update(local.Title, local.Description, local.Enabled, local.IsDefault);
        SetLanguagesFromLocals();

        return this;
    }

    // protected LocalizedKeyword CreateLocal<Dto>(String cultureCode, Dto? dto)
    // {
    //     return LocalizedKeyword.Create(cultureCode,string.Empty, string.Empty, false, false);
    // }

    // protected override LocalizedKeyword CreateLocal<>(string cultureCode)
    // {
    //     return LocalizedKeyword.Create(cultureCode, string.Empty, string.Empty, false, false);
    // }

}
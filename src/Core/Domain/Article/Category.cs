using FSH.WebApi.Domain.Common.Localizations;
using FSH.WebApi.Domain.Keywords;
using FSH.WebApi.Domain.ValueObjects;
using System;
using System.ComponentModel.DataAnnotations;

namespace FSH.WebApi.Domain.Article;

public class Category : LocalizedEntity<LocalizedCategory>, IAggregateRoot
{
    private Category() { }

    public Guid? ParentId { get; private set; }
    public string? Slug { get; private set; }
    public string? Color { get; private set; }
    public int? DisplayOrder { get; private set; } = 0;
    public bool Enabled { get; set; } = true;

    public virtual IEnumerable<Category>? Childrens { get; set; }

    // public virtual Category? Parent { get; set; }

    public virtual IEnumerable<News>? News { get; set; }

    public static Category Create(string cultureCode, string name, string? description, string? color)
    {
        // if (string.IsNullOrWhiteSpace(cultureCode))
        //cultureCode = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower();
        Category category = new Category();
        category.AddOrUpdateLocal(cultureCode, name, description);
        category.DefaultCulturCode = cultureCode;
        category.Slug = name.TrimStart().TrimEnd().Replace(" ", "-");
        category.Color = color;

        return category;
    }

    public Category Update(string? cultureCode, string? name, string? description, string? color, List<LocalizedCategory>? locals)
    {
        // if (string.IsNullOrWhiteSpace(cultureCode))
            // cultureCode = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower();
        if (color is not null && Color.Equals(color) is not true) Color = color;
        AddOrUpdateLocal(cultureCode, name, description);
        return this;
    }

    private LocalizedCategory AddOrUpdateLocal(string? cultureCode, string? name, string? description)
    {
        LocalizedCategory localizedCategory = GetLocal(cultureCode);
        if(localizedCategory is null)
        {
            localizedCategory = LocalizedCategory.Create(Id, cultureCode, name, description);
            Locals.Add(localizedCategory);
        }
        else
        {
            localizedCategory.Update(name, description);
        }

        // return localizedCategory.Update(name, description);
        return localizedCategory;

        // if (name is not null && localizedCategory.Name.Equals(name) is not true) localizedCategory.Name = name;
        // if (description is not null && localizedCategory.Description.Equals(description) is not true) localizedCategory.Description = description;
        // if (cultureCode is not null && localizedCategory.culturCode.Equals(cultureCode) is not true) localizedCategory.culturCode = cultureCode;

        // return localizedCategory;
    }

    public Category AddChildCategory(string cultureCode, string name, string? description, string? color)
    {
        // if (string.IsNullOrWhiteSpace(cultureCode))
        //cultureCode = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower();

        Category category = Create(cultureCode, name, description, color);
        category.ParentId = this.Id;
        return this;
    }

    public override void UpdateLocals(ICollection<LocalizedCategory> list)
    {
        base.UpdateLocals(list);
        foreach (var item in list.Where(e => e.Id != null || e.Id != default))
        {
            // AddOrUpdateLocal(item);

            UpdateLocalOfCategory(item);
        }
    }

    public Category AddOrUpdateLocal(string cultureCode, string name, string? description, bool enabled = true, bool isDefault= false)
    {
        LocalizedCategory localizedCategory = GetLocal(cultureCode) ?? LocalizedCategory.Create(this.Id, cultureCode, name, description, enabled, isDefault);

        AddOrUpdateLocal(localizedCategory);

        return this;
    }

    public Category AddOrUpdateLocal(LocalizedCategory local)
    {
        bool isCreate = local.Id == Guid.Empty || local.Id == default || GetLocal(local.CulturCode) == null;
        if (isCreate)
        {
            AddLocalToCategory(local);
        }
        else
        {
            UpdateLocalOfCategory(local);
        }

        return this;
    }

    public Category AddLocalToCategory(LocalizedCategory local)
    {
        if (local.Id == Guid.Empty || local.Id == default || GetLocal(local.CulturCode) == null)
        {
            Locals.Add(local);
            AddLanguage(local.CulturCode);

            return this;
        }

        throw new Exception(string.Format("Translation {0} already exists.", local.CulturCode));
    }

    public Category UpdateLocalOfCategory(LocalizedCategory local)
    {
        LocalizedCategory? localizedCategory = GetLocal(local.CulturCode) ?? throw new Exception(string.Format("Teanslation {0} not existing try to Add it.", local.CulturCode));
        localizedCategory.Update(local.Name, local.Description, local.Enabled, local.IsDefault);
        SetLanguagesFromLocals();

        return this;
    }
}
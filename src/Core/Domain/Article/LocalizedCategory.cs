using FSH.WebApi.Domain.Common.Localizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Article;
public class LocalizedCategory : AuditableEntity, ILocalizableEntity
{
    private LocalizedCategory() {}
    public Guid CategoryId { get; private set; }

    // TODO: public virtual Category Category { get; set; }

    public string CulturCode { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    public static LocalizedCategory Create(Guid categoryId, string cultureCode, string name, string description)
    {
        return new LocalizedCategory
        {
            CategoryId = categoryId,
            CulturCode = cultureCode,
            Name = name,
            Description = description,
        };
    }

    public LocalizedCategory Update(string? name, string? description)
    {
        if (name is not null && Name.Equals(name) is not true) Name = name;
        if (description is not null && Description.Equals(description) is not true) Description = description;
        return this;
    }

}

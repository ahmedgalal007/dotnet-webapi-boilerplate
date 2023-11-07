using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSH.WebApi.Domain.Common.Localizations;
public class Localization : AuditableEntity
{
    public Guid LocalizationSetId { get; set; }
    [MaxLength(2)]
    public string CultureCode { get; set; }
    public string Value { get; set; }

    public virtual LocalizationSet LocalizationSet { get; set; }
    public virtual Culture Culture { get; set; }
}

namespace FSH.WebApi.Domain.Common.Localizations;
public class LocalizationSet : AuditableEntity
{
    public virtual ICollection<Localization> Localizations { get; set; }
}

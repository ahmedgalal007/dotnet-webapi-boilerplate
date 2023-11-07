namespace FSH.WebApi.Domain.Common.Localizations;
public class LocalizationSet : AuditableEntity
{
    public virtual ICollection<Localization> Localizations { get; set; }


    public string Get(string cultureCode)
    {
        return Localizations.FirstOrDefault(x => x.CultureCode == cultureCode).Value;
    }

    public bool HasTraslation(string cultureCode)
    {
        return Localizations.Any(x => x.CultureCode == cultureCode);
    }

    public bool HasTraslationEquale(string cultureCode, string translation)
    {
        if(!this.HasTraslation(cultureCode)) return false;

        return Localizations.First(x => x.CultureCode == cultureCode).Value.Equals(translation);
    }
    public Localization AddOrUpdate(string cultureCode, string translation)
    {
        Localization? current = Localizations.FirstOrDefault(x => x.CultureCode == cultureCode);
        if (current is null)
        {
            Localization localization = new Localization
            {
                CultureCode = cultureCode,
                Value = translation
            };
            Localizations.Add(localization);
            return localization;
        }
        else
        {
            if (!current.Value.Equals(translation))
            {
                current.Value = translation;
            }

            return current;
        }
    }
}

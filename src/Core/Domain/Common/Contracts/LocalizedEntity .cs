using FSH.WebApi.Domain.Article;
using System.ComponentModel.DataAnnotations;

namespace FSH.WebApi.Domain.Common.Contracts;

public abstract class LocalizedEntity<T> : AuditableEntity
    where T : ILocalizableEntity, new()
{
    [MaxLength(6)]
    public string DefaultCulturCode { get; set; }

    public virtual ICollection<T> Locals { get; set; }
    public T? GetLocal(string cultureCode)
    {
        return Locals.FirstOrDefault(x => x.culturCode == cultureCode);
    }

    public T GetLocal()
    {
        return Locals.FirstOrDefault(x => x.culturCode == this.DefaultCulturCode)!;
    }

    public bool HasLocal(string cultureCode)
    {
        if (Locals is null) return false;
        return Locals.Any(x => x.culturCode == cultureCode);
    }

    public T LocalFactory(string? cultureCode)
    {
        if (string.IsNullOrWhiteSpace(cultureCode))
            cultureCode = DefaultCulturCode; // Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower();

        if (HasLocal(cultureCode))
        {
            return GetLocal(cultureCode)!;
        }
        else
        {
            T localEntity = new T() { culturCode = cultureCode };
            if (Locals is null) Locals = new List<T>();
            this.Locals.Add(localEntity);
            return localEntity;
        }
    }
}
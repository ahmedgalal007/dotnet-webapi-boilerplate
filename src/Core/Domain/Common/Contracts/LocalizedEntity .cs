using FSH.WebApi.Domain.Article;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Diagnostics.CodeAnalysis;

namespace FSH.WebApi.Domain.Common.Contracts;

public abstract class LocalizedEntity<T> : AuditableEntity
    where T : ILocalizableEntity //, new()
{
    protected abstract T CreateLocal(string cultureCode);

    [MaxLength(6)]
    public string DefaultCulturCode { get; protected set; }

    public virtual ICollection<T> Locals { get; set; }

    public T? GetLocal(string cultureCode)
    {
        if (Locals == null || Locals?.Count == 0)
            return default(T);
        return Locals.FirstOrDefault(x => x.CulturCode == cultureCode);
    }

    public T GetLocal()
    {
        return Locals.FirstOrDefault(x => x.CulturCode == this.DefaultCulturCode)!;
    }

    public bool HasLocal(string cultureCode)
    {
        if (Locals is null) return false;
        return Locals.Any(x => x.CulturCode == cultureCode);
    }

    protected TLocal LocalFactory<TLocal>(string cultureCode) where TLocal : T
    {
        // if (string.IsNullOrWhiteSpace(cultureCode))
        //     cultureCode = DefaultCulturCode; // Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower();

        if (HasLocal(cultureCode))
        {
            return (TLocal)GetLocal(cultureCode)!;
        }
        else
        {
            // T localEntity = new T() { /*CulturCode = cultureCode*/ };
            // if (Locals is null) Locals = new List<T>();
            // this.Locals.Add(localEntity);
            // return localEntity;
            TLocal localEntity = (TLocal)CreateLocal(cultureCode);
            Locals ??= new List<T>();
            this.Locals.Add(localEntity);
            return localEntity;
        }
    }
}
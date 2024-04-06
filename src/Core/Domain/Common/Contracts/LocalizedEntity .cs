using FSH.WebApi.Domain.Article;
using FSH.WebApi.Domain.Keywords;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Diagnostics.CodeAnalysis;

namespace FSH.WebApi.Domain.Common.Contracts;

public abstract class LocalizedEntity<T> : AuditableEntity
    where T : ILocalizableEntity // , new()
{
    // protected abstract T CreateLocal(string cultureCode);

    [MaxLength(6)]
    public string DefaultCulturCode { get; protected set; } = string.Empty;
    public string Languages { get; protected set; } = string.Empty;

    public virtual ICollection<T> Locals { get; set; } = new List<T>();

    public T? GetLocal(string cultureCode)
    {
        // if (Locals == null || Locals?.Count == 0)
            // return default(T);
        return Locals.Count > 0 ? Locals.FirstOrDefault(x => x.CulturCode == cultureCode) : default;
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

    public List<string> GetLanguages()
    {
        return Languages.Split(',').ToList();
    }

    public void SetLanguagesFromLocals()
    {
        List<string> langs = Locals.Select(e => e.CulturCode).ToList();
        SetLanguages(langs);
    }
    public string SetLanguages(List<string> languagesList)
    {
        Languages = string.Join(',', languagesList);
        return Languages;
    }

    public void AddLanguage(string cultureCode)
    {
        if (!GetLanguages().Any(e => e == cultureCode))
        {
            List<string> activeLangs = GetLanguages();
            activeLangs.Add(cultureCode);
            SetLanguages(activeLangs);
        }
    }

    public T DeleteLocal(Guid localId, string cultureCode)
    {
        T? localizedEntity= Locals.First(e => e.Id == localId);
        if(localizedEntity.CulturCode != cultureCode ) throw new Exception("Culture Code Not Match with the local");
        this.Locals.Remove(localizedEntity);
        SetLanguagesFromLocals();
        return localizedEntity;
    }

    // protected TLocal LocalFactory<TLocal>(string cultureCode)
    //     where TLocal : T
    // {
    //     // if (string.IsNullOrWhiteSpace(cultureCode))
    //     //     cultureCode = DefaultCulturCode; // Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower();
    //
    //     if (HasLocal(cultureCode))
    //     {
    //         return (TLocal)GetLocal(cultureCode)!;
    //     }
    //     else
    //     {
    //         // T localEntity = new T() { /*CulturCode = cultureCode*/ };
    //         // if (Locals is null) Locals = new List<T>();
    //         // this.Locals.Add(localEntity);
    //         // return localEntity;
    //         TLocal localEntity = (TLocal)CreateLocal(cultureCode);
    //         Locals ??= new List<T>();
    //         this.Locals.Add(localEntity);
    //         return localEntity;
    //     }
    // }
}
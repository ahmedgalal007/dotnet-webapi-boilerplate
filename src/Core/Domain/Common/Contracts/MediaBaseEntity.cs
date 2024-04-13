using FSH.WebApi.Domain.Keywords;
using FSH.WebApi.Shared.Localizations;

namespace FSH.WebApi.Domain.Common.Contracts;
public abstract class MediaBaseEntity<T> : LocalizedEntity<T>
    where T : AuditableLocalizedEntity
{
    public string Url { get; set; }
    public string TypeName { get; set; }
    public Guid FileId { get; set; }

    // public Storage.File File { get; set; }
    public virtual IEnumerable<Keyword> Keywords { get; set; }

    // protected abstract override T CreateLocal(string cultureCode);
}

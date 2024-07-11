using System;
using FSH.WebApi.Domain.Article;
using FSH.WebApi.Domain.Keywords;

namespace FSH.WebApi.Domain.Medias;
public class Media : MediaBaseEntity<LocalizedMedia>, IAggregateRoot
{
    protected Media()
    {
    }

    public virtual IEnumerable<Keyword>? Keywords { get; set; } = new List<Keyword>();
    public virtual IEnumerable<Album>? Albums { get; set; } = new List<Album>();
    public virtual IEnumerable<News>? News { get; set; } = new List<News>();

    protected virtual LocalizedMedia CreateLocal(string cultureCode)
    {
        return LocalizedMedia.Create();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSH.WebApi.Domain.Article;
using FSH.WebApi.Domain.Keywords;
using FSH.WebApi.Domain.Storage;

namespace FSH.WebApi.Domain.Medias;
public class Album : AuditableEntity, IAggregateRoot
{
    public string Title { get; set; }
    public virtual IEnumerable<Media>? Medias { get; set; }
    public virtual IEnumerable<Keyword>? Keywords { get; set; }
    public virtual IEnumerable<News>? News { get; set; }
}

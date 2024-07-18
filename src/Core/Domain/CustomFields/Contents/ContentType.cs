using FSH.WebApi.Domain.CustomFields.Columns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.CustomFields.Contents;
internal class ContentType
{
    public ContentScope Scope { get; set; }
    public List<ParentContentType> Parents { get; set; }
    public virtual List<Column> Columns { get; set; }

    public List<object> OtherResources { get; set; }
    public List<ContentModule> Templates { get; set; }
}

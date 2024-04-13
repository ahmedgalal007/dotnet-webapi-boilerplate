using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Keywords;
public class Schema : AuditableEntity
{
    public string TypeName { get; set; }
    public string Description { get; set; }
    public bool IsPersisted { get; set; } = true;
    public Guid? ParentId { get; set; }
    public List<Schema>? Children { get; set; }
    public List<SchemaProperty>? Properties { get; set; }
    public int DisplayOrder { get; set; }
    public string Template { get; set; }

}

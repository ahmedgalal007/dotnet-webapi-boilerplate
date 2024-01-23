using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Keywords;
public class KeywordSchema : AuditableEntity
{
    public Guid KeywordId { get; set; }
    // public Guid SchemaTypeName { get; set; }
    public string SchemaTypeName { get; set; } = string.Empty;  // From the Schema
    public HashSet<KeywordSchemaProperty>? Properties { get; set; }
}

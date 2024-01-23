using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Keywords;
public class KeywordSchemaProperty : AuditableEntity
{
    public Guid KeywordSchemaId { get; set; }
    // public Guid SchemaPropertyId { get; set; }
    public string? SchemaPropertyName { get; set; } = string.Empty;
    public string? SchemaPropertyType { get; set; } = string.Empty;
    public string PropertyEditor { get; set; } = string.Empty;
    public Guid? PropertyRefValue { get; set; } = Guid.Empty;
    public string? PropertyJsonValue { get; set; } = string.Empty;
    public bool IsSchema { get; set; } = false;
}

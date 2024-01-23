using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Keywords;
public class SchemaProperty
{
    public string Name { get; set; }
    public string? AllowedTypes { get; set; } = string.Empty;
    public bool IsList { get; set; }
    public bool IsRequired { get; set; }
    public bool IsString { get; set; }
    public bool IsUrl{ get; set; }
    public bool IsSchema{ get; set; }
    public Guid SchemaId { get; set; }

}

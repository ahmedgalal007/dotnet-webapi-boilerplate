using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.DynamicSchemas;
public class MetadataEntityProperty
{
    public string Name { get; set; }

    public string Type { get; set; }

    public string ColumnName { get; set; }

    public bool IsNavigation { get; set; }

    public string NavigationType { get; set; } = "Single";
}

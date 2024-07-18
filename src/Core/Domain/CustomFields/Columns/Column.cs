using FSH.WebApi.Domain.CustomFields.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.CustomFields.Columns;
internal class Column
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public FieldType Type { get; set; }
    public string Format { get; set; }
    public string Required { get; set; }
    public string Group { get; set; }
    public string Default { get; set; }
}

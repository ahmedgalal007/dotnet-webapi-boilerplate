using FSH.WebApi.Domain.CustomFields.Fields.Interfaces;

namespace FSH.WebApi.Domain.CustomFields.Fields;

public class Field : ICustomField
{
    public string Name { get; set; }
    public string Value { get; set; }
}
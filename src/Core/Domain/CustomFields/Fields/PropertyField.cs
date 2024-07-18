using FSH.WebApi.Domain.CustomFields.Fields.Interfaces;

namespace FSH.WebApi.Domain.CustomFields.Fields;

public class PropertyField : ICustomFieldProperty
{
    public string Name { get; set; }
    public string Value { get; set; }
    public string DisplayName { get; set; }
    public int MaxLength { get; set; }
    public int DisplaySize { get; set; }
    public Type Type { get; set; }
    public string Default { get; set; }
}
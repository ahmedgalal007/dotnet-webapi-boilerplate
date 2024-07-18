namespace FSH.WebApi.Domain.CustomFields.Fields;
internal class PropertySchema
{
    public string Name { get; set; }
    public FieldType Parent { get; set; }
    public List<PropertyField> Fields { get; set; }
}

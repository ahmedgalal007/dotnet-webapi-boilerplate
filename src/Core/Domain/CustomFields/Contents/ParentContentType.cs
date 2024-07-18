namespace FSH.WebApi.Domain.CustomFields.Contents;

public class ParentContentType
{
    public string ID { get; set; } // Guid
    public string Name { get; set; }
    public string Group { get; set; }
    public string Description { get; set; }
    public string Inherits { get; set; } // bool
    public string Version { get; set; } // int
    public List<FieldRef> FieldRefs { get; set; }
    public DocumentTemplate DocumentTemplate { get; set; }
}
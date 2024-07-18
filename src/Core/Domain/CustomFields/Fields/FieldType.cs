using FSH.WebApi.Domain.CustomFields.Fields.Interfaces;

namespace FSH.WebApi.Domain.CustomFields.Fields;
internal class FieldType : IAggregateRoot,ICustomFieldType
{
    public FieldType(IList<Field> fields, string FieldName)
    {
        // Populate Essential Fields
        Fields = new List<Field> {
            new(){ Name = "TypeName" },
            new(){ Name = "TypeDisplayName" },
            new(){ Name = "InternalType" },
            new(){ Name = "SQLType" },
            new(){ Name = "ParentType" },
            new(){ Name = "UserCreatable" },
            new(){ Name = "Sortable" },
            new(){ Name = "Filterable" },
            new(){ Name = "Hidden" },
            new(){ Name = "Readonly" },
            new(){ Name = "ShowInDisplayForm" },
        };

        foreach (var fld in fields)
        {
            var iField = Fields.Find(e => e.Name == fld.Name);
            if (iField != null)
            {
                iField.Value = fld.Value;
            }
        }
    }

    public readonly List<Field> Fields;
    public RenderPattern HeaderPattern { get; set; }
    public RenderPattern DisplayPattern { get; set; }
    public RenderPattern EditPattern { get; set; }
    public RenderPattern NewPattern { get; set; }
    public RenderPattern PreviewDisplayPattern { get; set; }
    public RenderPattern PreviewEditPattern { get; set; }
    public RenderPattern PreviewNewPattern { get; set; }

    public List<FieldType> Parents { get; set; }
    public List<FieldType> Childs { get; set; }
    public PropertySchema AdditionalColumnSettings { get; set; }

    public List<DomainEvent> DomainEvents => new();

    public virtual void GetValidatedString() { }
    public virtual void Validate() { }
}

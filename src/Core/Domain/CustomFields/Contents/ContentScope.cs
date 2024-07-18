namespace FSH.WebApi.Domain.CustomFields.Contents;

public class ContentScope
{
    public string Name { get; set; }
    public ContentScope Parent { get; set; }
}
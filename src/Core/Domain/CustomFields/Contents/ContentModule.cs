
namespace FSH.WebApi.Domain.CustomFields.Contents;

public class ContentModule
{
    public string Name { get; set; }
    public string Path { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string RootWebOnly { get; set; } = string.Empty;
    public ContentFile File { get; set; }
}
namespace FSH.WebApi.Domain.CustomFields.Contents;

public class DocumentTemplate
{
    private DocumentTemplate() { }

    public DocumentTemplate(string targetName)
    {
        TargetName = targetName;
    }

    public string TargetName { get; set; }
}
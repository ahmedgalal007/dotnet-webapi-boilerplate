using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Editors;
public class LocalizedEditor : AuditableEntity, ILocalizableEntity
{
    private LocalizedEditor()
    {
    }
    public Guid EditorId { get; private set; }

    // TODO: public virtual Category Category { get; set; }

    public string CulturCode { get; private set; } = string.Empty;

    public string? PreName { get; private set; }
    public string FirstName { get; private set; }
    public string SecondName { get; private set; }
    public string? ThirdName { get; private set; }
    public string FourthName { get; private set; }
    public string? PostName { get; private set; }
    public string? Title { get; private set; }
    public string? Description { get; private set; }

    public static LocalizedEditor Create(Guid editorId, string cultureCode, string? preName, string firstName, string secondName, string? thirdName, string? fourthName, string? postName, string? title, string? description)
    {
        return new LocalizedEditor()
        {
            EditorId = editorId,
            CulturCode = cultureCode,
            FirstName = firstName,
            SecondName = secondName,
            ThirdName = thirdName,
            FourthName = fourthName,
            PreName = preName,
            Title = title,
            Description = postName,
        };
    }

    public LocalizedEditor Update(string? preName, string? firstName, string? secondName, string? thirdName, string? fourthName, string? postName, string? title, string? description)
    {
        if (preName is not null && PreName.Equals(preName) is not true) PreName = preName;
        if (firstName is not null && FirstName.Equals(firstName) is not true) FirstName = firstName;
        if (secondName is not null && SecondName.Equals(secondName) is not true) SecondName = secondName;
        if (thirdName is not null && ThirdName.Equals(thirdName) is not true) ThirdName = thirdName;
        if (fourthName is not null && FourthName.Equals(fourthName) is not true) FourthName = fourthName;
        if (postName is not null && PostName.Equals(postName) is not true) PostName = postName;
        if (title is not null && Title.Equals(title) is not true) Title = title;
        if (description is not null && Description.Equals(description) is not true) Description = description;

        return this;
    }
}

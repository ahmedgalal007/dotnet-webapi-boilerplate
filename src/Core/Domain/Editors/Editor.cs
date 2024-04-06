using FSH.WebApi.Domain.Article;
using FSH.WebApi.Domain.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Editors;
public class Editor : LocalizedEntity<LocalizedEditor>, IAggregateRoot
{
    private Editor()
    {
    }

    public Guid? UserId { get; set; }

    public static Editor Create(string cultureCode, Guid? userId, string? preName, string firstName, string secondName, string? thirdName, string? fourthName, string? postName, string? title, string? description)
    {
        Editor editor = new Editor()
        {
            DefaultCulturCode = cultureCode,
            UserId = userId,
        };
        return editor.Update(cultureCode, userId, preName, firstName, secondName, thirdName, fourthName, postName, title, description);
    }

    public Editor Update(string? cultureCode, Guid? userId, string? preName, string firstName, string secondName, string? thirdName, string? fourthName, string? postName, string? title, string? description)
    {
        if(userId is not null && UserId.Equals(userId) is not true) UserId = userId;
        AddOrUpdateLocal(cultureCode, preName, firstName, secondName, thirdName, fourthName, postName, title, description);
        return this;
    }

    private LocalizedEditor AddOrUpdateLocal(string? cultureCode, string? preName, string firstName, string secondName, string? thirdName, string? fourthName, string? postName, string? title, string? description)
    {
        LocalizedEditor localizedEditory = GetLocal(cultureCode);
        if (localizedEditory is null)
        {
            localizedEditory = LocalizedEditor.Create(Id, cultureCode, preName, firstName, secondName, thirdName, fourthName, postName, title, description);
            Locals.Add(localizedEditory);
        }
        else
        {
            localizedEditory.Update(preName, firstName, secondName, thirdName, fourthName, postName, title, description);
        }
        return localizedEditory;
    }

    public Editor UnlinkEditorUser()
    {
        this.UserId = null;
        return this;
    }

    //protected override LocalizedEditor CreateLocal(String cultureCode)
    //{
    //    return LocalizedEditor.Create(Id, cultureCode, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
    //}
}

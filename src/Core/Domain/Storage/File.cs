using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Storage;
public class File : AuditableEntity
{
    public Guid FolderId { get; set; }
    public Folder Folder { get; set; }
    public bool Public { get; set; } = false;
    public string Name { get; set; }
    public string Extention { get; set; }
}

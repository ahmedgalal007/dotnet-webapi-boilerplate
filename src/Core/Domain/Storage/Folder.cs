using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Storage;
public class Folder : AuditableEntity
{
    public Adapter Adapter { get; set; }
    public bool IsRoot { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public string Directory { get; set; }
    public Guid? ParentId { get; set; }
    public Folder Parent { get; set; }
    public bool Public { get; set; } = false;

    public List<Folder> Childrens { get; set; }
    public List<File> Files { get; set; }

}

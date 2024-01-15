using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Storage;
public class StorageItemDto
{
    public Guid? Id { get; set; }
    public string Name { get; set; } = default!;
    public string Extension { get; set; } = default!;
    public string Data { get; set; } = default!;
    public int Size { get; set; } = 0;
    public Guid ParentFolderId { get; set; }
}

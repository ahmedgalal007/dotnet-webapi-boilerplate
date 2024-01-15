using FSH.WebApi.Application.Keywords;
using FSH.WebApi.Application.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Media;
public abstract class MediaBaseDto : IDto
{
    public Guid? Id { get; set; }
    public string Title { get; set; }
    public FileDto File { get; set; }
    public List<KeywordDto> Keywords { get; set; }
}

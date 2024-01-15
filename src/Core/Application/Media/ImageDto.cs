using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Media;
public class ImageDto : MediaBaseDto
{
    public int? Width { get; set; } = 0;
    public int? Height { get; set; } = 0;
}

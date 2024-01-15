using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Medias.Images;
public class Video : Media, IAggregateRoot
{
    public bool? IsExternal { get; set; }
    public bool? IsYoutube { get; set; }
    /// <summary>
    /// It's the video thumbnail. <Image></Image>
    /// </summary>
    public Guid? VideoImageId { get; set; }
    public Image? VideoImage { get; set; }
}

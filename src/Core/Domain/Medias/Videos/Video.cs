using FSH.WebApi.Domain.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Medias.Videos;
public class Video : Media, IAggregateRoot
{
    private Video()
    {
    }
    public bool? IsExternal { get; set; }
    public bool? IsYoutube { get; set; }
    /// <summary>
    /// It's the video thumbnail. <Image></Image>
    /// </summary>
    public Guid? VideoImageId { get; set; }
    public Video? VideoImage { get; set; }

    public static Video Create(Guid fileId, string culturCode,string title, string alt, string description, string videoTitle,Guid videoImageId, bool isExternal=false, bool isYoutube=false)
    {
        var video = new Video {
            FileId = fileId,
            DefaultCulturCode = culturCode,
            VideoImageId = videoImageId,
            IsExternal = isExternal,
            IsYoutube = isYoutube

        };
        video.AddOrUpdateLocal(culturCode, title, alt, description, videoTitle);
        return video;
    }

    public Video Update(string culturCode, string title, string alt, string description, string videoTitle, Guid videoImageId, bool isExternal = false, bool isYoutube = false)
    {
        if (videoImageId == Guid.Empty && VideoImageId.Equals(videoImageId) is not true) VideoImageId = videoImageId;
        if (IsExternal.Equals(isExternal) is not true) IsExternal = isExternal;
        if (IsYoutube.Equals(isYoutube) is not true) IsYoutube = isYoutube;

        AddOrUpdateLocal(culturCode, title, alt, description, videoTitle);
        return this;
    }

    protected override LocalizedVideo CreateLocal(string cultureCode)
    {
        return LocalizedVideo.Create(this.Id, cultureCode, string.Empty, string.Empty, string.Empty, string.Empty);
    }

    private LocalizedVideo AddOrUpdateLocal(string cultureCode, string? title, string? alt, string? description, string? videoTitle)
    {
        LocalizedVideo localizedVideo = (LocalizedVideo?)GetLocal(cultureCode) ?? CreateLocal(cultureCode);
        return localizedVideo.Update(title, alt, description, videoTitle);
    }
}

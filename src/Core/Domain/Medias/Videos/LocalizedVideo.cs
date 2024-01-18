using FSH.WebApi.Domain.Common.Localizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Medias.Videos;
public class LocalizedVideo : LocalizedMedia
{
    private LocalizedVideo()
    {
    }

    public string? VideoTitle { get; set; } = string.Empty;
    public static LocalizedVideo Create(Guid videoId, string cultureCode, string title, string alt, string description, string videoTitle)
    {
        LocalizedVideo video = new LocalizedVideo
        {
            MediaId = videoId,
            CulturCode = cultureCode,
        };
        return video.Update( title,  alt,  description,  videoTitle);
    }

    public LocalizedVideo Update(string? title, string? alt, string? description, string? videoTitle)
    {
        if(title is not null && Title.Equals(title) is not true) Title = Title;
        if(alt is not null && Alt.Equals(alt) is not true) Alt = alt;
        if(description is not null && Description.Equals(description) is not true) Description = description;
        if(videoTitle is not null && VideoTitle.Equals(videoTitle) is not true) VideoTitle = videoTitle;
        return this;
    }

}

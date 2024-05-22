using Agc.GoodShepherd.Domain.Enums;

namespace Agc.GoodShepherd.Domain.Models;

public class AnnouncementMedia:MediaBase
{
    public string AnnouncementId { get; set; }
    public Announcement Announcement { get; set; }
    public AnnouncementMedia() { }
    
    public AnnouncementMedia(string fileName, string extension, string url, string fileReference, string mimeType, MediaTypes mediaType)
    {
        FileName = fileName;
        Url = url;
        Extension = extension;
        MediaType = mediaType;
        FileReference = fileReference;
        MimeType = mimeType;
    }
}
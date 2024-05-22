using Agc.GoodShepherd.Domain.Enums;

namespace Agc.GoodShepherd.Domain.Models;

public class SermonMedia:MediaBase
{
    public string SermonId { get; set; }
    public Sermon Sermon { get; set; }
    public SermonMedia() { }
    
    public SermonMedia(string fileName, string extension, string url, string fileReference, string mimeType, MediaTypes mediaType)
    {
        FileName = fileName;
        Url = url;
        Extension = extension;
        MediaType = mediaType;
        FileReference = fileReference;
        MimeType = mimeType;
    }
}
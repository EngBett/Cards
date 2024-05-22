using Agc.GoodShepherd.Domain.Enums;

namespace Agc.GoodShepherd.Domain.Models;

public class TukioMedia:MediaBase
{
    public string TukioId { get; set; }
    public Tukio Tukio { get; set; }
    public TukioMedia() { }
    
    public TukioMedia(string fileName, string extension, string url, string fileReference, string mimeType, MediaTypes mediaType)
    {
        FileName = fileName;
        Url = url;
        Extension = extension;
        MediaType = mediaType;
        FileReference = fileReference;
        MimeType = mimeType;
    }
}
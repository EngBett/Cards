using System.ComponentModel.DataAnnotations.Schema;
using Agc.GoodShepherd.Domain.Enums;

namespace Agc.GoodShepherd.Domain.Models;

public class MediaBase:BaseEntity
{
    public string? FileName { get; set; }
    public string Url { get; set; }
    public string Extension { get; set; }
    public string FileReference { get; set; }
    public MediaTypes MediaType { get; set; }

    public string? MimeType { get; set; }

    [NotMapped]
    public string? Base64File { get; set; }
}
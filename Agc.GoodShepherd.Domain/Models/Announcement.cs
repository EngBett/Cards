namespace Agc.GoodShepherd.Domain.Models;

public class Announcement : BaseEntity
{
    public string Title { get; set; }
    public string? SubTitle { get; set; }
    public string Body { get; set; }
    public long Views { get; set; }
    public AnnouncementMedia AnnouncementMedia { get; set; }
}
namespace Agc.GoodShepherd.Application.DisplayModels;

public class AnnouncementDm:BaseDm
{
    public string Title { get; set; }
    public string? SubTitle { get; set; }
    public string Body { get; set; }
    public long Views { get; set; }
    public string ImageUrl { get; set; }
}
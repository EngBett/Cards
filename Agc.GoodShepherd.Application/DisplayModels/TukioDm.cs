namespace Agc.GoodShepherd.Application.DisplayModels;

public class TukioDm:BaseDm
{
    public string Title { get; set; }
    public string? SubTitle { get; set; }
    public string Body { get; set; }
    public long Views { get; set; }
    public string ImageUrl { get; set; }
    public string LocationType { get; set; }
    public string? Location { get; set; }
    public DateTime EventFrom { get; set; }
    public DateTime EventTo { get; set; }
}
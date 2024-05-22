using Agc.GoodShepherd.Domain.Enums;

namespace Agc.GoodShepherd.Domain.Models;

public class Tukio : BaseEntity
{
    public string Title { get; set; }
    public string? SubTitle { get; set; }
    public LocationTypes? LocationType { get; set; }
    public string? Location { get; set; }
    public DateTime EventFrom { get; set; }
    public DateTime EventTo { get; set; }
    public string Body { get; set; }
    public long Views { get; set; }
    public TukioMedia TukioMedia { get; set; }
}
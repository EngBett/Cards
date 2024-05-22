namespace Agc.GoodShepherd.Application.DisplayModels;

public class SermonDm : BaseDm
{
    public string Minister { get; set; }
    public string Title { get; set; }
    public string BibleBook { get; set; }
    public string BibleChapter { get; set; }
    public string BibleVerse { get; set; }
    public string BibleVersion { get; set; }
    public string Body { get; set; }
    public long Views { get; set; }
    public string ImageUrl { get; set; }
    public DateTime SermonDate { get; set; }
}
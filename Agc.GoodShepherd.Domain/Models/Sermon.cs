using Agc.GoodShepherd.Domain.Enums;

namespace Agc.GoodShepherd.Domain.Models;

public class Sermon:BaseEntity
{
    public string Minister { get; set; }
    public string Title { get; set; }
    public string BibleBook { get; set; }
    public string BibleChapter { get; set; }
    public string BibleVerse { get; set; }
    public BibleVersions BibleVersion { get; set; }
    public string Body { get; set; }
    
    public long Views { get; set; }
    public DateTime SermonDate { get; set; }
    public SermonMedia SermonMedia { get; set; }
}
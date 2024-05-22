namespace Agc.GoodShepherd.Domain.Models;

public class Hymn
{
    public int id { get; set; }
    public string title { get; set; }
    public string author { get; set; }
    public string meter { get; set; }
    public List<string> chorus { get; set; }
    public List<string> addedChorus { get; set; }
    public string tuneName { get; set; }
    public List<List<string>> verses { get; set; }
}
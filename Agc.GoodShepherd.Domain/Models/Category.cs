namespace Agc.GoodShepherd.Domain.Models;

public class Category:BaseEntity
{
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public List<Merchandise> Merchandises { get; set; }
}
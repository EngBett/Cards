namespace Agc.GoodShepherd.Domain.Models;

public class Merchandise:BaseEntity
{
    public string ProjectId { get; set; }
    public string CategoryId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public IEnumerable<Tag> Tags { get; set; }
    public int Stock { get; set; }
    public string? Color { get; set; }
    public Category Category { get; set; }
    public Project Project { get; set; }
    
}
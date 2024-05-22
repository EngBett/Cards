namespace Agc.GoodShepherd.Application.DisplayModels;

public class ProjectDm:BaseDm
{
    public string Title { get; set; }
    public string? Subtitle { get; set; }
    public decimal TargetAmount { get; set; }
    public decimal CurrentAmount { get; set; }
    public string Body { get; set; }
    public IEnumerable<CategoryDm> Categories { get; set; }
}
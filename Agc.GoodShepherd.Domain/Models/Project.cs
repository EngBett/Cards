using Agc.GoodShepherd.Domain.Enums;

namespace Agc.GoodShepherd.Domain.Models;

public class Project:BaseEntity
{
    public string Title { get; set; }
    public string? Subtitle { get; set; }
    public decimal TargetAmount { get; set; }
    public decimal CurrentAmount { get; set; }
    public string Body { get; set; }
    public ProjectStatuses Status { get; set; }
    public List<ProjectTransaction> ProjectTransactions { get; set; } = new();
    public List<Merchandise> Merchandises { get; set; } = new();
}
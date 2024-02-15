namespace Cards.Application.DisplayModels;

public abstract class BaseDm
{
    public string Id { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
}
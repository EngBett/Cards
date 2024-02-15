using Cards.Domain.Enums;

namespace Cards.Domain.Models;

public class Card : BaseEntity
{
    public string Name { get; set; }
    public string? Color { get; set; }
    public string? Description { get; set; }
    public CardStatuses Status { get; set; } = CardStatuses.ToDo;
    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }

    public void SetTodoStatus()
    {
        Status = CardStatuses.ToDo;
    }
    
    public void SetInProgressStatus()
    {
        Status = CardStatuses.InProgress;
    }
    
    public void SetDoneStatus()
    {
        Status = CardStatuses.Done;
    }
    
}
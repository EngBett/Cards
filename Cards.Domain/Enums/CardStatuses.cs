using System.ComponentModel;

namespace Cards.Domain.Enums;

public enum CardStatuses
{
    [Description("To Do")] ToDo = 1,
    [Description("In Progress")] InProgress = 2,
    [Description("Done")] Done = 3
}
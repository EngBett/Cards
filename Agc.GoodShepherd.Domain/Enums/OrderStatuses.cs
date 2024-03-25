using System.ComponentModel;

namespace Agc.GoodShepherd.Domain.Enums;

public enum OrderStatuses
{
    [Description("To Do")] ToDo = 1,
    [Description("In Progress")] InProgress = 2,
    [Description("Done")] Done = 3
}
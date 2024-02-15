using Cards.Common.Extensions;
using Cards.Domain.Models;

namespace Cards.Application.DisplayModels;

public class CardDm : BaseDm
{
    public string Name { get; set; }
    public string? Color { get; set; }
    public string? Description { get; set; }
    public string Status { get; set; }
}

public static class CardDto
{
    public static CardDm? ToDto(this Card? card) => card == null
        ? null
        : new CardDm
        {
            Id = card.Id,
            Name = card.Name,
            Color = card.Color,
            Description = card.Description,
            Status = card.Status.GetDescription(),
            DateCreated = card.DateCreated,
            DateUpdated = card.DateUpdated
        };
}
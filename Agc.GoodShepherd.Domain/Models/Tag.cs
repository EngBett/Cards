using Agc.GoodShepherd.Domain.Enums;

namespace Agc.GoodShepherd.Domain.Models;

public class Tag:BaseEntity
{
    public string Name { get; set; }
    public string? Abrv { get; set; }
    public TagTypes TagType { get; set; }
}
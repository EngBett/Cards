namespace Agc.GoodShepherd.Domain.Models;

public class Congregant:BaseEntity
{
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string? IpAddress { get; set; }
}
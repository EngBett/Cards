using Microsoft.AspNetCore.Identity;

namespace Cards.Domain.Models;

public class ApplicationUser:IdentityUser
{
    public IEnumerable<Card> Cards { get; set; }
    public DateTime DateCreated { get; set; }=DateTime.UtcNow;
    public DateTime DateUpdated { get; set; }=DateTime.UtcNow;
}
using Microsoft.AspNetCore.Identity;

namespace Agc.GoodShepherd.Domain.Models;

public class ApplicationUser:IdentityUser
{ 
    public DateTime DateCreated { get; set; }=DateTime.UtcNow;
    public DateTime DateUpdated { get; set; }=DateTime.UtcNow;
}
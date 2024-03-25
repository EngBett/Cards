using Agc.GoodShepherd.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Agc.GoodShepherd.Application.Interfaces;

public interface IAppDbContext
{
    
    DbSet<Congregant> Congregants { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
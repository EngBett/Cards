using Cards.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Cards.Application.Interfaces;

public interface IAppDbContext
{
    DbSet<Card> Cards { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
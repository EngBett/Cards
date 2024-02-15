using Cards.Application.Interfaces;
using Cards.Infrastructure.Extensions;

namespace Cards.Infrastructure.DataAccess;

public class Repository:IRepository
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<T>> SQLQuery<T>(string sql, params object[] parameters) where T : new()
    {
        return await _context.Database.GetModelFromQuery<T>(sql, parameters);
    }
}
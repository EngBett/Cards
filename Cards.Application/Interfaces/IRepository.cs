namespace Cards.Application.Interfaces;

public interface IRepository
{
    Task<IEnumerable<T>> SQLQuery<T>(string sql, params object[] parameters) where T : new();
}
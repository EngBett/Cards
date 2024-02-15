namespace Cards.Application.Interfaces
{
    public interface ICurrentUserService
    {
        string Id { get; }
        string UserName { get; }
    }
}

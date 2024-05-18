using WordFinder.Domain.Entities;

namespace WordFinder.Application.Interface.Persistence
{
    public interface IUserRepository
    {
        Task<User> GetAync(string client);
        Task<bool> InsertAync(User user);
    }
}

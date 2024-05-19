using WordFinder.Domain.Entities;

namespace WordFinder.Application.Interface.Persistence
{
    public interface IUserRepository
    {
        Task<User> GetAsync(string client);
        Task<bool> InsertAync(User user);
    }
}

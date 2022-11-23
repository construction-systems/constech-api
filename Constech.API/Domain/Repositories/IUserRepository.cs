using Constech.API.Domain.Models;

namespace Constech.API.Domain.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> ListAsync();
    Task<User> AddAsync(User user);
    Task<User> FindByIdAsync(Guid id);
    Task<User?> FindByUsernameAsync(string username);
    public bool ExistsByUsername(string username);
    User FindById(int id);
    public Task<User> GetProfile();
    void Update(User user);
    void Remove(User user);
}
using Constech.API.Domain.Models;
using Constech.API.Domain.Repositories;
using Constech.API.Shared.Persistence.Contexts;
using Constech.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Constech.API.Persistence.Repositories;

public class UserRepository : BaseRepository, IUserRepository
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public  UserRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public Task<IEnumerable<User>> ListAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<User> AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync(); 
        return user;
    }

    public async Task<User> FindByIdAsync(Guid id)
    {
        return await _context.Users.FindAsync(id);
    }

    public Task<User?> FindByUsernameAsync(string username)
    {
        return _context.Users.SingleOrDefaultAsync(x => x.Username == username);
    }

    public bool ExistsByUsername(string username)
    {
        return _context.Users.Any(x => x.Username == username);
    }

    public User FindById(int id)
    {
        return _context.Users.Find(id);
    }

    public User GetProfile()
    {
        return _httpContextAccessor.HttpContext.Items["User"] as User;
    }

    public void Update(User user)
    {
        throw new NotImplementedException();
    }

    public void Remove(User user)
    {
        throw new NotImplementedException();
    }
}
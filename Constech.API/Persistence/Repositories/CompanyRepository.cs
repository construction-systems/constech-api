using Constech.API.Domain.Models;
using Constech.API.Domain.Repositories;
using Constech.API.Shared.Persistence.Contexts;
using Constech.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Constech.API.Persistence.Repositories;

public class CompanyRepository : BaseRepository, ICompanyRepository
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CompanyRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Company> ListAsync()
    {
        var user = _httpContextAccessor.HttpContext?.Items["User"] as User;
        return await _context.Companies
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.UserId == user.Id);
    }

    public async Task AddAsync(Company company)
    {
        await _context.Companies.AddAsync(company);
    }

    public Task<Company> FindByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Company> FindByUserIdAsync(Guid userId)
    {
        return await _context.Companies.SingleOrDefaultAsync(x => x.UserId == userId);
    }

    public async Task<Company> GetCurrentCompany()
    {
        var reqUser = _httpContextAccessor.HttpContext.Items["User"] as User;
        return await _context.Companies
            .Include(x => x.Projects)
            .SingleOrDefaultAsync(x => x.UserId == reqUser.Id);
    }

    public void Update(Company company)
    {
        throw new NotImplementedException();
    }

    public void Remove(Company company)
    {
        throw new NotImplementedException();
    }
}
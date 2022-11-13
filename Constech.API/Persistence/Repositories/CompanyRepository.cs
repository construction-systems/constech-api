using Constech.API.Domain.Models;
using Constech.API.Domain.Repositories;
using Constech.API.Shared.Persistence.Contexts;
using Constech.API.Shared.Persistence.Repositories;

namespace Constech.API.Persistence.Repositories;

public class CompanyRepository : BaseRepository, ICompanyRepository
{
    public CompanyRepository(AppDbContext context) : base(context)
    {
    }
    public Task<IEnumerable<Company>> ListAsync()
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(Company company)
    {
        await _context.Companies.AddAsync(company);
    }

    public Task<Company> FindByIdAsync(int id)
    {
        throw new NotImplementedException();
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
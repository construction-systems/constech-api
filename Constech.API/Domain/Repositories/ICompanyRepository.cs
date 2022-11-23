using Constech.API.Domain.Models;

namespace Constech.API.Domain.Repositories;

public interface ICompanyRepository
{
    Task<Company> ListAsync();
    Task AddAsync(Company company);
    Task<Company> FindByIdAsync(int id);
    Task<Company> FindByUserIdAsync(Guid userId);
    Task<Company> GetCurrentCompany();
    void Update(Company company);
    void Remove(Company company);
}
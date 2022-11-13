using Constech.API.Domain.Models;
using Constech.API.Domain.Services.Communication.Response;

namespace Constech.API.Domain.Services;

public interface ICompanyService
{
    Task<CompanyResponse> SaveAsync(Company company);
}
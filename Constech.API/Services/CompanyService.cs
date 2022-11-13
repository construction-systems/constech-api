using Constech.API.Domain.Models;
using Constech.API.Domain.Repositories;
using Constech.API.Domain.Services;
using Constech.API.Domain.Services.Communication.Response;
using Constech.API.Shared.Domain.Repositories;

namespace Constech.API.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CompanyService(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
    {
        _companyRepository = companyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CompanyResponse> SaveAsync(Company company)
    {
        try
        {
            await _companyRepository.AddAsync(company);
            
            await _unitOfWork.CompleteAsync();
            
            return new CompanyResponse(company);
        }
        catch (Exception e)
        {
            return new CompanyResponse($"An error occurred while saving the company: {e.Message}");
        }
    }
}
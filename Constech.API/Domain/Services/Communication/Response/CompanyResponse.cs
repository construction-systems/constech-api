using Constech.API.Shared.Domain.Services.Communication;

namespace Constech.API.Domain.Services.Communication.Response;

public class CompanyResponse : BaseResponse<Models.Company>
{
    public CompanyResponse(string message) : base(message)
    {
    }

    public CompanyResponse(Models.Company resource) : base(resource)
    {
    }
}
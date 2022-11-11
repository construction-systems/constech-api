using Constech.API.Domain.Models;
using Constech.API.Shared.Domain.Services.Communication;

namespace Constech.API.Domain.Services.Communication;

public class ProjectResponse : BaseResponse<Project>
{
    public ProjectResponse(string message) : base(message)
    {
    }

    public ProjectResponse(Project resource) : base(resource)
    {
    }
}
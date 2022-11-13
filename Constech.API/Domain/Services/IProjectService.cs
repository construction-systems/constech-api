using Constech.API.Domain.Models;
using Constech.API.Domain.Services.Communication;
using Constech.API.Domain.Services.Communication.Response;

namespace Constech.API.Domain.Services;

public interface IProjectService
{
    Task<IEnumerable<Project>> ListAsync();
    Task<ProjectResponse> SaveAsync(Project project);
}
using Constech.API.Domain.Models;
using Constech.API.Domain.Services.Communication;

namespace Constech.API.Domain.Services;

public interface IProjectService
{
    Task<IEnumerable<Project>> ListAsync();
    Task<ProjectResponse> SaveAsync(Project project);
}
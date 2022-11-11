using Constech.API.Domain.Models;
using Constech.API.Domain.Services.Communication;
using Constech.API.Resources;

namespace Constech.API.Domain.Services;

public interface IProjectsService
{
    Task<IEnumerable<Project>> ListAsync();
    Task<ProjectResponse> SaveAsync(Project project);
}
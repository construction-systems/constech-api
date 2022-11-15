using Constech.API.Domain.Models;

namespace Constech.API.Domain.Repositories;

public interface IProjectRepository
{
    Task<IEnumerable<Project>> ListAsync();
    Task AddAsync(Project project);
    Task<Project?> FindByIdAsync(Guid projectId);
    void Update(Project project);
    void Remove(Project project);
}
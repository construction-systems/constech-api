using Constech.API.Domain.Models;
using Constech.API.Domain.Repositories;
using Constech.API.Domain.Services;
using Constech.API.Domain.Services.Communication;
using Constech.API.Resources;
using Constech.API.Shared.Domain.Repositories;

namespace Constech.API.Services;

public class ProjectsService : IProjectsService
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public async Task<IEnumerable<Project>> ListAsync()
    {
        return await _projectRepository.ListAsync();
    }

    public async Task<ProjectResponse> SaveAsync(Project project)
    {
        try
        {
            await _projectRepository.AddAsync(project);

            await _unitOfWork.CompleteAsync();

            return new ProjectResponse(project);
        }
        catch (Exception e)
        {
            return new ProjectResponse($"An error occurred while saving the project: {e.Message}");
        }
    }
}
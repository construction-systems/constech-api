using Constech.API.Domain.Models;
using Constech.API.Domain.Repositories;
using Constech.API.Domain.Services;
using Constech.API.Domain.Services.Communication.Response;
using Constech.API.Shared.Domain.Repositories;

namespace Constech.API.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProjectService(IProjectRepository projectRepository, IUnitOfWork unitOfWork)
    {
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
    }

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
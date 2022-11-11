using Constech.API.Domain.Models;
using Constech.API.Domain.Repositories;
using Constech.API.Shared.Persistence.Contexts;
using Constech.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Constech.API.Persistence;

public class ProjectRepository : BaseRepository, IProjectRepository
{
    public ProjectRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Project?>> ListAsync()
    {
        return await _context.Projects
            .ToListAsync();
    }

    public async Task AddAsync(Project? project)
    {
        await _context.Projects.AddAsync(project);
    }

    public async Task<Project?> FindByIdAsync(int projectId)
    {
        return await _context.Projects
            .FirstOrDefaultAsync(p => p != null && p.Id == projectId);
    }

    public void Update(Project project)
    {
        _context.Projects.Update(project);
    }

    public void Remove(Project project)
    {
        _context.Projects.Remove(project);
    }
}
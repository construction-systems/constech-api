using System.Net.Mime;
using AutoMapper;
using Constech.API.Domain.Models;
using Constech.API.Domain.Services;
using Constech.API.Resources;
using Constech.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Constech.API.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Create, read, update and delete Projects")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectsService _projectsService;
    private readonly IMapper _mapper;

    public ProjectsController(IProjectsService projectsService, IMapper mapper)
    {
        _projectsService = projectsService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProjectResource>), 200)]
    public async Task<IEnumerable<ProjectResource>> GetAllAsync()
    {
        var projects = await _projectsService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Project>, IEnumerable<ProjectResource>>(projects);

        return resources;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProjectResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> PostAsync([FromBody] SaveProjectResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var project = _mapper.Map<SaveProjectResource, Project>(resource);
        var result = await _projectsService.SaveAsync(project);

        if (!result.Success)
            return BadRequest(result.Message);

        var projectResource = _mapper.Map<Project, ProjectResource>(result.Resource);

        return Created(nameof(PostAsync), projectResource);
    }
}
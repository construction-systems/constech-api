using System.Net.Mime;
using AutoMapper;
using Constech.API.Authorization.Attributes;
using Constech.API.Domain.Models;
using Constech.API.Domain.Services;
using Constech.API.Resources.Project;
using Constech.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Constech.API.Controllers;

[Authorize]
[ApiController]
[Route("/api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[SwaggerTag("Create, read, update and delete Projects")]
public class ProjectsController : ControllerBase
{
    private readonly ICompanyService _companyService;
    private readonly IMapper _mapper;
    private readonly IProjectService _projectService;
    private readonly IUserService _userService;

    public ProjectsController(IProjectService projectService, IUserService userService, ICompanyService companyService,
        IMapper mapper)
    {
        _userService = userService;
        _companyService = companyService;
        _projectService = projectService;
        _mapper = mapper;
    }

    [Microsoft.AspNetCore.Authorization.AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProjectResource>), 200)]
    public async Task<IEnumerable<ProjectResource>> GetAllAsync()
    {
        var projects = await _projectService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Project>, IEnumerable<ProjectResource>>(projects);
        return resources;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProjectResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> PostAsync([FromBody] SaveProjectResource resource)
    {
        var company = await _companyService.GetCompany();

        if (company == null)
            return BadRequest(new {error = "You must be an owner of a company"});
        resource.CompanyId = company.Id;
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var project = _mapper.Map<SaveProjectResource, Project>(resource);
        var result = await _projectService.SaveAsync(project);
        if (!result.Success)
            return BadRequest(result.Message);
        var projectResource = _mapper.Map<Project, ProjectResource>(result.Resource);

        return Created(nameof(PostAsync), projectResource);
    }
}
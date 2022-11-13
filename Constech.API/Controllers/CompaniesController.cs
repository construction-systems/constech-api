using System.Net.Mime;
using AutoMapper;
using Constech.API.Authorization.Attributes;
using Constech.API.Domain.Models;
using Constech.API.Domain.Services;

using Constech.API.Resources.Company;
using Constech.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Constech.API.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Create, read, update and delete Projects")]
public class CompaniesController : ControllerBase
{
    private readonly ICompanyService _companyService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public CompaniesController(ICompanyService companyService, IMapper mapper, IUserService userService)
    {
        _companyService = companyService;
        _mapper = mapper;
        _userService = userService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(CompanyResource), 200)]
    public async Task<CompanyResource> GetAsync()
    {
        var company = await _companyService.ListAsync();
        return _mapper.Map<Company, CompanyResource>(company);
    }
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveCompanyResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var newUser = await _userService.RegisterAsync(resource.Admin);
        var company = _mapper.Map<SaveCompanyResource, Company>(resource);
        company.UserId = newUser.Id;
        var result = await _companyService.SaveAsync(company);
        
        if (!result.Success)
            return BadRequest(result.Message);
        
        var companyResource = _mapper.Map<Company, CompanyResource>(result.Resource);
        return Created(nameof(PostAsync), new { company = companyResource, token = newUser.Token });
    }
}
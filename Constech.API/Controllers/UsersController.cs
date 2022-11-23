using System.Net.Mime;
using AutoMapper;
using Constech.API.Authorization.Attributes;
using Constech.API.Domain.Models;
using Constech.API.Domain.Services;
using Constech.API.Domain.Services.Communication.Request;
using Constech.API.Resources.Company;
using Constech.API.Resources.User;
using Microsoft.AspNetCore.Mvc;

namespace Constech.API.Controllers;

[Authorize]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[Route("/api/v1/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ICompanyService _companyService;
    private readonly IMapper _mapper;

    public UsersController(IUserService userService, ICompanyService companyService, IMapper mapper)
    {
        _userService = userService;
        _companyService = companyService;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequest request)
    {
        var response = await _userService.Authenticate(request);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var user = await _userService.RegisterAsync(request);
        return Ok(user);
    }

    [HttpGet]
    [HttpGet("profile")]
    public async Task<ActionResult> GetProfile()
    {
        var user = await _userService.GetProfile();
        var company = await _companyService.FindByUserId(user.Id);
        var resource = _mapper.Map<User, UserResource>(user);
        if (company != null)
            resource.Company = _mapper.Map<Company, CompanyResource>(company);
        return Ok(resource);
    }
}
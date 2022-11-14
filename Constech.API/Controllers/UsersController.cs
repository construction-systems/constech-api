using AutoMapper;
using Constech.API.Authorization.Attributes;
using Constech.API.Domain.Models;
using Constech.API.Domain.Services;
using Constech.API.Domain.Services.Communication;
using Constech.API.Domain.Services.Communication.Request;
using Constech.API.Resources.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Constech.API.Controllers;

[Authorize]
[ApiController]
[Route("/api/v1/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
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
    public ActionResult GetProfile()
    {
        var user = _userService.GetProfile();
        var resource = _mapper.Map<User, UserResource>(user);
        return Ok(resource);
    }
}
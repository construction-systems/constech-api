using System.ComponentModel.DataAnnotations;

namespace Constech.API.Domain.Services.Communication.Request;
public class AuthenticateRequest
{
    [Required] 
    public string Username { get; set; }

    [Required] 
    public string Password { get; set; }
}
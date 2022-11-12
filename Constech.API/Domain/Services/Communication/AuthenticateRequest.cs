using System.ComponentModel.DataAnnotations;

namespace Constech.API.Domain.Services.Communication;
public class AuthenticateRequest
{
    [Required] 
    public string Username { get; set; }

    [Required] 
    public string Password { get; set; }
}
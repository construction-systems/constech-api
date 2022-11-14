using System.ComponentModel.DataAnnotations;

namespace Constech.API.Domain.Services.Communication.Request;

public class RegisterRequest
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    [MinLength(5)]
    public string Username { get; set; }
    public string? PhotoUrl { get; set; }
    public string? Occupation { get; set; }
    public string? Bio { get; set; }

    [Required]
    public string Password { get; set; }
}
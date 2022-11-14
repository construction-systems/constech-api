using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Constech.API.Domain.Models;

public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string? PhotoUrl { get; set; }
    public string? Occupation { get; set; }
    public string? Bio { get; set; }
    // Relationships
    public Company? Company { get; set; }
    [JsonIgnore]
    public string Password { get; set; }
}
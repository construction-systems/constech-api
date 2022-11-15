using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Constech.API.Domain.Enums;

namespace Constech.API.Domain.Models;


public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string? PhotoUrl { get; set; }
    public string? Phone { get; set; }
    public string? Occupation { get; set; }
    public string? Bio { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    // Relationships
    [JsonIgnore] 
    public Collection<Follow> FollowingProjects { get; set; } = new();
    public Company? Company { get; set; }
    [JsonIgnore] 
    public Configuration Configuration { get; set; } = new();
    [JsonIgnore]
    public string Password { get; set; }
}
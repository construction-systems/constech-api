using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace Constech.API.Domain.Models;

public class Project
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime CreatedAt { get; set; }

    //! Relationships

    //? Phases
    [JsonIgnore]
    public Collection<Phase> Phases { get; set; } = new();
    //? Threads
    [JsonIgnore]
    public Collection<Thread> Threads { get; set; } = new();
    //? Followers
    [JsonIgnore]
    public Collection<Follow> Followers { get; set; } = new();
    //? Companies
    public int CompanyId { get; set; }
    public Company Company { get; set; }
    
}
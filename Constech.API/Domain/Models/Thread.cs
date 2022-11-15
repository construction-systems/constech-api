

using System.Collections.ObjectModel;

namespace Constech.API.Domain.Models;

public class Thread
{
    public int Id { get; set; }
    public string Body { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    
    
    //? Project
    public Guid ProjectId { get; set; }
    public Project Project { get; set; }
    
    //? Comments
    public Collection<Comment> Comments { get; set; } = new();
}
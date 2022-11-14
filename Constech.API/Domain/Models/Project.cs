using System.Collections.ObjectModel;
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
    public Collection<Phase> Phases { get; set; } = new Collection<Phase>();
    
    //? Companies
    public int CompanyId { get; set; }
    public Company Company { get; set; }
}
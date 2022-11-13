namespace Constech.API.Domain.Models;

public class Project
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    
    //! Relationships
    
    //? Companies
    public int CompanyId { get; set; }
    public Company Company { get; set; }
}
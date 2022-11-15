namespace Constech.API.Domain.Models;

public class Phase
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime DateTime { get; set; } = DateTime.Now;
    
    public Guid ProjectId { get; set; }
    public Project Project { get; set; }
}
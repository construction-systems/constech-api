namespace Constech.API.Resources.Project;

public class ProjectResource
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? DueDate { get; set; }
    public int CompanyId { get; set; }
}
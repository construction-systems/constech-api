using System.Collections.ObjectModel;

namespace Constech.API.Domain.Models;

public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    //! Relationships

    //? Users
    public Guid UserId { get; set; }
    public User User { get; set; }

    //? Projects
    public ICollection<Project> Projects { get; set; } = new Collection<Project>();
}
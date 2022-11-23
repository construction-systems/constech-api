using Constech.API.Resources.Company;

namespace Constech.API.Resources.User;

public class UserResource
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string PhotoUrl { get; set; }
    public string Occupation { get; set; }
    public string Bio { get; set; }
    public CompanyResource Company { get; set; }
}
using Constech.API.Resources.User;

namespace Constech.API.Resources.Company;

public class CompanyResource
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public UserResource User { get; set; }
}
using System.ComponentModel.DataAnnotations;
using Constech.API.Domain.Services.Communication.Request;
using Constech.API.Resources.User;
using Swashbuckle.AspNetCore.Annotations;

namespace Constech.API.Resources.Company;
[SwaggerSchema(Required = new []{ "Title" })]
public class SaveCompanyResource
{
    [SwaggerSchema("Company Title")]
    [Required]
    public string Name { get; set; }
    
    [SwaggerSchema("Company Description")]
    public string Description { get; set; }
    
    [SwaggerSchema("Admin Info")]
    public RegisterRequest Admin { get; set; }
}
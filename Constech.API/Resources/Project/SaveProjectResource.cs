using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Constech.API.Resources.Project;

[SwaggerSchema(Required = new []{ "Title" })]
public class SaveProjectResource
{
    [SwaggerSchema("Project Title")]
    [Required]
    public string Title { get; set; }
    
    [SwaggerSchema("Project Description")]
    public string Description { get; set; }
}
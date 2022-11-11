using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Constech.API.Resources;

[SwaggerSchema(Required = new []{ "Name" })]
public class SaveProjectResource
{
    [SwaggerSchema("Category Name")]
    [Required]
    public string Name { get; set; }
}
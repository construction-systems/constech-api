using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace Constech.API.Controllers;

[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
[Route("/")]
public class HomeController : ControllerBase
{
    public HomeController() { }

    [HttpGet]
    public IDictionary<string, string> Greet()
    {
        return new Dictionary<string, string>
        {
            { "response", "Welcome to the Constech API" },
            { "time", DateTime.Now.ToString("HH:mm:ss tt") }
        };
    }
}
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace MyProject.Controllers.V1 // It's a good practice to use versioned namespaces
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Version 1.0 endpoint.");
        }
    }
}

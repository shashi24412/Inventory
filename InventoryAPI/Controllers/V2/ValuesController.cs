using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace MyProject.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Version 2.0 endpoint (with new features).");
        }
    }
}

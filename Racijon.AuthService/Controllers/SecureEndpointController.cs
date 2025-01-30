using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Racijon.AuthService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SecureEndpointController : ControllerBase
    {
        [Authorize]
        [HttpGet("secure")]
        public IActionResult SecureEndpoint()
        {
            return Ok("Ви маєте доступ до цього ресурсу!");
        }
    }
}
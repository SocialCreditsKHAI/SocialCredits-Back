using Microsoft.AspNetCore.Mvc;

namespace SocialCredits_Back.Controllers
{

    [ApiController]
    [Route("User")]
    public class UserController : ControllerBase
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}

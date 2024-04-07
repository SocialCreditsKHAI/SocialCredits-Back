using Microsoft.AspNetCore.Mvc;
using SocialCredits.Domain.DTO;
using SocialCredits.Domain.ViewModels;
using SocialCredits.Services.Interfaces;

namespace SocialCredits.Controllers
{

    [ApiController]
    [Route("User")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _service;

        public UserController(IUserServices service)
        {
            _service = service;
        }


        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login(UserLoginDTO userLoginDTO)
        {
            var response = await _service.Login(userLoginDTO);

            switch (response.Item1)
            {
                case System.Net.HttpStatusCode.OK:
                    return Ok(response.Item2);
                case System.Net.HttpStatusCode.BadRequest:
                    return BadRequest(response.Item2);
                case System.Net.HttpStatusCode.Unauthorized:
                    return Unauthorized(response.Item2);
                case System.Net.HttpStatusCode.NotFound:
                    return NotFound(response.Item2);
            }
            return Ok();

        }
        [HttpPost]
        [Route("RegistrationImage")]
        public async Task<IActionResult> RegistrationImage(UserRegistrationWithImageViewModel credentials)
        {
            return Ok(await _service.Registration(credentials));
        }
        [HttpPost]
        [Route("RegistrationImageName")]
        public async Task<IActionResult> RegistrationImageName(UserRegistrationWithImageNameViewModel credentials)
        {

            return Ok(await _service.Registration(credentials));
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialCredits.Domain.DTO;
using SocialCredits.Domain.Models;
using SocialCredits.Domain.ViewModels;
using SocialCredits.Services.Interfaces;

namespace SocialCredits.Controllers
{

    [ApiController]
    [Route("User")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _service;
        private readonly IUserAcceptVoteService _userAcceptVoteService;

        public UserController(IUserServices service, IUserAcceptVoteService userAcceptVoteService)
        {
            _service = service;
            _userAcceptVoteService = userAcceptVoteService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(UserLoginDTO userLoginDTO)
        {
            var response = await _service.Login(userLoginDTO);

            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return Ok(response.Message);
                case System.Net.HttpStatusCode.BadRequest:
                    return BadRequest(response.Message);
                case System.Net.HttpStatusCode.Unauthorized:
                    return Unauthorized(response.Message);
                case System.Net.HttpStatusCode.NotFound:
                    return NotFound(response.Message);
            }
            return BadRequest();

        }

        [HttpPost]
        [Route("RegistrationImage")]
        public async Task<IActionResult> RegistrationImage(UserRegistrationWithImageViewModel credentials)
        {
            try
            {
                if (await _service.Registration(credentials))
                {
                    await _userAcceptVoteService.CreateUserAcceptVote(new UserAcceptVote(credentials.Login));
                    return Ok(true);
                }
                else
                {
                    return Ok(false);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        [HttpPost]
        [Route("RegistrationImageName")]
        public async Task<IActionResult> RegistrationImageName(UserRegistrationWithImageNameViewModel credentials)
        {
            try
            {
                if(await _service.Registration(credentials))
                {
                await _userAcceptVoteService.CreateUserAcceptVote(new UserAcceptVote(credentials.Login));
                return Ok(true);
                }
                else
                {
                    return Ok(false);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetUsersList")]
        
        public async Task<IActionResult> GetUsersList()
        {
            return Ok(await _service.GetAllUsersList());
        }
    }
}

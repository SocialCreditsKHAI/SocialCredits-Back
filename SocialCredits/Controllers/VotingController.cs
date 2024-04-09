using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using SocialCredits.Domain.Models;
using SocialCredits.Domain.ViewModels;
using SocialCredits.Services.Interfaces;

namespace SocialCredits_Back.Controllers
{
    [Route("Voting")]
    //[Authorize]
    public class VotingController : ControllerBase
    {
        private readonly IUserAcceptVoteService _userAcceptVoteService;
        private readonly IUserServices _userServices;
        private readonly IMapper _mapper;

        public VotingController(IUserAcceptVoteService userAcceptVoteService, IUserServices userService, IMapper mapper)
        {
            _userAcceptVoteService = userAcceptVoteService;
            _userServices = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllUsersOnVote")]
        public async Task<IActionResult> GetAllUsersOnVote()
        {
            var response = new List<(UserAcceptVote, UserToShowViewModel)>();
            var UsersOnVote = await _userAcceptVoteService.GetUsersOnVote();
            if (UsersOnVote != null)
            {
                foreach (var item in UsersOnVote)
                {
                    var user = await _userServices.GetUserByLogin(item.UserLogin);
                    response.Add((item, _mapper.Map<UserToShowViewModel>(user)));
                }
            }
                return Ok(response.ToJson());
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using SocialCredits.Domain.Enums;
using SocialCredits.Domain.Models;
using SocialCredits.Domain.ViewModels;
using SocialCredits.Services.Interfaces;

namespace SocialCredits_Back.Controllers
{
    [Route("Voting")]
    [Authorize]
    public class VotingController : ControllerBase
    {
        private readonly IUserAcceptVoteService _userAcceptVoteService;
        private readonly IUserServices _userService;
        private readonly IMapper _mapper;

        public VotingController(IUserAcceptVoteService userAcceptVoteService, IUserServices userService, IMapper mapper)
        {
            _userAcceptVoteService = userAcceptVoteService;
            _userService = userService;
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
                    var user = await _userService.GetUserByLogin(item.UserLogin);
                    response.Add((item, _mapper.Map<UserToShowViewModel>(user)));
                }
            }
            return Ok(response.ToJson());
        }
        [HttpPost]
        [Route("VoteForUser")]
        public async Task<IActionResult> VoteForUser([FromBody]VoteForUserViewModel model)
        {
            var userName = User.Claims.FirstOrDefault()!.Value;
            Voter vote = new() { isAccept = model.IsAccept, VoterLogin = userName };
            var usersCount = await _userService.GetUsersCount();
            var voteResult = await _userAcceptVoteService.AddVoter(vote, model.UserVoteForLogin, (int)Math.Ceiling((decimal)usersCount * 40 / 100));
            if (voteResult == VoteStatus.Approved) await _userService.ChangeRoleToUser(model.UserVoteForLogin);
                return Ok(voteResult.ToString());
        }
    }
}

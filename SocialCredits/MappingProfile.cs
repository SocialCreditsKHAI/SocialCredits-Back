using AutoMapper;
using SocialCredits.Domain.Models;
using SocialCredits.Domain.ViewModels;


namespace SocialCredits_Back
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserToShowViewModel>();
        }
    }
}

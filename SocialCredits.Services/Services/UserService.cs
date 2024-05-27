using Amazon.SecurityToken.Model;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SocialCredits.Domain.DTO;
using SocialCredits.Domain.Models;
using SocialCredits.Domain.ViewModels;
using SocialCredits.Repositories.Interfaces;
using SocialCredits.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using ZstdSharp.Unsafe;

namespace SocialCredits.Services
{
    public class UserService : IUserServices
    {
        private readonly IUserRepository _repository;
        private readonly JWTSettings _options;
        private readonly IMapper _mapper;


        public UserService(IOptions<JWTSettings> options, IUserRepository userRepository, IMapper mapper)
        {
            _options = options.Value;
            _repository = userRepository;
            _mapper = mapper;
        }

        public async Task<User> GetUserByLogin(string login)
        {
            return await _repository.GetUserByLogin(login);
        }

        public async Task<UserToShowViewModel> GetUserToShow(string login)
        {
            var user = await _repository.GetUserByLogin(login);
            return _mapper.Map<UserToShowViewModel>(user); 
        }

        public User GetUserByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserToShowViewModel>> GetAllUsersList()
        {
            var users = await _repository.GetAllUsersList();
            var mappingUsers = new List<UserToShowViewModel>();
            foreach (var user in users)
            {
                mappingUsers.Add(_mapper.Map<UserToShowViewModel>(user));
            }
            return mappingUsers;
        }

        public async Task<(HttpStatusCode StatusCode, string Message)> Login(UserLoginDTO user)
        {
            var baseUser = await _repository.GetUserByLogin(user.Login);
            if (baseUser == null)
            {
                return (HttpStatusCode.NotFound, "Користувача не знайдено");
            }
            else if (baseUser.Password != user.Password)
            {
                return (HttpStatusCode.BadRequest, "Невірний пароль");
            }
            else if (baseUser.Role == "Unreg")
            {
                return (HttpStatusCode.Unauthorized, "Дочекайтеся поки спільнота схвалить вас");
            }
            else
            {
                return (HttpStatusCode.OK, GetToken(baseUser));
            }

        }

        public async Task<bool> Registration(UserRegistrationWithImageNameViewModel model)
        {
            var user = await _repository.GetUserByLogin(model.Login);
            if (user != null)
            {
                return false;
            }
            var newUser = new User(model.Login, model.Name, model.Password, model.imageName, model.Socials);
            var result = await _repository.CreateUser(newUser);
            return result;

        }

        public async Task<bool> Registration(UserRegistrationWithImageViewModel model)
        {
            var user = await _repository.GetUserByLogin(model.Login);
            if (user != null)
            {
                return false;
            }
            var imagePath = SaveImage(model.Image);
            var newUser = new User(model.Login, model.Name, model.Password, imagePath, model.Socials);
            var result = await _repository.CreateUser(newUser);
            return result;

        }
        
        public async Task<long> GetUsersCount()
        {
            return await _repository.GetUsersCount();
        }
        
        public async Task ChangeRoleToUser(string login)
        {
            await _repository.UpdateUserRole(login, "User");
        }


        private string GetToken(User user)
        {
            List<Claim> claims = new();
            claims.Add(new Claim("Login", user.Login));
            claims.Add(new Claim("Name", user.Name));
            claims.Add(new Claim("Role", user.Role.ToString()));
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
        private string SaveImage(IFormFile image)
        {
            try
            {
                if (image != null && image.Length > 0)
                {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
                    var imagePath = Path.Combine("D:/images/", fileName);
                    //for test D:/images/
                    //for prod : /app/images
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        image.CopyTo(stream);
                    }
                    return "https://serverip/socialimages/" + fileName;
                }
                else return "Something Wrong With Image";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

       
    }
}

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

namespace SocialCredits.Services
{
    public class UserService : IUserServices
    {
        private readonly IUserRepository _repository;
        private readonly JWTSettings _options;

        public UserService(IOptions<JWTSettings> options, IUserRepository userRepository)
        {
            _options = options.Value;
            _repository = userRepository;
        }

        public User GetUserByLogin(int id)
        {
            throw new NotImplementedException();
        }

        public User GetUserByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<(HttpStatusCode, string)> Login(UserLoginDTO user)
        {
            var baseUser = await _repository.GetUserByLogin(user.Login);
            if (baseUser == null)
            {
                return (HttpStatusCode.NotFound, "Користувача не знайдено");
            }
            else if (baseUser.Password != user.Password)
            {
                return (HttpStatusCode.BadRequest, "Невірне ім'я або пароль");
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
            var newUser = new User(model.Login, model.Name, model.Password, model.imageName,model.socials);
            var result = await _repository.CreateUser(newUser);
            return result;

        }

        public async Task<bool> Registration(UserRegistrationWithImageViewModel model)
        {
            var imagePath = SaveImage(model.Image);
            var newUser = new User(model.Login, model.Name, model.Password, imagePath, model.socials);
            var result = await _repository.CreateUser(newUser);
            return result;

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

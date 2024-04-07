
namespace SocialCredits.Domain.DTO
{
    public class UserLoginDTO
    {
        public string Login {  get; set; }
        public string Password { get; set; }

        public UserLoginDTO(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}

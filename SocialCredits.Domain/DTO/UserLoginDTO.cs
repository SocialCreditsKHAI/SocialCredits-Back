using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

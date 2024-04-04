using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialCredits.Domain.Models
{
    public class User
    {
        public int _id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string ImagePath { get; set; }
        public decimal Credits { get; set; }
        public List<Socials> Social { get; set; }
        public string Role { get; set; }
        private Random rand = new Random();

        private User()
        {
            _id = rand.Next(99999999);
            Role = "Unreg";
            Credits = 0;
        }

        public User(string login, string name, string password) : this()
        {
            Login = login;
            Name = name;
            Password = password;
            Social = new List<Socials> { }; 
        }

        public User(string login, string name, string password, List<Socials> social) : this()
        {
            Login = login;
            Name = name;
            Password = password;
            Social = social;
        }
    }
}

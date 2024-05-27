using MongoDB.Bson;

namespace SocialCredits.Domain.Models
{
    public class User
    {
        public ObjectId _id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string ImagePath { get; set; }
        public decimal Credits { get; set; }
        public List<Socials> Social { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }

        private User()
        {
            CreatedAt = DateTime.Now;
            Role = "Unreg";
            Credits = 0;
        }

        public User(string login, string name, string password, string imagePath) : this()
        {
            Login = login;
            Name = name;
            Password = password;
            Social = new List<Socials> { }; 
            ImagePath = imagePath;
        }

        public User(string login, string name, string password, string imagePath, List<Socials> social) : this()
        {
            Login = login;
            Name = name;
            Password = password;
            Social = social;
            ImagePath = imagePath;
        }
        
        public void EditRole()
        {
            Role = "user";
        }
    }
}

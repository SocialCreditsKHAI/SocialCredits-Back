using MongoDB.Bson;
using SocialCredits.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialCredits.Domain.ViewModels
{
    public class UserToShowViewModel
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public decimal Credits { get; set; }
        public List<Socials> Social { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

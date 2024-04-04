using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialCredits.Domain.Models
{
    public class Socials
    {
        public string SocialName{ get; set; }
        public string SocialLink { get; set;}
        
        public Socials(string socialName, string socialLink)
        {
            SocialName = socialName;
            SocialLink = socialLink;
        }
    }
}

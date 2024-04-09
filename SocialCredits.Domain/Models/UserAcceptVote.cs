using MongoDB.Bson;
using System.Reflection;

namespace SocialCredits.Domain.Models
{
    public class UserAcceptVote
    {
        public ObjectId _id { get; set; }
        public string UserLogin { get; set; }
        public List<Voter> Voters { get; set; }

        public UserAcceptVote(string userLogin)
        {
            UserLogin = userLogin;
            Voters = new List<Voter>();
        }

        public (int Accept,int Unaccept) GetVotersStatistic()
        {
            return (Voters.Count(v => v.isAccept), Voters.Count(v => !v.isAccept));
        }
    }
}

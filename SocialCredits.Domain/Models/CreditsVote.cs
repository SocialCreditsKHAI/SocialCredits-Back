using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SocialCredits.Domain.Models
{
    public class CreditsVote
    {
        public ObjectId _Id { get; set; }
        public decimal Amount { get; set; }
        public string FromUser { get; set; }
        public string AboutUser { get; set; }
        public string Topic { get; set; }
        public List<Voter> Voters { get; set; }
        public DateTime CreateTime { get; private set; }

        public CreditsVote() {
        CreateTime = DateTime.Now;
        }

        public (int Accept, int Unaccept) GetVotersStatistic()
        {
            return (Voters.Count(v => v.isAccept), Voters.Count(v => !v.isAccept));
        }
    }
}

namespace SocialCredits.Domain.Models
{
    public class Voter
    {

        public string VoterLogin { get; set; }
        public bool isAccept { get; set; }
        public DateTime DateTime { get; set; }

        public Voter()
        {
            DateTime = DateTime.Now;
        }
    }
}

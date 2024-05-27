using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialCredits.Domain.Enums
{
    public enum VoteStatus : byte
    {
        Approved,
        Unapproved,
        Continue,
        AlreadyVoted,
        NotFound
    }
}

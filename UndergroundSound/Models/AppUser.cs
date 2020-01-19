using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UndergroundSound.Models
{
    public class AppUser : IdentityUser
    {

       
        public DateTime Last_Day_When_Votes_Added { get; set; }

        public AppUser()
        {
            //Votes = new HashSet<Vote>();


            


        }

        public int VoteCount { get; set; }
      

        public string SubtractVote()
        {
            if (VoteCount > 0)
            {
                VoteCount = VoteCount - 1;
                return "Vote Succes";
            }
            else
            {
                return "Vote Error";
            }
        }






       


        public Band Band { get; set; }
        //public ICollection<Vote> Votes { get; set; }
    }
}

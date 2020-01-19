using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UndergroundSound.Models
{
    public class CheckAvailableVotes
    {

        public CheckAvailableVotes()
        {
            bandsrecord = new BandsRecord();
            song = new Song();
        }

       public int AvailableVotes { get; set; }
       public int HoursToUpdate { get; set; }
        public BandsRecord bandsrecord { get; set; }
        public Song song { get; set; }
        
    }
}

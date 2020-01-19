using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UndergroundSound.Models
{
    public class AddUserToBandViewModel
    {

        public AddUserToBandViewModel()
        {
            list = new List<AppUser>();
        }
        public string UserId { get; set; }
        public int BandId { get; set; }
        public List<AppUser> list { get; set; }
    }
}

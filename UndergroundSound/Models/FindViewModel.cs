using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UndergroundSound.Models
{
    public class FindViewModel
    {
        public FindViewModel()
        {
            Songs = new List<Song>();
            Bands = new List<Band>();
            BandsRecords = new List<BandsRecord>();
        }
        public List<Song> Songs { get; set; }
        public List<Band> Bands { get; set; }
        public List<BandsRecord> BandsRecords { get; set; }
        public int Count { get; set; } = 0;

    }
}

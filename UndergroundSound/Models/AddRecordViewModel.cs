using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UndergroundSound.Models
{

    


    public class AddRecordViewModel
    {

        public AddRecordViewModel()
        {
            bandsrecord = new BandsRecord();
        }

        public int BandId { get; set; }
        public BandsRecord bandsrecord { get; set; }
      
    }
}

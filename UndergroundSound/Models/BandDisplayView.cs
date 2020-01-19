using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UndergroundSound.Models
{
    public class BandDisplayView
    {
        Band Band { get; set; }
        List<BandsRecord> BandsRecords { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaxMind.GeoIP2;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UndergroundSound.Models;
namespace UndergroundSound.Models
{
    public class UserLocationDetails
    {
        public string Ip { get; set; }
        public string Province { get; set; }
        public string County { get; set; }
        public string City { get; set; }
     

    }
}

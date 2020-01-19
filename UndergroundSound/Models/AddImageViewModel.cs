using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UndergroundSound.Models
{
    public class AddImageViewModel
    {
        public int ObjectId { get; set; }
        public IFormFile ImageFile { get; set; }
        public string PictureObjectType { get; set; }
    }
}

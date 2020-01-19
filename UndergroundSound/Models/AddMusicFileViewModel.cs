using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UndergroundSound.Models
{
    public class AddMusicFileViewModel
    {
        public int SongId { get; set; }
        public IFormFile SongFile { get; set; }
    }
}

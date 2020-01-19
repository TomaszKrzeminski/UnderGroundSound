using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UndergroundSound.Models
{
    public class AddSongViewModel
    {
        public AddSongViewModel()
        {
            song = new Song();
        }

        public int BandsRecordId { get; set; }
        [Required]
        public Song song { get; set; }


    }

}

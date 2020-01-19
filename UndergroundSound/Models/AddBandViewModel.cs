using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UndergroundSound.Models
{
    public class AddBandViewModel
    {

        public AddBandViewModel()
        {
            MusicGenres = new List<string>();
            ProvincesList = new List<Province>();
            CreateProvinceList();
        }

        public List<Province> ProvincesList { get; set; }
       


        public void CreateProvinceList()
        {

            string[] list = new string[] { "","dolnośląskie", "kujawsko-pomorskie", "lubelskie", "lubuskie", "łódzkie", "małopolskie", "mazowieckie", "opolskie", "podkarpackie", "podlaskie", "pomorskie", "śląskie", "świętokrzyskie", "warmińsko-mazurskie", "wielkopolskie", "zachodniopomorskie" };

            for (int i = 0; i < 16; i++)
            {
                ProvincesList.Add(new Province() { ProvinceName = list[i] });
            }


            List<string> dolnośląskie = new List<string> { "bolesławiecki", "dzierżoniowski", "głogowski", "jaworski", "Jelenia Góra" };
            List<string> kujawsko_pomorskie = new List<string> { "aleksandrowski", "brodnicki ", "bydgoski", "chełmiński", "golubsko-dobrzyński" };
            List<string> lubelskie = new List<string> { "bialski", "biłgorajski", "chełmski", "hrubieszowski", "janowski" };


            foreach (var item in dolnośląskie)
            {
                ProvincesList[1].CountyList.Add(new County() { CountyName = item });
            }

            foreach (var item in kujawsko_pomorskie)
            {
                ProvincesList[2].CountyList.Add(new County() { CountyName = item });
            }


            foreach (var item in lubelskie)
            {
                ProvincesList[3].CountyList.Add(new County() { CountyName = item });
            }





        }

        [Required]
        public string BandName { get; set; }

        [Required]
        public string FormedPlace { get; set; }

        
        public Place Place{get;set;}

      [Required]
        public DateTime FormedDate { get; set; }

        [Required]
        public string History { get; set; }






        public string  AppUserId { get; set; }
        [Display(Name = "Styl Muzyczny")]
        [Required(ErrorMessage ="Wybierz styl muzyczny")]
        public string MusicGenre {get;set;}
        public List<string> MusicGenres { get; set; }
    }
}

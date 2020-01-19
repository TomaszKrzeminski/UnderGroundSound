using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UndergroundSound.Models
{

    public class Province
    {
        public Province()
        {
            CountyList = new List<County>();
        }

        public string ProvinceName { get; set; }
        public List<County> CountyList { get; set; }
    }

    public class County
    {
        public string CountyName { get; set; }
    }


    public class MapViewData
    {

        public MapViewData()
        {
            ProvinceList = new List<Province>();
            CreateProvinceList();
        }

        public List<Province> ProvinceList { get; set; }

        public void CreateProvinceList()
        {

            string[] list = new string[] { "dolnośląskie", "kujawsko-pomorskie", "lubelskie", "lubuskie", "łódzkie", "małopolskie", "mazowieckie", "opolskie", "podkarpackie", "podlaskie", "pomorskie", "śląskie", "świętokrzyskie", "warmińsko-mazurskie", "wielkopolskie", "zachodniopomorskie" };

            for (int i = 0; i < 16; i++)
            {
                ProvinceList.Add(new Province() { ProvinceName = list[i] });
            }


            List<string> dolnośląskie = new List<string> { "bolesławiecki", "dzierżoniowski", "głogowski", "jaworski", "Jelenia Góra" };
            List<string> kujawsko_pomorskie = new List<string> { "aleksandrowski", "brodnicki ", "bydgoski", "chełmiński", "golubsko-dobrzyński" };
            List<string> lubelskie = new List<string> { "bialski", "biłgorajski", "chełmski", "hrubieszowski", "janowski" };


            foreach (var item in dolnośląskie)
            {
                ProvinceList[0].CountyList.Add(new County() { CountyName = item });
            }

            foreach (var item in kujawsko_pomorskie)
            {
                ProvinceList[1].CountyList.Add(new County() { CountyName = item });
            }


            foreach (var item in lubelskie)
            {
                ProvinceList[2].CountyList.Add(new County() { CountyName = item });
            }





        }




    }
}

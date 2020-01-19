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

namespace UndergroundSound.Controllers
{
    public class HomeController : Controller
    {

        IRepository repository;
        UserManager<AppUser> UserMgr;
        private readonly IHostingEnvironment _hostingEnvironment;

        public HomeController(IRepository repo, UserManager<AppUser> userMgr, IHostingEnvironment hostingEnvironment)
        {
            repository = repo;
            UserMgr = userMgr;
            _hostingEnvironment = hostingEnvironment;
        }

        private Task<AppUser> GetCurrentUserAsync() => UserMgr.GetUserAsync(HttpContext.User);

        public ActionResult FiltrMusicGenre(string City, string County, string Province, string MusicGenreName)
        {
                                 
            HomePageViewModel model = SetHomePageModel(City, County, Province);
          
            model.SetListCollections();
            model.SortBandsRecordLists(MusicGenreName);
            model.SortSongLists(MusicGenreName);
            model.AddBandsRecordIfNecessary();
            model.AddSongsIfNecessary();
            ReplaceListsInModel(ref model);
            return View("Index", model);
        }


        void ReplaceListsInModel(ref HomePageViewModel model)
        {
            model.ListOfNewestsSongs = model.listCollectionSongs[0];
            model.ListOfHighestRatedSongs = model.listCollectionSongs[1];
            model.ListOfNewestsSongsLocalProvince = model.listCollectionSongs[2];
            model.ListOfHighestRatedSongsLocalProvince = model.listCollectionSongs[3];
            model.ListOfNewestsSongsLocalCounty = model.listCollectionSongs[4];
            model.ListOfHighestRatedSongsLocalCounty = model.listCollectionSongs[5];


            model.ListOfNewestsBandsRecord = model.listCollectionBandsRecors[0];
            model.ListOfHighestRatedBandsRecord = model.listCollectionBandsRecors[1];
            model.ListOfNewestsBandsRecordLocalProvince = model.listCollectionBandsRecors[2];
            model.ListOfHighestRatedBandsRecordLocalProvince = model.listCollectionBandsRecors[3];
            model.ListOfNewestsBandsRecordLocalCounty = model.listCollectionBandsRecors[4];
            model.ListOfHighestRatedBandsRecordLocalCounty = model.listCollectionBandsRecors[5];


        }


        List<Song> AddSongsIfNecessary(List<Song> list)
        {
            if (list.Count >= 5)
            {
                return list;
            }
            else
            {
                int numberToAdd = list.Count;

                for (int i = numberToAdd; i < 5; i++)
                {
                    list.Add(new Song() { SongName = "Brak" });
                }
                return list;
            }


        }


        public System.Net.IPAddress GetClientIp()
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress;
            return ipAddress;
        }








        public ActionResult MapDisplay()
        {
            MapDataView model = new MapDataView(repository);

            return View(model);
        }



        public ActionResult UsersPanel()
        {
            return View();
        }

        public ActionResult SelectProvince(string Name)
        {
            return RedirectToAction("Index");
        }


        public IActionResult Index(int Id = 0, string MusicGenre = "None")
        {

            string LocalProvince = "None";
            string LocalCounty = "None";
            string City = "None";

            if (Id != 0)
            {
                Models.ProvincePoland province = repository.GetProvinceByCountyId(Id);

                if (province.ProvincePolandId == 0)
                {
                    LocalProvince = "kujawsko-pomorskie";
                    LocalCounty = "chełmiński";
                    City = "Chełmno";
                }
                else
                {
                    LocalProvince = province.ProvinceName;
                    LocalCounty = province.Countys.Where(c => c.CountyPolandId == Id).First().CountyName;
                }

            }
            else if (GetClientIp() != null && repository.GetUserLocationDetails(GetClientIp()).City != "Not Found")
            {
                UserLocationDetails ModelLocation = repository.GetUserLocationDetails(GetClientIp());
                LocalCounty = ModelLocation.County;
                LocalProvince = ModelLocation.Province;
                City = ModelLocation.City;

            }
            else
            {
                LocalProvince = "kujawsko-pomorskie";
                LocalCounty = "chełmiński";
                City = "Chełmno";
            }


            HomePageViewModel model = SetHomePageModel(City, LocalCounty, LocalProvince);




            return View(model);
        }


        HomePageViewModel SetHomePageModel(string City, string LocalCounty, string LocalProvince)
        {
            HomePageViewModel model = new HomePageViewModel();
            model.City = City;
            model.County = LocalCounty;
            model.Province = LocalProvince;
            model.MusicGenres = repository.MusicGenres.ToList();


            model.ListOfNewestsSongs = repository.GetNewestsSongs();
            model.ListOfHighestRatedSongs = repository.GetHighestRatedSongs();
            model.ListOfNewestsBandsRecord = repository.GetNewestsBandsRecords();
            model.ListOfHighestRatedBandsRecord = repository.GetHighestRatedBandsRecords();

            model.ListOfNewestsSongsLocalProvince = repository.GetNewestsSongs(LocalProvince);
            model.ListOfHighestRatedSongsLocalProvince = repository.GetHighestRatedSongs(LocalProvince);
            model.ListOfNewestsBandsRecordLocalProvince = repository.GetNewestsBandsRecords(LocalProvince);
            model.ListOfHighestRatedBandsRecordLocalProvince = repository.GetHighestRatedBandsRecords(LocalProvince);


            model.ListOfNewestsSongsLocalCounty = repository.GetNewestsSongs(LocalProvince, LocalCounty);
            model.ListOfHighestRatedSongsLocalCounty = repository.GetHighestRatedSongs(LocalProvince, LocalCounty);
            model.ListOfNewestsBandsRecordLocalCounty = repository.GetNewestsBandsRecords(LocalProvince, LocalCounty);
            model.ListOfHighestRatedBandsRecordLocalCounty = repository.GetHighestRatedBandsRecords(LocalProvince, LocalCounty);

            return model;
        }


    }
}
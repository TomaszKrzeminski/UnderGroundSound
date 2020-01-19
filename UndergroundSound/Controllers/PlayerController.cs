using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UndergroundSound.Models;

namespace UndergroundSound.Controllers
{
    public class PlayerController : Controller
    {

        IRepository repository;
        UserManager<AppUser> UserMgr;
        public PlayerController(IRepository repo, UserManager<AppUser> userMgr)
        {
            repository = repo;
            UserMgr = userMgr;
        }

        private Task<AppUser> GetCurrentUserAsync() => UserMgr.GetUserAsync(HttpContext.User);


        public ActionResult BandsDisplay(int BandId=0,int SongId=0,int BandsRecordId=0)
        {

            Band band = new Band();

            if(BandId>0)
            {
                band = repository.GetBandById(BandId);
            }
            else if(SongId>0)
            {
              band=repository.GetSongById(SongId).BandsRecord.Band;
           
                band = repository.GetBandById(band.BandId);


            }
            else if(BandsRecordId>0)
            {

                band = repository.GetBandsRecordById(BandsRecordId).Band;

                band = repository.GetBandById(band.BandId);

            }


            return View(band);
        }



        public ActionResult FindSong_BandsRecord_Band(string Name)
        {

            FindViewModel model = repository.GetFindResults(Name);

            return View(model);
        }

        public ActionResult SongDetails(int Id)
        {

            Song song = repository.GetSongById(Id);

            if(song!=null)
            {
                return View(song);
            }
            else
            {
                return View("Error", "Nie znaleziono Piosenki");
            }

            
        }

    }
}
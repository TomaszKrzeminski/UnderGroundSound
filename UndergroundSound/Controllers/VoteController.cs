using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UndergroundSound.Models;

namespace UndergroundSound.Controllers
{
    [Authorize]
    public class VoteController : Controller
    {

        IRepository repository;
        UserManager<AppUser> UserMgr;
        public VoteController(IRepository repo, UserManager<AppUser> userMgr)
        {
            repository = repo;
            UserMgr = userMgr;

           
        }

        private Task<AppUser> GetCurrentUserAsync() => UserMgr.GetUserAsync(HttpContext.User);



       
        public ActionResult BandsRecordAddVote(int Id)
        { 
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            BandsRecord bandsrecord = repository.GetBandsRecordById(Id);
            CheckAvailableVotes voteData = repository.CheckVotesData(userId);
            voteData.bandsrecord = bandsrecord;
            return View(voteData);
        }

      
        [HttpPost]
        public ActionResult BandsRecordAddVote(BandsRecord bandsrecord)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool value = repository.AddVoteToBandsRecord(bandsrecord, userId);


            if(value)
            {
                return RedirectToAction("BandsDisplay", "Player", new { BandId = bandsrecord.Band.BandId });
            }

            return View("Error", "Nie można oddać głosu");
        }



        public ActionResult SongAddVote(int Id)
        {
            
            Song song = repository.GetSongById(Id);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            CheckAvailableVotes voteData= repository.CheckVotesData(userId);
            voteData.song = song;
            return View(voteData);
        }


        [HttpPost]
        public ActionResult SongAddVote(Song song)
        {

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool value = repository.AddVoteToSong(song, userId);


            if (value)
            {
                return RedirectToAction("BandsDisplay", "Player", new { BandId = song.BandsRecord.Band.BandId });
            }

            return View("Error", "Nie można oddać głosu");





        }





    }
}
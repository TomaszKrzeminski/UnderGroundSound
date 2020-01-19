using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UndergroundSound.Models;

namespace UndergroundSound.Controllers
{
    public class SongController : Controller
    {
        IRepository repository;
        private readonly IHostingEnvironment hostingEnvironment;
        UserManager<AppUser> UserMgr;
        public SongController(IRepository repo, UserManager<AppUser> userMgr, IHostingEnvironment environment)
        {
            hostingEnvironment = environment;
            repository = repo;
            UserMgr = userMgr;
        }

        private Task<AppUser> GetCurrentUserAsync() => UserMgr.GetUserAsync(HttpContext.User);



        public IActionResult SongList(int Id)
        {

            ViewBag.BandId = Id;
            List<Song> list = repository.GetSongsByBandsRecordId(Id);

            if (list != null)
            {
                return View(list);
            }
            else
            {
                return View(new List<Song>());
            }

        }


        public ActionResult AddSong(int Id)
        {

            AddSongViewModel model = new AddSongViewModel();
            model.BandsRecordId = Id;

            return View(model);
        }


        [HttpPost]
        public ActionResult AddSong(AddSongViewModel model)
        {


            if (ModelState.IsValid)
            {
                bool value = repository.AddSongToBandsRecord(model);
                if (value)
                {
                    return RedirectToAction("SongList", new { Id = model.BandsRecordId });
                }
                else
                {
                    return View("Error", $"Dodanie utworu nie powiodło się ");
                }
            }
            else
            {
                return View("AddSong", model);
            }





        }



        public ActionResult RemoveSong(int Id)
        {

            Song song = repository.RemoveSong(Id);

            if (song != null)
            {
                return RedirectToAction("SongList", new { Id = song.SongId });
            }
            else
            {
                return View("Error", $"Piosenka  nie usunięta");
            }

        }



        public ActionResult EditSong(int Id)
        {


            Song song = repository.GetSongById(Id);
            if (song != null)
            {
                return View(song);
            }
            else
            {
                return View("Error", $"Błąd przy edycji piosenki");
            }

        }

        [HttpPost]
        public ActionResult EditSong(Song song)
        {

            int value = repository.ChangeSong(song);

            if (value > 0)
            {
                return RedirectToAction("SongList", new { Id = value });
            }
            else
            {
                return View("Error", $"Błąd przy zmianie utworu");
            }


        }




        //public ActionResult AddMusicFile(int Id)
        //{
        //    AddMusicFileViewModel model = new AddMusicFileViewModel();
        //    model.SongId = Id;
        //    return View(model);
        //}


       

        //[HttpPost]
        //public ActionResult AddMusicFile(AddMusicFileViewModel model)
        //{

        //    CheckExtensionMp3Wav(model);

        //    if (model.SongFile != null&&ModelState.IsValid)
        //    {
        //        var uniqueFileName = GetUniqueFileNameAndSavePathToSong(model.SongFile.FileName, model.SongId);
        //        var uploads = Path.Combine(hostingEnvironment.WebRootPath, "Songs");
        //        var filePath = Path.Combine(uploads, uniqueFileName);
        //        model.SongFile.CopyTo(new FileStream(filePath, FileMode.Create));

        //        //to do : Save uniqueFileName  to your db table   
        //    }
        //    else
        //    {
        //        return View("AddMusicFile", model);
        //    }

        //    int value = repository.GetBandsRecordIdBySongId(model.SongId);
        //    if (value > 0)
        //    {
        //        return RedirectToAction("SongList", new { Id = value });
        //    }
        //    else
        //    {
        //        return View("Error", "Nie znaleziono płyty");
        //    }

        //}
        //public void CheckExtensionMp3Wav(AddMusicFileViewModel model)
        //{
        //    if (model.SongFile != null)
        //    {
        //        string extension = Path.GetExtension(model.SongFile.FileName);
        //        if (extension == ".mp3" || extension == ".wav")
        //        {

        //        }
        //        else
        //        {
        //            ModelState.AddModelError(nameof(model.SongFile), $"Plik typu {extension} nie jest akceptowany wprowadź plik typu wav lub mp3");

        //        }
        //    }



        //}



        //private string GetUniqueFileNameAndSavePathToSong(string fileName, int Id)
        //{

        //    Song song = repository.GetSongById(Id);
        //    string SongName = song.SongName;

        //    fileName = Path.GetFileName(fileName);


        //    string FullName = SongName
        //              + "_"
        //              + Guid.NewGuid().ToString().Substring(0, 4)
        //              + Path.GetExtension(fileName);

        //    bool value = repository.AddPathToSong(Id, FullName);
        //    return FullName;
        //}




    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using UndergroundSound.Models;

namespace UndergroundSound.Controllers
{
    [Authorize]
    public class UsersOptionsController : Controller
    {


      

        IRepository repository;
        UserManager<AppUser> UserMgr;
        public UsersOptionsController(IRepository repo, UserManager<AppUser> userMgr)
        {
            repository = repo;
            UserMgr = userMgr;
           
        }

        private Task<AppUser> GetCurrentUserAsync() => UserMgr.GetUserAsync(HttpContext.User);

        //var user = await GetCurrentUserAsync();
        //var userId = user?.Id;
        //string mail = user?.Email;


        List<string> GetMusicGenresNames()
        {
            List<string> names = new List<string>();
            foreach (var item in repository.MusicGenres)
            {
                names.Add(item.MusicGenreName);
            }

            return names;
        }




        [HttpPost]
        public IActionResult LoadComponent(string id)
        {
            return ViewComponent("ProvinceDisplay", id);
        }






        public ActionResult AddBand(string ProvinceName = "")
        {
            AddBandViewModel model = new AddBandViewModel();



            model.AppUserId = UserMgr.GetUserId(HttpContext.User);

            model.MusicGenres.AddRange(GetMusicGenresNames());
            return View(model);
        }

        public ActionResult RemoveBand()
        {
            string AppUserId = UserMgr.GetUserId(HttpContext.User);
            List<Band> bandList = repository.GetBandsCreatedByUser(AppUserId);
            return View(bandList);
        }

        public ActionResult EditBand()
        {
            AppUser user = GetCurrentUserAsync().Result;
            List<Band> list = repository.GetBandsByUserId(user.Id);
            return View(list);
        }
        [HttpPost]
        public ActionResult AddBand(AddBandViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                bool value = repository.AddBand(viewmodel);
                if (value)
                {
                    return View("Success", $"Band name {viewmodel.BandName} added");
                }
                else
                {
                    return View("Error", $"Band name {viewmodel.BandName} adding error");
                }

            }
            else
            {
                viewmodel.AppUserId = UserMgr.GetUserId(HttpContext.User);

                viewmodel.MusicGenres.AddRange(GetMusicGenresNames());
                return View(viewmodel);
            }

        }
        [HttpPost]
        public ActionResult RemoveBand(int BandId)
        {
            bool value = repository.RemoveBand(BandId);
            if (value)
            {
                return RedirectToAction("RemoveBand");
            }
            else
            {
                return View("Error", $"Band removing error");
            }

        }

        public ActionResult EditBandById(int id)
        {
            Band band = repository.GetBandById(id);
            return View(band);
        }

        [HttpPost]
        public ActionResult EditBandById(Band band)
        {


            if (ModelState.IsValid)
            {

                bool value = repository.ChangeBand(band);
                if (value)
                {
                    return View("Success", $"Band name {band.BandName} changed");
                }
                else
                {
                    return View("Error", $"Band name {band.BandName} change error");
                }

            }
            else
            {



                return View("EditBandById", band);
            }




        }



        public ViewResult EditBandMembers(int id)
        {
            Band band = repository.GetBandWithUsers(id);

            return View(band);


        }


        public ViewResult AddUserToBand(int id)
        {

            AddUserToBandViewModel model = new AddUserToBandViewModel();
            model.BandId = id;
            model.list = UserMgr.Users.ToList();
            return View(model);

        }


        [HttpPost]
        public ViewResult AddUserToBand(AddUserToBandViewModel model)
        {



            bool value = repository.AddUserToBand(model.BandId, model.UserId);
            if (value)
            {
                return View("Success", $"Użytkownik dodany do zespołu");
            }
            else
            {
                return View("Error", $"Błąd użytkownik nie dodany do zespołu");
            }



        }



        public ViewResult RemoveUserFromBand(int BandId, string UserId)
        {

            bool value = repository.RemoveUserFromBand(UserId, BandId);
            if (value)
            {
                return View("Success", $"Użytkownik usunięty z  zespołu");
            }
            else
            {
                return View("Error", $"Błąd użytkownik nie usunięty z  zespołu");
            }


        }

        /////////////////////

        //Add Picture Methods//

        //public ActionResult AddPictureFile(int Id)
        //{
        //    AddImageViewModel model = new AddImageViewModel();
        //    model.ObjectId = Id;
        //    return View(model);
        //}




        //[HttpPost]
        //public ActionResult AddPictureFile(AddImageViewModel model)
        //{
        //    bool check = false;

        //    imageAdder.CheckExtensionType(model);

        //    if (model.ImageFile != null && ModelState.IsValid)
        //    {
        //      check=  imageAdder.AddPictureFileToServer(model);

        //        //to do : Save uniqueFileName  to your db table   
        //    }
        //    else
        //    {
        //        return View("AddPictureFile", model);
        //    }

           
        //    if (check)
        //    {
        //        return RedirectToAction("BandPictures", new { Id = model.ObjectId });
        //    }
        //    else
        //    {
        //        return View("Error", "Nie dodano zdjęcia");
        //    }

        //}
       

        //public ActionResult BandPictures(int Id)
        //{
        //    List<Picture> PictureList = repository.GetBandById(Id).Pictures.ToList();

        //    return View(PictureList);

        //}

                                       








    }
}
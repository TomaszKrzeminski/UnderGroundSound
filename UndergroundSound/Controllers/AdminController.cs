using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UndergroundSound.Models;

namespace UndergroundSound.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<AppUser> userManager;
        private IRepository repository;
        public AdminController(UserManager<AppUser> usrMgr,IRepository repo)
        {
            userManager = usrMgr;
            repository = repo;
        }



        public ViewResult Index() => View(userManager.Users);

        public ViewResult Create() => View();
                      
        [HttpPost]
        public async Task<IActionResult> Create(CreateModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser { UserName = model.Name, Email = model.Email };


                IdentityResult result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

            }

            return View(model);
        }

        ///////////////////////////////////////////
        

        public ActionResult AddGenre()
        {

            MusicGenre musicgenre = new MusicGenre();


            return View(musicgenre);
        }

        [HttpPost]
        public ActionResult AddGenre(MusicGenre musicgenre)
        {

            if(ModelState.IsValid)
            {
                repository.AddMusicGenre(musicgenre);
               return RedirectToAction("Index", "Home", null);
            }
            else
            {
              return View(musicgenre);
            }
            
        }

        public ActionResult RemoveGenre()
        {
            List<MusicGenre> list = repository.MusicGenres.ToList();            
            return View(list);
        }

       
        public ActionResult DeleteGenre(int Id)
        {

         bool check=repository.RemoveMusicGenre(Id);

            if(check)
            {
              return  RedirectToAction("Index", "Home", null);
            }
            else
            {
                List<MusicGenre> list = repository.MusicGenres.ToList();                
                return View(list);
            }
        }










    }
}




//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using UndergroundSound.Models;

//namespace UndergroundSound.Controllers
//{
//    public class PictureController : Controller
//    {


//        ImageAdder imageAdder;

//        IRepository repository;
//        UserManager<AppUser> UserMgr;
//        public PictureController(IRepository repo, UserManager<AppUser> userMgr, ImageAdder imageAdderParam)
//        {
//            repository = repo;
//            UserMgr = userMgr;
//            imageAdder = imageAdderParam;
//        }

//        private Task<AppUser> GetCurrentUserAsync() => UserMgr.GetUserAsync(HttpContext.User);




//        public ActionResult AddPictureFile(int Id,string PictureObjectType)
//        {
//            AddImageViewModel model = new AddImageViewModel();
//            model.ObjectId = Id;
//            model.PictureObjectType = PictureObjectType;
//            return View(model);
//        }


        
//        public IPictureObjectFactory GetPictureObject(string ObjectType)
//        {
//            IPictureObjectFactory pictureobject;
//            switch(ObjectType)
//                {
//          case "Band":
//            pictureobject = new BandFactory(repository);
//            break;
//          case "Song":
//                    pictureobject = new SongFactory(repository);
//                    break;
//             case "MusicGenre":
//                    pictureobject = new MusicGenreFactory(repository);
//                    break;
//          case "BandsRecord":
//                    pictureobject = new BandsRecordFactory(repository);
//                    break;
//            default:
//                    pictureobject = null;
//            break;
//            }

//            return pictureobject;



//        }


//        [HttpPost]
//        public async Task<ActionResult> AddPictureFile(AddImageViewModel model)
//        {
//            bool check = false;
//            IPictureObjectFactory objectFactory = GetPictureObject(model.PictureObjectType);
//            imageAdder.pictureObject = objectFactory.BuildPictureObject();

//            imageAdder.CheckExtensionType(model);

//            if (model.ImageFile != null && ModelState.IsValid)
//            {
//                check =await imageAdder.AddPictureFileToServer(model);

//                //to do : Save uniqueFileName  to your db table   
//            }
//            else
//            {
//                return View("AddPictureFile", model);
//            }


//            if (check)
//            {
//                return RedirectToAction("ShowPictures", new { PictureObjectType = model.PictureObjectType, Id = model.ObjectId });
//            }
//            else
//            {
//                return View("Error", "Nie dodano zdjęcia");
//            }

//        }





       


//        public ActionResult ShowPictures(string PictureObjectType, int Id)
//        {

//            IPictureObjectFactory imageObjectFactory= GetPictureObject(PictureObjectType);
//            IPictureObject pictureObject = imageObjectFactory.BuildPictureObject();
//            List<Picture> PictureList = pictureObject.GetObjectPictures(Id);

//            return View(PictureList);

//        }
//    }
//}
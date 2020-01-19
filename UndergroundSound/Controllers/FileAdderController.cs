using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UndergroundSound.Models;
using UndergroundSound.Models.UndergroundSound.Models;

namespace UndergroundSound.Controllers
{
    public class FileAdderController : Controller
    {
        IFileAdder fileAdder;

        IRepository repository;
        UserManager<AppUser> UserMgr;
        public FileAdderController(IRepository repo, UserManager<AppUser> userMgr, IFileAdder fileAdderParam)
        {
            repository = repo;
            UserMgr = userMgr;
            fileAdder = fileAdderParam;
        }

        private Task<AppUser> GetCurrentUserAsync() => UserMgr.GetUserAsync(HttpContext.User);




        public ActionResult AddFile(int Id, string ObjectType,FileType filetype)
        {
            AddFileViewModel model = new AddFileViewModel();
            model.ObjectId = Id;
            model.ObjectType = ObjectType;
            model.FileDetails = new FileTypeDetails(filetype);
            return View(model);
        }



        public IFileObjectFactory GetFileObject(string ObjectType)
        {
            IFileObjectFactory fileobject;
            switch (ObjectType)
            {
                case "Band":
                    fileobject = new BandFactory(repository);
                    break;
                case "Song":
                    fileobject = new SongFactory(repository);
                    break;
                case "MusicGenre":
                    fileobject = new MusicGenreFactory(repository);
                    break;
                case "BandsRecord":
                    fileobject = new BandsRecordFactory(repository);
                    break;
                default:
                    fileobject = null;
                    break;
            }

            return fileobject;



        }


        [HttpPost]
        public async Task<ActionResult> AddFile(AddFileViewModel model)
        {
            Error error;

            if(!ModelState.IsValid)
            {
                return View(model);
            }

            bool check = false;
            IFileObjectFactory objectFactory = GetFileObject(model.ObjectType);
            fileAdder.fileobject = objectFactory.BuildObject();

           error=fileAdder.CheckExtensionType(model);

           if(error!=null)
            {
                ModelState.AddModelError(error.Key, error.Message);
            }

            if ( ModelState.IsValid)
            {
                check = await fileAdder.AddFileToServer(model);

                //to do : Save uniqueFileName  to your db table   
            }
            else
            {
                return View("AddFile", model);
            }


            if (check)
            {
                return RedirectToAction(model.FileDetails.MethodName, new { ObjectType = model.ObjectType, Id = model.ObjectId });
            }
            else
            {
                return View("Error", "Nie dodano pliku");
            }

        }




        public ActionResult ShowPictures(string ObjectType, int Id)
        {

            IFileObjectFactory ObjectFactory = GetFileObject(ObjectType);
            IFileObject Object = ObjectFactory.BuildObject();
            List<Picture> PictureList = Object.GetObjectPictures(Id);

            return View(PictureList);

        }
        public ActionResult ShowMusicVideo(string ObjectType, int Id)
        {

            IFileObjectFactory ObjectFactory = GetFileObject(ObjectType);
            IFileObject Object = ObjectFactory.BuildObject();
            List<MusicVideo> MusicVideoList = Object.GetObjectMusicVideos(Id);

            return View(MusicVideoList);

        }
        public ActionResult ShowMusicFile(string ObjectType, int Id)
        {

            IFileObjectFactory ObjectFactory = GetFileObject(ObjectType);
            IFileObject Object = ObjectFactory.BuildObject();
            string MusicFilePath = Object.GetObjectMusicFiles(Id);

            return View("ShowMusicFile",MusicFilePath);

        }
    }
}
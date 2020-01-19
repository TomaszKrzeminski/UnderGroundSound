//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Hosting.Internal;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.ModelBinding;
//using UndergroundSound.Models;

//namespace UndergroundSound.Models
//{
//    public interface ImageAdder
//    {
//        IHostingEnvironment hostingEnvironment { get; set; }
//        int ObjectId { get; set; }
//        IFormFile File { get; set; }
//        ModelStateDictionary ModelState { get; set; }
//        Task<bool> AddPictureFileToServer(AddImageViewModel model);
//        void CheckExtensionType(AddImageViewModel model);
//        string GetUniqueFileName(string fileName, int Id);
//       IPictureObject pictureObject { get; set; }
//    }



    


//    public class PictureAdderBand : ImageAdder
//    {
//        public int ObjectId { get; set; }
//        public IFormFile File { get; set; }
//        public ModelStateDictionary ModelState { get; set; }
//        public IHostingEnvironment hostingEnvironment { get; set; }
//        public IPictureObject pictureObject { get ; set ; }

//        private IRepository repository;
       


//        public PictureAdderBand(IHostingEnvironment hostingEnvironment, IRepository repository)
//        {
//            this.repository = repository;
//            this.hostingEnvironment = hostingEnvironment;
//            ModelState = new ModelStateDictionary();
//        }



//        public async Task<bool> AddPictureFileToServer(AddImageViewModel model)
//        {
//            try
//            {

//                var uniqueFileName = GetUniqueFileName(model.ImageFile.FileName, model.ObjectId);
//                var uploads = Path.Combine(hostingEnvironment.WebRootPath, "Images");
//                var filePath = Path.Combine(uploads, uniqueFileName);
           

//                await model.ImageFile.CopyToAsync(new FileStream(filePath, FileMode.Create));


//                return  pictureObject.SavePath(model.ObjectId, uniqueFileName);

//            }
//            catch (Exception ex)
//            {
//                return false;
//            }

//        }







       

//        public void CheckExtensionType(AddImageViewModel model)
//        {
//            if (model.ImageFile != null)
//            {
//                string extension = Path.GetExtension(model.ImageFile.FileName);
//                if (extension == ".jpg" || extension == ".png" || extension == ".gif")
//                {

//                }
//                else
//                {
//                    ModelState.AddModelError(nameof(model.ImageFile), $"Plik typu {extension} nie jest akceptowany wprowadź plik typu jpg,png lub gif");

//                }
//            }
//        }


       


//        public string GetUniqueFileName(string fileName, int Id)
//        {

//            string ObjectName = pictureObject.GetObjectName(Id);
//            fileName = Path.GetFileName(fileName);


//            string FullName = ObjectName
//                      + "_"
//                      + Guid.NewGuid().ToString().Substring(0, 4)
//                      + Path.GetExtension(fileName);


//            return FullName;
//        }

       



//    }


//    public interface IPictureObject
//    {
        
//        bool SavePath(int Id, string FullName);
//        string GetObjectName(int Id);
//        List<Picture> GetObjectPictures(int Id);
//    }


//    public interface IPictureObjectFactory
//    {
//         IPictureObject BuildPictureObject();
//    }


//    public class BandFactory : IPictureObjectFactory
//    {
//        IRepository repo;
//        public BandFactory(IRepository repository)
//        {
//            repo = repository;
//        }
//        public IPictureObject BuildPictureObject()
//        {
//            return new Band(repo );
//        }
//    }

//    public class SongFactory : IPictureObjectFactory
//    {
//        IRepository repo;
//        public SongFactory(IRepository repository)
//        {
//            repo = repository;
//        }
//        public IPictureObject BuildPictureObject()
//        {
//            return new Song(repo);
//        }
//    }

//    public class MusicGenreFactory : IPictureObjectFactory
//    {
//        IRepository repo;
//        public MusicGenreFactory(IRepository repository)
//        {
//            repo = repository;
//        }
//        public IPictureObject BuildPictureObject()
//        {
//            return new MusicGenre(repo);
//        }
//    }

//    public class BandsRecordFactory : IPictureObjectFactory
//    {
//        IRepository repo;
//        public BandsRecordFactory(IRepository repository)
//        {
//            repo = repository;
//        }
//        public IPictureObject BuildPictureObject()
//        {
//            return new BandsRecord(repo);
//        }
//    }

//}

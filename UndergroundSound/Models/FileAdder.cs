using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UndergroundSound.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Hosting.Internal;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using UndergroundSound.Models;

    namespace UndergroundSound.Models
    {
        public interface IFileAdder
        {
            IHostingEnvironment hostingEnvironment { get; set; }

            int ObjectId { get; set; }

            IFormFile File { get; set; }

            ModelStateDictionary ModelState { get; set; }

            Task<bool> AddFileToServer(AddFileViewModel model);

            Error CheckExtensionType(AddFileViewModel model);

            string GetUniqueFileName(string fileName, int Id);

            IFileObject fileobject { get; set; }

           
        }

        public class Error
        {
            public Error(string key, string message)
            {
                Key = key;
                Message = message;
            }

            public string Key { get; set; }
            public string Message { get; set; }
        }




        public class FileAdder : IFileAdder
        {
            public int ObjectId { get; set; }

            public IFormFile File { get; set; }

            public ModelStateDictionary ModelState { get; set; }

            public IHostingEnvironment hostingEnvironment { get; set; }

           public IFileObject fileobject { get; set; }

            private IRepository repository;



            public FileAdder(IHostingEnvironment hostingEnvironment, IRepository repository)
            {
                this.repository = repository;
                this.hostingEnvironment = hostingEnvironment;
                ModelState = new ModelStateDictionary();
            }



            public async Task<bool> AddFileToServer(AddFileViewModel model)
            {
                try
                {

                    var uniqueFileName = GetUniqueFileName(model.FileToSave.FileName, model.ObjectId);
                    var uploads = Path.Combine(hostingEnvironment.WebRootPath, model.FileDetails.DirectoryToSave);
                    var filePath = Path.Combine(uploads, uniqueFileName);


                    await model.FileToSave.CopyToAsync(new FileStream(filePath, FileMode.Create));


                    return fileobject.SavePath(model.ObjectId, uniqueFileName,model.FileDetails);

                }
                catch (Exception ex)
                {
                    return false;
                }

            }





            public Error CheckExtensionType(AddFileViewModel model)
            {
                bool accept = false;
                Error error = null;
                string extensionsText = String.Join(" ", model.FileDetails.ExtensionList);
                string extension = Path.GetExtension(model.FileToSave.FileName);
                foreach (var extensionToCheck in model.FileDetails.ExtensionList)
                {

                    if (model.FileToSave != null)
                    {

                        if (extension == extensionToCheck)
                        {
                            accept = true;
                            break;
                        }

                    }

                }


                if (accept == false)
                {
                    //ModelState.AddModelError("FileToSave", $"Plik typu {extension} nie jest akceptowany wprowadź plik typu {extensionsText}");
                    error = new Error("FileToSave", $"Plik typu {extension} nie jest akceptowany wprowadź plik typu {extensionsText}");

                }

                return error;
            }







            public string GetUniqueFileName(string fileName, int Id)
            {

                string ObjectName = fileobject.GetObjectName(Id);
                fileName = Path.GetFileName(fileName);


                string FullName = ObjectName
                          + "_"
                          + Guid.NewGuid().ToString().Substring(0, 4)
                          + Path.GetExtension(fileName);


                return FullName;
            }





        }

        public interface IFileObject
        {
            bool SavePath(int Id, string FullName, FileTypeDetails fileType);
            string GetObjectName(int Id);
            List<Picture> GetObjectPictures(int Id);
            List<MusicVideo> GetObjectMusicVideos(int Id);
            string GetObjectMusicFiles(int Id);
        }



        
        public interface IFileObjectFactory
        {
            IFileObject BuildObject();
          
        }


        public class BandFactory : IFileObjectFactory
        {
            IRepository repo;
            public BandFactory(IRepository repository)
            {
                repo = repository;
            }

            public IFileObject BuildObject()
            {
                return new Band(repo);
            }
        }

        public class SongFactory : IFileObjectFactory
        {
            IRepository repo;
            public SongFactory(IRepository repository)
            {
                repo = repository;
            }

            public IFileObject BuildObject()
            {
                return new Song(repo);
            }
        }

        public class MusicGenreFactory : IFileObjectFactory
        {
            IRepository repo;
            public MusicGenreFactory(IRepository repository)
            {
                repo = repository;
            }

            public IFileObject BuildObject()
            {
                return new MusicGenre(repo);
            }
        }

        public class BandsRecordFactory : IFileObjectFactory
        {
            IRepository repo;
            public BandsRecordFactory(IRepository repository)
            {
                repo = repository;
            }

            public IFileObject BuildObject()
            {
                return new BandsRecord(repo);
            }
        }

    }

}

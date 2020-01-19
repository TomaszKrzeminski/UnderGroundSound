using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UndergroundSound.Models
{
    public class AddFileViewModel
    {
        public AddFileViewModel()
        {
            FileDetails = new FileTypeDetails();
        }

        public int ObjectId { get; set; }

        [Required(ErrorMessage ="Nie wybrałeś pliku")]
        public IFormFile FileToSave { get; set; }

        [Required]
        public string ObjectType { get; set; }
       
        public FileTypeDetails FileDetails { get; set; }

        
    }

    public class FileTypeDetails
    {
        public FileType filetype { get; set; }
        public string MethodName { get; set; }
        public string DirectoryToSave { get; set; }
        public List<string> ExtensionList { get; set; }
        private List<string> ExtensionListPicture { get; set; }
        private List<string> ExtensionListMusicVideo { get; set; }
        private List<string> ExtensionListMusicFile { get; set; }


        public FileTypeDetails()
        {

        }

        public FileTypeDetails(FileType filetype)
        {
            this.filetype = filetype;
            ExtensionList = new List<string>();
            ExtensionListMusicFile = new List<string> { ".wav", ".mp3" };
            ExtensionListMusicVideo = new List<string> { ".mp4", ".avi", ".wmv" };
            ExtensionListPicture = new List<string> { ".jpg", ".png", ".gif" };
            SetExtensionList(filetype);
            SetDirectoryToSave(filetype);
            SetMethodName(filetype);
        }

        private void SetExtensionList(FileType filetype)
        {

            switch (filetype)
            {
                case FileType.musicfile:
                    ExtensionList = ExtensionListMusicFile;
                    break;
                case FileType.picture:
                    ExtensionList = ExtensionListPicture;
                    break;
                case FileType.musicvideo:
                    ExtensionList = ExtensionListMusicVideo;
                    break;
                default:
                    break;
            }

        }

        private void SetDirectoryToSave(FileType filetype)
        {

            switch (filetype)
            {
                case FileType.musicfile:
                    DirectoryToSave = "Songs";
                    break;
                case FileType.picture:
                    DirectoryToSave = "Images";
                    break;
                case FileType.musicvideo:
                    DirectoryToSave = "MusicVideos";
                    break;
                default:
                    break;
            }

        }

        private void SetMethodName(FileType filetype)
        {

            switch (filetype)
            {
                case FileType.musicfile:
                    MethodName = "ShowMusicFile";
                    break;
                case FileType.picture:
                    MethodName = "ShowPictures";
                    break;
                case FileType.musicvideo:
                    MethodName = "ShowMusicVideo";
                    break;
                default:
                    break;
            }

        }


    }


    public enum FileType
    {
        picture, musicvideo, musicfile
    }

}
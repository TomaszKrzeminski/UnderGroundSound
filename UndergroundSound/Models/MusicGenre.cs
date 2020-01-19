using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UndergroundSound.Models.UndergroundSound.Models;

namespace UndergroundSound.Models
{
    public class MusicGenre: IFileObject
    {
        private IRepository repository;


        public MusicGenre()
        {
            Bands = new HashSet<Band>();
            Pictures = new HashSet<Picture>();
        }

        public MusicGenre(IRepository repository)
        {
            this.repository = repository;
            Bands = new HashSet<Band>();
            Pictures = new HashSet<Picture>();
        }

        public int MusicGenreId { get; set; }
        [Required]
        public string MusicGenreName { get; set; }
        [Required]
        public string MusicGenreDescription { get; set; }


        public virtual ICollection<Band> Bands { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }

        public string GetObjectMusicFiles(int Id)
        {
            throw new NotImplementedException();
        }

        public List<MusicVideo> GetObjectMusicVideos(int Id)
        {
            throw new NotImplementedException();
        }

        public string GetObjectName(int Id)
        {
            

            MusicGenre musicgenre = repository.GetMusicGenreById(Id);
            string MusicGenreName = musicgenre.MusicGenreName;
            return MusicGenreName;

        }

        public List<Picture> GetObjectPictures(int Id)
        {
            List<Picture> list = repository.GetMusicGenreById(Id).Pictures.ToList();
            return list;
        }

        public bool SavePath(int Id, string FullName, FileTypeDetails fileTypeDetails)
        {
                      
            bool value = repository.AddPictureToMusicGenre(Id, FullName);
            return value;
        }
    }



    public class Band : IFileObject
    {
        private IRepository repository;


        public Band()
        {
            BandsRecords = new HashSet<BandsRecord>();
            Votes = new HashSet<Vote>();
            AppUsers = new HashSet<AppUser>();
            Pictures = new HashSet<Picture>();
            Awards = new HashSet<Award>();
        }

        public Band(IRepository repository)
        {
            this.repository = repository;
            BandsRecords = new HashSet<BandsRecord>();
            Votes = new HashSet<Vote>();
            AppUsers = new HashSet<AppUser>();
            Pictures = new HashSet<Picture>();
            Awards = new HashSet<Award>();
        }

        public int BandId { get; set; }

        public string CreatorId { get; set; }


        [Required]
        public string BandName { get; set; }


        public string FormedPlace { get; set; }


        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Required]
        public DateTime FormedDate { get; set; }

        [Required]
        public string History { get; set; }



        public MusicGenre MusicGenre { get; set; }

        public virtual ICollection<BandsRecord> BandsRecords { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
        public virtual ICollection<AppUser> AppUsers { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }
        public virtual ICollection<Award> Awards { get; set; }

        public Place place { get; set; }

        public string GetObjectMusicFiles(int Id)
        {
            throw new NotImplementedException();
        }

        public List<MusicVideo> GetObjectMusicVideos(int Id)
        {
            throw new NotImplementedException();
        }

        public string GetObjectName(int Id)
        {
            Band band = repository.GetBandById(Id);

            string BandName = band.BandName;

            return BandName;
        }

        public List<Picture> GetObjectPictures(int Id)
        {
            List<Picture> list = repository.GetBandById(Id).Pictures.ToList();
            return list;
        }

        public bool SavePath(int Id, string FullName, FileTypeDetails fileTypeDetails)
        {
            bool value = repository.AddPictureToBand(Id, FullName);
            return value;
        }
    }

    public class BandsRecord : IFileObject
    {
        private IRepository repository;


        public BandsRecord()
        {
            Songs = new HashSet<Song>();
            Votes = new HashSet<Vote>();
            Pictures = new HashSet<Picture>();
            Awards = new HashSet<Award>();
        }



        public BandsRecord(IRepository repository)
        {
            this.repository = repository;
            Songs = new HashSet<Song>();
            Votes = new HashSet<Vote>();
            Pictures = new HashSet<Picture>();
            Awards = new HashSet<Award>();
        }

        public int BandsRecordId { get; set; }
        [Required]
        public DateTime ReleaseOfTheCd { get; set; }
        [Required]
        public string BandsRecordName { get; set; }

                     




        public Band Band { get; set; }

        public virtual ICollection<Song> Songs { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }
        public virtual ICollection<Award> Awards { get; set; }

        public string GetObjectMusicFiles(int Id)
        {
            throw new NotImplementedException();
        }

        public List<MusicVideo> GetObjectMusicVideos(int Id)
        {
            throw new NotImplementedException();
        }

        public string GetObjectName(int Id)
        {
            BandsRecord bandsrecord = repository.GetBandsRecordById(Id);
            
            string BandsRecordName = bandsrecord.BandsRecordName;

            return BandsRecordName;
        }

        public List<Picture> GetObjectPictures(int Id)
        {
            List<Picture> list = repository.GetBandsRecordById(Id).Pictures.ToList();
            return list;
        }

        public bool SavePath(int Id, string FullName,FileTypeDetails fileTypeDetails)
        {
            bool value = repository.AddPictureToBandsRecord(Id, FullName);
            return value;
        }
    }


    public class Song : IFileObject
    {
        private IRepository repository;


        public Song()
        {
            Votes = new HashSet<Vote>();
            Pictures = new HashSet<Picture>();
            Awards = new HashSet<Award>();
            MusicVideos = new HashSet<MusicVideo>();
        }


        public Song(IRepository repository)
        {
            this.repository = repository;
            Votes = new HashSet<Vote>();
            Pictures = new HashSet<Picture>();
            Awards = new HashSet<Award>();
            MusicVideos = new HashSet<MusicVideo>();

        }

        public int SongId { get; set; }
        [Required]
        public string SongName { get; set; }
        public string SongText { get; set; }

        public string Path { get; set; }


        public BandsRecord BandsRecord { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }
        public virtual ICollection<MusicVideo> MusicVideos { get; set; }
        public virtual ICollection<Award> Awards { get; set; }

        public string GetObjectMusicFiles(int Id)
        {
            Song song = repository.GetSongById(Id);

            if(song.Path!=null)
            {
                return song.Path;
            }
            else
            {
                return "No Files";
            }

         
        }

        public List<MusicVideo> GetObjectMusicVideos(int Id)
        {
            List<MusicVideo> list = repository.GetSongById(Id).MusicVideos.ToList();
            return list;
        }

        public string GetObjectName(int Id)
        {
            Song song = repository.GetSongById(Id);

            string songName = song.SongName;

            return songName;
        }

        public List<Picture> GetObjectPictures(int Id)
        {
            List<Picture> list = repository.GetSongById(Id).Pictures.ToList();
            return list;
        }

        public bool SavePath(int Id, string FullName, FileTypeDetails fileTypeDetails)
        {

            bool value = false;
            switch (fileTypeDetails.filetype)
            {
                case FileType.musicfile:
                  value= repository.AddPathToSong(Id, FullName);
                    value = true;
                    break;
                case FileType.picture:
                    value = repository.AddPictureToSong(Id, FullName);
                    break;
                case FileType.musicvideo:
                    value = repository.AddMusicVideoToSong(Id, FullName);
                    break;
                default:
                    break;
            }




            
            return value;
        }

        
    }



    public class Vote
    {
        public int VoteId { get; set; }

        public Band Band { get; set; }
        public BandsRecord BandsRecord { get; set; }
        public Song Song { get; set; }
        //public AppUser AppUser { get; set; }

        public string UserId { get; set; }



    }


    public class Picture
    {
        public int PictureId { get; set; }
        public string PicturePath { get; set; }

        public Band Band { get; set; }
        public Song Song { get; set; }
        public MusicGenre MusicGenre { get; set; }


    }


    public class MusicVideo
    {
        public int MusicVideoId { get; set; }
        public string MusicVideoPath { get; set; }


        public Song Song { get; set; }


    }



    public class Award
    {
        public int AwardId { get; set; }

        public string AwardName { get; set; }

        public string AwardDescription { get; set; }
    }




    public class Place
    {



        public int PlaceId { get; set; }

        public string Province { get; set; }
        public string County { get; set; }





        public Band Band { get; set; }
        public int BandId { get; set; }



    }

    public class ProvincePoland
    {
        public ProvincePoland()
        {
            Countys = new HashSet<CountyPoland>();
        }
        public int ProvincePolandId { get; set; }
        public string ProvinceName { get; set; }
        public virtual ICollection<CountyPoland> Countys { get; set; }
    }

    public class CountyPoland
    {
        public CountyPoland()
        {
            Cities = new HashSet<CityPoland>();
        }
        public int CountyPolandId { get; set; }
        public string CountyName { get; set; }
        public virtual ICollection<CityPoland> Cities { get; set; }
        public ProvincePoland ProvincePoland { get; set; }






    }

    public class CityPoland
    {
        public int CityPolandId { get; set; }
        public string CityName { get; set; }
        public CountyPoland CountryPoland { get; set; }
    }



}
using MaxMind.GeoIP2;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace UndergroundSound.Models
{
    public class Repository : IRepository
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private AppIdentityDbContext context;
        public Repository(AppIdentityDbContext ctx, IHostingEnvironment hostingEnvironment)
        {
            context = ctx;
            _hostingEnvironment = hostingEnvironment;
        }

        public IQueryable<MusicGenre> MusicGenres
        {
            get
            {
                if (context.MusicGenres.Any())
                {
                    return context.MusicGenres;
                }
                else
                {
                    return new List<MusicGenre>().AsQueryable();
                }



            }
        }

        public IQueryable<Band> Bands
        {
            get
            {
                if (context.Bands.Any())
                {
                    return context.Bands;
                }
                else
                {
                    return new List<Band>().AsQueryable();
                }



            }
        }




        public IQueryable<BandsRecord> BandsRecords => throw new NotImplementedException();

        public IQueryable<Song> Songs => throw new NotImplementedException();

        public IQueryable<Vote> Votes => throw new NotImplementedException();

        public IQueryable<Picture> Pictures => throw new NotImplementedException();

        public IQueryable<MusicVideo> MusicVideos => throw new NotImplementedException();

        public IQueryable<Award> Awards => throw new NotImplementedException();

        public IQueryable<Place> Places => throw new NotImplementedException();



        public bool AddBand(AddBandViewModel model)
        {




            try
            {

                Place place = new Place() { County = model.Place.County, Province = model.Place.Province };


                Band band = new Band { BandName = model.BandName, FormedDate = model.FormedDate, FormedPlace = model.FormedPlace, History = model.History, CreatorId = model.AppUserId };
                AppUser user = context.Users.Find(model.AppUserId);
                band.AppUsers.Add(user);

                context.Bands.Add(band);
                context.SaveChanges();
                band.place = place;
                context.SaveChanges();

                MusicGenre musicGenre = context.MusicGenres.Where(m => m.MusicGenreName == model.MusicGenre).First();
                musicGenre.Bands.Add(band);
                context.SaveChanges();



                return true;
            }
            catch (Exception ex)
            {
                return false;
            }






        }

        public bool AddMusicGenre(MusicGenre value)
        {
            try
            {
                context.MusicGenres.Add(value);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddPathToSong(int Id, string Path)
        {
            try
            {
                Song song = context.Songs.Find(Id);
                song.Path = Path;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }



        }

        public bool AddRecordToBand(AddRecordViewModel model)
        {

            try
            {

                Band band = context.Bands.Find(model.BandId);
                band.BandsRecords.Add(model.bandsrecord);
                context.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }


        }

        public bool AddSongToBandsRecord(AddSongViewModel model)
        {

            try
            {

                BandsRecord record = context.BandsRecords.Find(model.BandsRecordId);
                record.Songs.Add(model.song);
                context.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }




        }

        public bool AddUserToBand(int BandId, string UserId)
        {

            try
            {
                AppUser user = context.Users.Find(UserId);
                Band band = context.Bands.Find(BandId);

                foreach (var item in band.AppUsers)
                {
                    if (item == user)
                    {
                        return true;
                    }
                }

                band.AppUsers.Add(user);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }



        }

        public bool AddVoteDateAndDateCount(string UserId)
        {


            try
            {
                AppUser user = context.Users.Find(UserId);
                user.Last_Day_When_Votes_Added = DateTime.Now;
                user.VoteCount = 2;
                context.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }



        }


        bool CheckIfCanVote(string UserId)
        {
            int value = context.Users.Find(UserId).VoteCount;

            if (value > 0)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        public bool RemoveOneVoteFromUser(string UserId)
        {
            try
            {
                AppUser user = context.Users.Find(UserId);
                user.VoteCount = user.VoteCount - 1;
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool AddVoteToBandsRecord(BandsRecord bandsrecord, string UserId)
        {


            try
            {

                if (CheckIfCanVote(UserId))
                {
                    Vote voteToAdd = new Vote();
                    BandsRecord bandsRecord = context.BandsRecords.Find(bandsrecord.BandsRecordId);
                    voteToAdd.UserId = UserId;
                    bandsRecord.Votes.Add(voteToAdd);

                    context.SaveChanges();
                    RemoveOneVoteFromUser(UserId);
                    return true;
                }
                else
                {
                    return false;
                }




            }
            catch (Exception ex)
            {

                return false;


            }




        }

        public List<BandsRecord> BandsRecordByBandId(int Id)
        {
            try
            {
                Band band = context.Bands.Include("BandsRecords").Where(b => b.BandId == Id).First();
                List<BandsRecord> list = band.BandsRecords.ToList();

                if (list != null)
                {
                    return list;
                }
                else
                {
                    return new List<BandsRecord>();
                }

            }
            catch (Exception ex)
            {
                return new List<BandsRecord>();
            }
        }

        public bool ChangeBand(Band band)
        {
            try
            {
                Band bandToChange = context.Bands.Find(band.BandId);
                bandToChange.BandName = band.BandName;
                bandToChange.FormedDate = band.FormedDate;
                bandToChange.FormedPlace = band.FormedPlace;
                bandToChange.History = band.History;

                context.SaveChanges();
                return true;


            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public int ChangeBandsRecord(BandsRecord record)
        {
            try
            {
                BandsRecord record_to_change = context.BandsRecords.Include("Band").Where(r => r.BandsRecordId == record.BandsRecordId).First();
                record_to_change.BandsRecordName = record.BandsRecordName;
                record_to_change.ReleaseOfTheCd = record.ReleaseOfTheCd;
                context.SaveChanges();
                return record_to_change.Band.BandId;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int ChangeSong(Song song)
        {
            Song song_to_change = context.Songs.Include("BandsRecord").Where(s => s.SongId == song.SongId).First();
            song_to_change.SongName = song.SongName;
            song_to_change.SongText = song.SongText;
            context.SaveChanges();
            return song_to_change.BandsRecord.BandsRecordId;

        }

        public bool CheckAndUpdateVoteDate(string UserId)
        {

            try
            {
                AppUser user = context.Users.Find(UserId);
                DateTime date = user.Last_Day_When_Votes_Added;
                double totalhours = DateTime.Now.Subtract(date).TotalHours;

                if (totalhours >= 24)
                {
                    user.VoteCount = 2;
                    user.Last_Day_When_Votes_Added = DateTime.Now;
                    context.SaveChanges();
                    return true;


                }

                return false;


            }
            catch (Exception ex)
            {
                return false;
            }








        }

        public CheckAvailableVotes CheckVotesData(string UserId)
        {
            try
            {
                CheckAvailableVotes model = new CheckAvailableVotes();
                AppUser user = context.Users.Find(UserId);
                model.AvailableVotes = user.VoteCount;
                DateTime date = user.Last_Day_When_Votes_Added;
                double totalhours = DateTime.Now.Subtract(date).TotalHours;
                model.HoursToUpdate = (int)totalhours;
                return model;

            }
            catch (Exception ex)
            {

                return null;

            }
        }

        //Old Version
        //public Band GetBandById(int id)
        //{
        //    try
        //    {
        //        Band band = context.Bands.Include("MusicGenre").Where(x => x.BandId == id).First();
        //        return band;
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Band();
        //    }
        //}

        public Band GetBandById(int id)
        {
            try
            {
                //Band band = context.Bands.Where(x => x.BandId == id).Include(m => m.MusicGenre).Include(m => m.Votes).Include("BandsRecords.Votes").Include("BandsRecords.Songs").Include("BandsRecords.Songs.Votes").First();
                Band band = context.Bands.Where(x => x.BandId == id).Include(p=>p.Pictures).Include(m => m.MusicGenre).Include(m => m.Votes).Include("BandsRecords.Votes").Include("BandsRecords.Songs").Include("BandsRecords.Songs.Votes").First();

                return band;
            }
            catch (Exception ex)
            {
                return new Band();
            }
        }

        public List<Band> GetBandsCreatedByUser(string UserId)
        {

            List<Band> list = context.Bands.Where(b => b.CreatorId == UserId).ToList();
            if (list != null)
            {
                return list;
            }
            else
            {
                return new List<Band>();
            }


        }

        public BandsRecord GetBandsRecordById(int Id)
        {

            try
            {
                BandsRecord record = context.BandsRecords.Include("Band").Include(p=>p.Pictures).Where(b => b.BandsRecordId == Id).First();
                return record;
            }
            catch (Exception ex)
            {
                return null;
            }





        }

        public int GetBandsRecordIdBySongId(int Id)
        {

            try
            {
                Song song = context.Songs.Include("BandsRecord").Where(s => s.SongId == Id).First();
                int BandId = song.BandsRecord.BandsRecordId;
                return BandId;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public Band GetBandWithUsers(int id)
        {
            try
            {
                Band band = context.Bands.Include("AppUsers").Where(x => x.BandId == id).First();
                return band;
            }
            catch (Exception ex)
            {
                return new Band();
            }
        }

        public FindViewModel GetFindResults(string Name)
        {

            try
            {
                //List<Band> Bands = context.Bands.Where(b => b.BandName.ToLower() == Name.ToLower()).ToList();
                //List<Song> Songs = context.Songs.Where(s => s.SongName.ToLower() == Name.ToLower()).ToList();
                //List<BandsRecord> BandsRecords = context.BandsRecords.Where(r => r.BandsRecordName.ToLower() == Name.ToLower()).ToList();

                List<Band> Bands = context.Bands.Where(b => b.BandName.ToLower().Contains(Name.ToLower())).ToList();
                List<Song> Songs = context.Songs.Where(s => s.SongName.ToLower().Contains(Name.ToLower())).ToList();
                List<BandsRecord> BandsRecords = context.BandsRecords.Where(r => r.BandsRecordName.ToLower().Contains(Name.ToLower())).ToList();


                FindViewModel model = new FindViewModel();
                model.Bands = Bands;
                model.Songs = Songs;
                model.BandsRecords = BandsRecords;

                return model;
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public Song GetSongById(int Id)
        {

            try
            {
                Song song = context.Songs.Include("BandsRecord.Band").Include(p=>p.Pictures).Include(m=>m.MusicVideos).Where(s => s.SongId == Id).First();
                
                return song;
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public List<Song> GetSongsByBandsRecordId(int Id)
        {

            try
            {

                BandsRecord record = context.BandsRecords.Include("Songs").Where(r => r.BandsRecordId == Id).First();
                List<Song> list = record.Songs.ToList();
                return list;

            }
            catch (Exception ex)
            {
                return null;
            }




        }

        public bool RemoveBand(int Id)
        {
            try
            {
                Band band = context.Bands.Include("AppUsers").Where(x => x.BandId == Id).First();


                foreach (var item in band.AppUsers)
                {
                    item.Band.AppUsers = null;

                }

                band.AppUsers = null;

                context.SaveChanges();

                context.Bands.Remove(band);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public BandsRecord RemoveBandsRecord(int Id)
        {


            try
            {
                BandsRecord recordRemoved = new BandsRecord();
                BandsRecord record = context.BandsRecords.Include("Band").Where(b => b.BandsRecordId == Id).First();
                Band band = context.Bands.Find(record.Band.BandId);
                recordRemoved.BandsRecordName = record.BandsRecordName;
                recordRemoved.BandsRecordId = record.Band.BandId;
                band.BandsRecords.Remove(record);
                context.SaveChanges();
                return recordRemoved;




            }
            catch (Exception ex)
            {
                return null;
            }





        }

        public bool RemoveMusicGenre(int Id)
        {

            try
            {
                MusicGenre musicgenre = context.MusicGenres.Find(Id);
                context.MusicGenres.Remove(musicgenre);
                context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }

        }

        public Song RemoveSong(int Id)
        {
            try
            {
                Song songRemoved = new Song();
                Song song = context.Songs.Include("BandsRecord").Where(b => b.SongId == Id).First();
                BandsRecord bandsrecord = context.BandsRecords.Find(song.BandsRecord.BandsRecordId);
                songRemoved.SongName = song.SongName;
                songRemoved.SongId = song.BandsRecord.BandsRecordId;
                bandsrecord.Songs.Remove(song);
                context.SaveChanges();
                return songRemoved;




            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public bool RemoveUserFromBand(string UserId, int BandId)
        {
            try
            {
                AppUser user = context.Users.Find(UserId);
                Band band = context.Bands.Find(BandId);
                band.AppUsers.Remove(user);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool AddVoteToSong(Song song, string UserId)
        {

            try
            {

                if (CheckIfCanVote(UserId))
                {
                    Vote voteToAdd = new Vote();
                    Song Song = context.Songs.Find(song.SongId);
                    voteToAdd.UserId = UserId;
                    Song.Votes.Add(voteToAdd);

                    context.SaveChanges();
                    RemoveOneVoteFromUser(UserId);
                    return true;
                }
                else
                {
                    return false;
                }




            }
            catch (Exception ex)
            {

                return false;


            }





        }


        public List<Song> PopulateSongListIfNecessarily(List<Song> list)
        {
            List<Song> Songs = new List<Song>();
            Songs.AddRange(list);

            if (list.Count < 5)
            {
                int count = 5 - list.Count;

                for (int i = 0; i < count; i++)
                {
                    Songs.Add(new Song() { SongName = "Brak" });
                }

                return Songs;
            }
            else
            {
                return Songs;
            }
        }

        public List<BandsRecord> PopulateBandsRecordListIfNecessarily(List<BandsRecord> list)
        {
            List<BandsRecord> BandsRecord = new List<BandsRecord>();
            BandsRecord.AddRange(list);

            if (list.Count < 5)
            {
                int count = 5 - list.Count;

                for (int i = 0; i < count; i++)
                {
                    BandsRecord.Add(new BandsRecord() { BandsRecordName = "Brak" });
                }

                return BandsRecord;
            }
            else
            {
                return BandsRecord;
            }
        }


        public List<Song> GetHighestRatedSongs(string Province = "Brak", string County = "Brak")
        {

            List<Song> Songs = new List<Song>();

            try
            {
                if (Province == "Brak")
                {
                    Songs = context.Songs.Include(b=>b.BandsRecord.Band.MusicGenre).Include("Votes").OrderByDescending(v => v.Votes.Count).Take(10).ToList();


                }
                else if (Province != "Brak" && County == "Brak")
                {

                    List<Band> BandsList = context.Bands.Include(m=>m.MusicGenre).Include(b => b.BandsRecords).Where(p => p.place.Province == Province).ToList();
                    List<BandsRecord> BandsRecordList = BandsList.SelectMany(r => r.BandsRecords).ToList();
                    Songs = BandsRecordList.SelectMany(s => s.Songs).OrderByDescending(v => v.Votes.Count).Take(10).ToList();


                }
                else if (County != "Brak")
                {
                    Songs = context.Songs.Where(p => p.BandsRecord.Band.place.County == County).OrderByDescending(v => v.Votes.Count).Take(10).ToList();
                }

                return PopulateSongListIfNecessarily(Songs);



            }
            catch (Exception ex)
            {
                return new List<Song>();
            }
        }

        public List<Song> GetNewestsSongs(string Province = "Brak", string County = "Brak")
        {
            List<Song> Songs = new List<Song>();
            try
            {
                if (Province == "Brak")
                {
                  Songs = context.Songs.Include(b => b.BandsRecord.Band.MusicGenre).OrderByDescending(d => d.BandsRecord.ReleaseOfTheCd).Take(10).ToList();


                }
                else if (Province != "Brak" && County == "Brak")
                {
                  Songs = context.Songs.Include(b => b.BandsRecord.Band.MusicGenre).Where(p => p.BandsRecord.Band.place.Province == Province).OrderByDescending(d => d.BandsRecord.ReleaseOfTheCd).Take(10).ToList();

                }
                else if (County != "Brak")
                {
                    Songs = context.Songs.Include(b => b.BandsRecord.Band.MusicGenre).Where(p => p.BandsRecord.Band.place.County == County).OrderByDescending(s => s.BandsRecord.ReleaseOfTheCd).Take(10).ToList();
                }
                return PopulateSongListIfNecessarily(Songs);
            }
            catch (Exception ex)
            {
                return new List<Song>();
            }
        }

        public List<BandsRecord> GetHighestRatedBandsRecords(string Province = "Brak", string County = "Brak")
        {
            List<BandsRecord> BandsRecord = new List<BandsRecord>();
            try
            {
                if (Province == "Brak")
                {
                    BandsRecord = context.BandsRecords.Include(m=>m.Band.MusicGenre).Include("Votes").OrderByDescending(d => d.Votes.Count).Take(10).ToList();

                }
                else if (Province != "Brak" && County == "Brak")
                {

                    List<Band> BandsList = context.Bands.Include(m=>m.MusicGenre).Include(b => b.BandsRecords).Where(p => p.place.Province == Province).ToList();
                    List<BandsRecord> BandsRecordList = BandsList.SelectMany(r => r.BandsRecords).ToList();
                    BandsRecord = BandsRecordList.OrderByDescending(v => v.Votes.Count).Take(10).ToList();


                }
                else if (County != "Brak")
                {
                    BandsRecord = context.BandsRecords.Include(m=>m.Band.MusicGenre).Where(b => b.Band.place.County == County).OrderByDescending(v => v.Votes.Count).Take(10).ToList();
                }
                return PopulateBandsRecordListIfNecessarily(BandsRecord);

            }
            catch (Exception ex)
            {
                return new List<BandsRecord>();
            }
        }

        public List<BandsRecord> GetNewestsBandsRecords(string Province = "Brak", string County = "Brak")
        {
            List<BandsRecord> BandsRecord = new List<BandsRecord>();
            try
            {
                if (Province == "Brak")
                {
                    BandsRecord = context.BandsRecords.Include(m=>m.Band.MusicGenre).OrderByDescending(d => d.ReleaseOfTheCd).Take(10).ToList();

                }
                else if (Province != "Brak" && County == "Brak")
                {
                    List<Band> BandsList = context.Bands.Include(m=>m.MusicGenre).Include(b => b.BandsRecords).Where(p => p.place.Province == Province).ToList();
                    List<BandsRecord> BandsRecordList = BandsList.SelectMany(r => r.BandsRecords).ToList();
                    BandsRecord = BandsRecordList.OrderByDescending(d => d.ReleaseOfTheCd).Take(10).ToList();


                }
                else if (County != "Brak")
                {
                    BandsRecord = context.BandsRecords.Include(m=>m.Band.MusicGenre).Where(b => b.Band.place.County == County).OrderByDescending(d => d.ReleaseOfTheCd).Take(10).ToList();
                }
                return PopulateBandsRecordListIfNecessarily(BandsRecord);

            }
            catch (Exception ex)
            {
                return new List<BandsRecord>();
            }
        }

        public UserLocationDetails GetUserLocationDetails(IPAddress IpAddress)
        {

            UserLocationDetails Model = new UserLocationDetails();
            Model.Ip = IpAddress.ToString();

            try
            {
                using (var reader = new DatabaseReader(_hostingEnvironment.ContentRootPath + "\\GeoLite2-City.mmdb"))
                {
                    var city = reader.City(IpAddress).City;
                    Model.City = city.Name;
                    CityPoland cityPoland = context.Cities.Include(c => c.CountryPoland).Include(p => p.CountryPoland.ProvincePoland).Where(n => n.CityName == Model.City).First();
                    Model.County = cityPoland.CountryPoland.CountyName;
                    Model.Province = cityPoland.CountryPoland.ProvincePoland.ProvinceName;
                    return Model;
                }

            }
            catch
            {
                Model.City = "Not Found";
                Model.County = "Not Found";
                Model.Province = "Not Fount";
                return Model;
            }


        }

        public List<ProvincePoland> GetAllProvinces()
        {
            List<ProvincePoland> listProvince= new List<ProvincePoland>();
            try
            {
                listProvince = context.ProvincesPoland.Include(c => c.Countys).ToList();
                return listProvince;
            }
            catch (Exception ex)
            {
                return listProvince;
            }
        }

        public ProvincePoland GetProvinceByCountyId(int Id)
        {
            ProvincePoland province;
            try
            {
                province = context.CountysPoland.Include(p => p.ProvincePoland).Where(c => c.CountyPolandId == Id).First().ProvincePoland;
                return province;
            }
            catch(Exception ex)
            {
                return province=new ProvincePoland() {ProvincePolandId=0 };
            }
        }

        public List<Band> GetBandsByUserId(string UserId)
        {
            try
            {
                List<Band> BandsList = context.Users.Include(b => b.Band).Where(u => u.Id == UserId).Select(b => b.Band).ToList();
                BandsList.RemoveAll(x => x == null);
                return BandsList;
            }
            catch (Exception ex)
            {
                return new List<Band>();
            }

        }

        public bool AddPictureToBand(int Id, string FullName)
        {
            try
            {
                Picture picture = new Picture() { PicturePath = FullName };

                Band band = context.Bands.Find(Id);

                band.Pictures.Add(picture);

                context.SaveChanges();

                return true;
                               
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public MusicGenre GetMusicGenreById(int Id)
        {
            MusicGenre musicgenre = new MusicGenre();
            try
            {
                musicgenre = context.MusicGenres.Include(p => p.Pictures).Where(m => m.MusicGenreId == Id).First();
                return musicgenre;
            }
            catch
            {
                return musicgenre;
            }
        }

        public bool AddPictureToMusicGenre(int Id, string FullName)
        {
            try
            {
                Picture picture = new Picture() { PicturePath = FullName };

                MusicGenre musicgenre = context.MusicGenres.Find(Id);


                musicgenre.Pictures.Add(picture);

                context.SaveChanges();

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool AddPictureToSong(int Id, string FullName)
        {
            try
            {
                Picture picture = new Picture() { PicturePath = FullName };

                Song song = context.Songs.Find(Id);

                song.Pictures.Add(picture);

                context.SaveChanges();

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool AddPictureToBandsRecord(int Id, string FullName)
        {
            try
            {
                Picture picture = new Picture() { PicturePath = FullName };

                BandsRecord bandsrecord = context.BandsRecords.Find(Id);

                bandsrecord.Pictures.Add(picture);

                context.SaveChanges();

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool AddMusicVideoToSong(int Id,  string FullName)
        {
            try
            {
                Song song = context.Songs.Find(Id);
                MusicVideo musicvideo = new MusicVideo() {MusicVideoPath=FullName };
                song.MusicVideos.Add(musicvideo);
                context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
           
        }
    }
}

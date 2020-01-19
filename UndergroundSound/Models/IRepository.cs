using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UndergroundSound.Models
{
  public  interface IRepository
    {
        IQueryable<MusicGenre> MusicGenres { get; }
        IQueryable<Band> Bands { get; }
        IQueryable<BandsRecord> BandsRecords { get; }
        IQueryable<Song> Songs { get; }
        IQueryable<Vote> Votes { get; }
        IQueryable<Picture> Pictures { get; }
        IQueryable<MusicVideo> MusicVideos { get; }
        IQueryable<Award> Awards { get; }
        IQueryable<Place> Places { get; }

        List<Song> GetHighestRatedSongs(string Province = "Brak",string County="Brak");
        List<Song> GetNewestsSongs(string Province = "Brak", string County = "Brak");
        List<BandsRecord> GetHighestRatedBandsRecords(string Province="Brak", string County = "Brak");
        List<BandsRecord> GetNewestsBandsRecords(string Province="Brak", string County = "Brak");

        UserLocationDetails GetUserLocationDetails(System.Net.IPAddress IpAddress);

        List<ProvincePoland> GetAllProvinces();

        ProvincePoland GetProvinceByCountyId(int Id);

        //Province GetProvinceById(int Id);

        bool AddMusicGenre(MusicGenre value);
        bool RemoveMusicGenre(int Id);
        MusicGenre GetMusicGenreById(int Id);

        bool AddPictureToMusicGenre(int Id, string FullName);
        bool AddPictureToSong(int Id, string FullName);
        bool AddPictureToBandsRecord(int Id, string FullName);
        bool AddPictureToBand(int Id,string FullName);

        bool AddBand(AddBandViewModel model);

        List<Band> GetBandsCreatedByUser(string UserId);
        bool RemoveBand(int Id);
        Band GetBandById(int id);
        List<Band> GetBandsByUserId(string UserId);
        Band GetBandWithUsers(int id);
        bool ChangeBand(Band band);
        bool AddUserToBand(int BandId, string UserId);
        bool RemoveUserFromBand(string UserId, int BandId);
        List<BandsRecord> BandsRecordByBandId(int Id);
        bool AddRecordToBand(AddRecordViewModel model);
        BandsRecord RemoveBandsRecord(int Id);
        Song RemoveSong(int Id);
        BandsRecord GetBandsRecordById(int Id);
        int ChangeBandsRecord(BandsRecord record);
        List<Song> GetSongsByBandsRecordId(int Id);
        bool AddSongToBandsRecord(AddSongViewModel model);
        Song GetSongById(int Id);
        int ChangeSong(Song song);
        bool AddPathToSong(int Id,string Path);
        int GetBandsRecordIdBySongId(int Id);
        FindViewModel GetFindResults(string Name);
        bool AddVoteDateAndDateCount(string UserId);
        bool CheckAndUpdateVoteDate(string UserId);
        CheckAvailableVotes CheckVotesData(string UserId);
        bool AddVoteToBandsRecord(BandsRecord bandsrecord,string UserId);
        bool AddVoteToSong(Song song, string UserId);
        bool AddMusicVideoToSong(int Id,string FullName);
       
    }




}

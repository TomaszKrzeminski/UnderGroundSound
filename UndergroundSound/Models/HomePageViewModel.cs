using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UndergroundSound.Models
{
    public class HomePageViewModel
    {
        public string Province { get; set; }
        public string County { get; set; }
        public string City { get; set; }

        public List<MusicGenre> MusicGenres { get; set; }

        public List<Song> ListOfNewestsSongs { get; set; }
        public List<Song> ListOfHighestRatedSongs { get; set; }
        public List<BandsRecord> ListOfNewestsBandsRecord { get; set; }
        public List<BandsRecord> ListOfHighestRatedBandsRecord { get; set; }

        public List<Song> ListOfNewestsSongsLocalProvince { get; set; }
        public List<Song> ListOfHighestRatedSongsLocalProvince { get; set; }
        public List<BandsRecord> ListOfNewestsBandsRecordLocalProvince { get; set; }
        public List<BandsRecord> ListOfHighestRatedBandsRecordLocalProvince { get; set; }


        public List<Song> ListOfNewestsSongsLocalCounty { get; set; }
        public List<Song> ListOfHighestRatedSongsLocalCounty { get; set; }
        public List<BandsRecord> ListOfNewestsBandsRecordLocalCounty { get; set; }
        public List<BandsRecord> ListOfHighestRatedBandsRecordLocalCounty { get; set; }





      public  List<List<Song>> listCollectionSongs { get; set; }
     public   List<List<BandsRecord>> listCollectionBandsRecors { get; set; }




        public HomePageViewModel()
        {
            listCollectionSongs = new List<List<Song>>();
            listCollectionBandsRecors = new List<List<BandsRecord>>();
            
        }


      



        public void SetListCollections()
        {
            
            listCollectionSongs.Add(this.ListOfNewestsSongs);
            listCollectionSongs.Add(this.ListOfHighestRatedSongs);
            listCollectionSongs.Add(this.ListOfNewestsSongsLocalProvince);
            listCollectionSongs.Add(this.ListOfHighestRatedSongsLocalProvince);
            listCollectionSongs.Add(this.ListOfNewestsSongsLocalCounty);
            listCollectionSongs.Add(this.ListOfHighestRatedSongsLocalCounty);





            listCollectionBandsRecors.Add(ListOfNewestsBandsRecord);
            listCollectionBandsRecors.Add(ListOfHighestRatedBandsRecord);
            listCollectionBandsRecors.Add(ListOfNewestsBandsRecordLocalProvince);
            listCollectionBandsRecors.Add(ListOfHighestRatedBandsRecordLocalProvince);
            listCollectionBandsRecors.Add(ListOfNewestsBandsRecordLocalCounty);
            listCollectionBandsRecors.Add(ListOfHighestRatedBandsRecordLocalCounty);
        }



        //public List<List<Song>> SortSongLists(string MusicGenreName)
        //{
        //    List<List<Song>> listCollection = listCollectionSongs;
        //    List<Song> ListToReplace = new List<Song>();



        //    for (int i = 0; i < 6; i++)
        //    {
        //        ListToReplace.Clear();
        //        ListToReplace.AddRange(listCollection[i].Where(m => m.BandsRecord != null && m.BandsRecord.Band != null && m.BandsRecord.Band.MusicGenre.MusicGenreName == MusicGenreName).ToList());
        //        listCollection[i] = ListToReplace;

        //    }

        //    return listCollection;

        //}

        public void SortSongLists(string MusicGenreName)
        {
           



            for (int i = 0; i < 6; i++)
            {
               
               listCollectionSongs[i]= listCollectionSongs[i].Where(m => m.BandsRecord != null && m.BandsRecord.Band != null && m.BandsRecord.Band.MusicGenre.MusicGenreName == MusicGenreName).ToList();
              

            }

           

        }






        public void AddSongsIfNecessary()
    {
        List<List<Song>> listCollection = listCollectionSongs;

       
        foreach (var list in listCollection)
        {
            if (list.Count >= 5)
            {

            }
            else
            {
                int numberToAdd = list.Count;

                for (int i = numberToAdd; i < 5; i++)
                {
                    list.Add(new Song() { SongName = "Brak" });
                }

            }
        }


    }



    public void SortBandsRecordLists(string MusicGenreName)
    {
        List<List<BandsRecord>> listCollection = listCollectionBandsRecors;
            List<BandsRecord> ListToReplace = new List<BandsRecord>();





        for (int i = 0; i < 6; i++)
        {
                ListToReplace.Clear();
                ListToReplace.AddRange(listCollection[i].Where(m => m.Band != null && m.Band.MusicGenre.MusicGenreName == MusicGenreName).ToList());
                listCollection[i] = ListToReplace;
        }

            listCollectionBandsRecors = listCollection;

    }



    public void AddBandsRecordIfNecessary()
    {
        List<List<BandsRecord>> listCollection = listCollectionBandsRecors;



        foreach (var list in listCollection)
        {
            if (list.Count >= 5)
            {

            }
            else
            {
                int numberToAdd = list.Count;

                for (int i = numberToAdd; i < 5; i++)
                {
                    list.Add(new BandsRecord() { BandsRecordName = "Brak" });
                }

            }
        }


    }






}
}

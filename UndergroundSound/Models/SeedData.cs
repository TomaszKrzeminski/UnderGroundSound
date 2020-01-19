using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using UndergroundSound.Models;
using System.Collections.Generic;
using static CountyPoland;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace UndergroundSound.Models

{
    public static class SeedData
    {

        //public static void EnsurePopulated(IApplicationBuilder app)
        public static void EnsurePopulated(AppIdentityDbContext context)
        {


            context.Database.EnsureCreated();


            void AddSong(string BandsRecordName, Song song)
            {

                try
                {

                    BandsRecord record = context.BandsRecords.Where(b => b.BandsRecordName == BandsRecordName).First();
                    record.Songs.Add(song);
                    context.SaveChanges();


                }
                catch (Exception ex)
                {

                }




            }





            void AddRecordToBand(BandsRecord record, string BandName)
            {

                try
                {

                    Band band = context.Bands.Where(b => b.BandName == BandName).First();
                    band.BandsRecords.Add(record);
                    context.SaveChanges();


                }
                catch (Exception ex)
                {

                }


            }



            void SeedUser(string MName, string MEmail)
            {


                try
                {


                    var User = new AppUser
                    {
                        UserName = MName,                                              
                        Email = MEmail,
                        EmailConfirmed = false,
                        LockoutEnabled = true,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        NormalizedEmail=MEmail.ToUpper(),
                        NormalizedUserName=MName.ToUpper(),
                    };

                    if (!context.Users.Any(u => u.UserName == User.UserName))
                    {
                        var password = new PasswordHasher<AppUser>();
                        var hashed = password.HashPassword(User, "Sekret123@");
                        User.PasswordHash = hashed;
                        UserStore<AppUser> userStore;

                        userStore = new UserStore<AppUser>(context);

                        userStore.CreateAsync(User).Wait();


                    }
                    context.SaveChanges();

                }
                catch (Exception ex)
                {

                }






            }



            void AddUserToBand(string UserEmail, string BandName)
            {

                try
                {
                    AppUser user = context.Users.Where(u => u.Email == UserEmail).First();
                    Band band = context.Bands.Where(b => b.BandName == BandName).First();

                    band.AppUsers.Add(user);
                    context.SaveChanges();

                }
                catch (Exception ex)
                {

                }



            }

            void AddBand(string County, string Province, string BandName, DateTime FormedDate, string FormedPlace, string History, string CreatorEmail, string MusicGenre)
            {




                try
                {

                    Place place = new Place() { County = County, Province = Province };
                    AppUser user = context.Users.Where(u => u.Email == CreatorEmail).First();

                    Band band = new Band { BandName = BandName, FormedDate = FormedDate, FormedPlace = FormedPlace, History = History, CreatorId = user.Id };

                    band.AppUsers.Add(user);

                    context.Bands.Add(band);
                    context.SaveChanges();
                    band.place = place;
                    context.SaveChanges();

                    MusicGenre musicGenre = context.MusicGenres.Where(m => m.MusicGenreName == MusicGenre).First();
                    musicGenre.Bands.Add(band);
                    context.SaveChanges();




                }
                catch (Exception ex)
                {

                }






            }














            if (!context.ProvincesPoland.Any())
            {

                List<ProvincePoland> ProvinceList = new List<ProvincePoland>();
                ProvinceList.Add(new ProvincePoland { ProvinceName = "dolnośląskie" });
                ProvinceList.Add(new ProvincePoland { ProvinceName = "kujawsko-pomorskie" });
                ProvinceList.Add(new ProvincePoland { ProvinceName = "lubelskie" });
                ProvinceList.Add(new ProvincePoland { ProvinceName = "łódzkie" });
                ProvinceList.Add(new ProvincePoland { ProvinceName = "małopolskie" });
                ProvinceList.Add(new ProvincePoland { ProvinceName = "mazowieckie" });
                ProvinceList.Add(new ProvincePoland { ProvinceName = "opolskie" });
                ProvinceList.Add(new ProvincePoland { ProvinceName = "podkarpackie" });
                ProvinceList.Add(new ProvincePoland { ProvinceName = "podlaskie" });
                ProvinceList.Add(new ProvincePoland { ProvinceName = "pomorskie" });
                ProvinceList.Add(new ProvincePoland { ProvinceName = "śląskie" });
                ProvinceList.Add(new ProvincePoland { ProvinceName = "świętokrzyskie" });
                ProvinceList.Add(new ProvincePoland { ProvinceName = "warmińsko-mazurskie" });
                ProvinceList.Add(new ProvincePoland { ProvinceName = "wielkopolskie" });
                ProvinceList.Add(new ProvincePoland { ProvinceName = "zachodniopomorskie" });
                ProvinceList.Add(new ProvincePoland { ProvinceName = "lubuskie" });

                context.ProvincesPoland.AddRange(ProvinceList);


                context.SaveChanges();











            }


            if (!context.CountysPoland.Any())
            {
                ProvincePoland Province_Dolnośląskie = context.ProvincesPoland.Where(n => n.ProvinceName == "dolnośląskie").First();

                List<CountyPoland> Countys_Dolnośląskie = new List<CountyPoland>
              {
                new CountyPoland() { CountyName ="bolesławiecki" },
                new CountyPoland() { CountyName ="dzierżoniowski" },
                new CountyPoland() { CountyName ="głogowski" },
                new CountyPoland() { CountyName ="jaworski" },
                new CountyPoland() { CountyName ="Jelenia Góra" }
              };

                foreach (var county in Countys_Dolnośląskie)
                {
                    Province_Dolnośląskie.Countys.Add(county);
                }







                ProvincePoland Province_Kujawsko_Pomorskie = context.ProvincesPoland.Where(n => n.ProvinceName == "kujawsko-pomorskie").First();

                List<CountyPoland> Countys_Kujawsko_Pomorskie = new List<CountyPoland>
              {
                new CountyPoland() { CountyName ="aleksandrowski" },
                new CountyPoland() { CountyName ="brodnicki " },
                new CountyPoland() { CountyName ="bydgoski" },
                new CountyPoland() { CountyName = "chełmiński"},
                new CountyPoland() { CountyName ="golubsko-dobrzyński" }
              };

                foreach (var county in Countys_Kujawsko_Pomorskie)
                {
                    Province_Kujawsko_Pomorskie.Countys.Add(county);
                }








                ProvincePoland Province_Lubuskie = context.ProvincesPoland.Where(n => n.ProvinceName == "lubuskie").First();

                List<CountyPoland> Countys_Lubuskie = new List<CountyPoland>
              {
                new CountyPoland() { CountyName ="bialski" },
                new CountyPoland() { CountyName ="biłgorajski" },
                new CountyPoland() { CountyName ="chełmski" },
                new CountyPoland() { CountyName ="chrubieszowski" },
                new CountyPoland() { CountyName = "janowski" }
              };

                foreach (var county in Countys_Lubuskie)
                {
                    Province_Lubuskie.Countys.Add(county);
                }
                context.SaveChanges();




            }


            if (!context.Cities.Any())
            {
                CountyPoland County_Chełmiński = context.CountysPoland.Where(n => n.CountyName == "chełmiński").First();


                List<CityPoland> Cities_Chełmiński = new List<CityPoland>()
                {

                    new CityPoland{CityName="Chełmno"},
                     new CityPoland{CityName="Dolne Wymiary"},
                      new CityPoland{CityName="Górne Wymiary"}
                };


                foreach (var city in Cities_Chełmiński)
                {
                    County_Chełmiński.Cities.Add(city);
                }

                CountyPoland County_Bydgoski = context.CountysPoland.Where(n => n.CountyName == "bydgoski").First();


                List<CityPoland> Cities_Bydgoski = new List<CityPoland>()
                {

                    new CityPoland{CityName="Bydgoszcz"},
                     new CityPoland{CityName="Osielsko"},
                      new CityPoland{CityName="Trzeciewiec"}
                };


                foreach (var city in Cities_Bydgoski)
                {
                    County_Bydgoski.Cities.Add(city);
                }


                context.SaveChanges();








            }





            if (!context.MusicGenres.Any())
            {

                List<MusicGenre> musicgenresList = new List<MusicGenre>();
                musicgenresList.Add(new MusicGenre { MusicGenreName = "Pop", MusicGenreDescription = "Lorem Ipsum jest tekstem stosowanym jako przykładowy wypełniacz w przemyśle poligraficznym. Został po raz pierwszy użyty w XV w. przez nieznanego drukarza do wypełnienia tekstem próbnej książki. Pięć wieków później zaczął być używany przemyśle elektronicznym, pozostając praktycznie niezmienionym. Spopularyzował się w latach 60. XX w. wraz z publikacją arkuszy Letrasetu, zawierających fragmenty Lorem Ipsum, a ostatnio z zawierającym różne wersje Lorem Ipsum oprogramowaniem przeznaczonym do realizacji druków na komputerach osobistych, jak Aldus PageMaker" });
                musicgenresList.Add(new MusicGenre { MusicGenreName = "Rock", MusicGenreDescription = "Lorem Ipsum jest tekstem stosowanym jako przykładowy wypełniacz w przemyśle poligraficznym. Został po raz pierwszy użyty w XV w. przez nieznanego drukarza do wypełnienia tekstem próbnej książki. Pięć wieków później zaczął być używany przemyśle elektronicznym, pozostając praktycznie niezmienionym. Spopularyzował się w latach 60. XX w. wraz z publikacją arkuszy Letrasetu, zawierających fragmenty Lorem Ipsum, a ostatnio z zawierającym różne wersje Lorem Ipsum oprogramowaniem przeznaczonym do realizacji druków na komputerach osobistych, jak Aldus PageMaker" });
                musicgenresList.Add(new MusicGenre { MusicGenreName = "HipHop/Rap", MusicGenreDescription = "Lorem Ipsum jest tekstem stosowanym jako przykładowy wypełniacz w przemyśle poligraficznym. Został po raz pierwszy użyty w XV w. przez nieznanego drukarza do wypełnienia tekstem próbnej książki. Pięć wieków później zaczął być używany przemyśle elektronicznym, pozostając praktycznie niezmienionym. Spopularyzował się w latach 60. XX w. wraz z publikacją arkuszy Letrasetu, zawierających fragmenty Lorem Ipsum, a ostatnio z zawierającym różne wersje Lorem Ipsum oprogramowaniem przeznaczonym do realizacji druków na komputerach osobistych, jak Aldus PageMaker" });
                musicgenresList.Add(new MusicGenre { MusicGenreName = "Disco", MusicGenreDescription = "Lorem Ipsum jest tekstem stosowanym jako przykładowy wypełniacz w przemyśle poligraficznym. Został po raz pierwszy użyty w XV w. przez nieznanego drukarza do wypełnienia tekstem próbnej książki. Pięć wieków później zaczął być używany przemyśle elektronicznym, pozostając praktycznie niezmienionym. Spopularyzował się w latach 60. XX w. wraz z publikacją arkuszy Letrasetu, zawierających fragmenty Lorem Ipsum, a ostatnio z zawierającym różne wersje Lorem Ipsum oprogramowaniem przeznaczonym do realizacji druków na komputerach osobistych, jak Aldus PageMaker" });
                musicgenresList.Add(new MusicGenre { MusicGenreName = "Muzyka Elektroniczna", MusicGenreDescription = "Lorem Ipsum jest tekstem stosowanym jako przykładowy wypełniacz w przemyśle poligraficznym. Został po raz pierwszy użyty w XV w. przez nieznanego drukarza do wypełnienia tekstem próbnej książki. Pięć wieków później zaczął być używany przemyśle elektronicznym, pozostając praktycznie niezmienionym. Spopularyzował się w latach 60. XX w. wraz z publikacją arkuszy Letrasetu, zawierających fragmenty Lorem Ipsum, a ostatnio z zawierającym różne wersje Lorem Ipsum oprogramowaniem przeznaczonym do realizacji druków na komputerach osobistych, jak Aldus PageMaker" });


                context.MusicGenres.AddRange(musicgenresList);



            }



            if (!context.Bands.Any())
            {
                SeedUser("USER1", "user1@example.com");
                AddBand("Poland", "kujawsko-pomorskie", "Zespół 1", new DateTime(2018, 8, 21), "Świecie", "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum", "user1@example.com", "HipHop/Rap");

                SeedUser("USER2", "user2@example.com");
                AddBand("Poland", "kujawsko-pomorskie", "Zespół 2", new DateTime(2018, 1, 21), "Chełmno", "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum", "user2@example.com", "Pop");

                SeedUser("USER1Zespół1", "user01@example.com");
                SeedUser("USER2Zespół1", "user02@example.com");
                SeedUser("USER1Zespół2", "user03@example.com");
                SeedUser("USER2Zespół2", "user04@example.com");

                AddUserToBand("user01@example.com", "Zespół 1");
                AddUserToBand("user02@example.com", "Zespół 1");
                AddUserToBand("user03@example.com", "Zespół 2");
                AddUserToBand("user04@example.com", "Zespół 2");

            }



            if (!context.BandsRecords.Any())
            {

                BandsRecord PłytaA = new BandsRecord();
                PłytaA.BandsRecordName = "Płyta z1 A";
                PłytaA.ReleaseOfTheCd = new DateTime(2018, 7, 1);

                BandsRecord PłytaB = new BandsRecord();
                PłytaB.BandsRecordName = "Płyta z1 B";
                PłytaB.ReleaseOfTheCd = new DateTime(2018, 8, 1);

                BandsRecord PłytaC = new BandsRecord();
                PłytaC.BandsRecordName = "Płyta z2 C";
                PłytaC.ReleaseOfTheCd = new DateTime(2018, 9, 1);

                BandsRecord PłytaD = new BandsRecord();
                PłytaD.BandsRecordName = "Płyta z2 D";
                PłytaD.ReleaseOfTheCd = new DateTime(2017, 7, 1);

                AddRecordToBand(PłytaA, "Zespół 1");
                AddRecordToBand(PłytaB, "Zespół 1");
                AddRecordToBand(PłytaC, "Zespół 2");
                AddRecordToBand(PłytaD, "Zespół 2");






            }


            if (!context.Songs.Any())
            {
                Song song1 = new Song() { SongName = "Name A", SongText = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum." };
                Song song2 = new Song() { SongName = "Name B", SongText = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum." };
                Song song3 = new Song() { SongName = "Name C", SongText = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum." };
                Song song4 = new Song() { SongName = "Name D", SongText = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum." };
                Song song5 = new Song() { SongName = "Name E", SongText = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum." };
                Song song6 = new Song() { SongName = "Name F", SongText = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum." };

                AddSong("Płyta z1 A", song1);
                AddSong("Płyta z1 A", song2);
                AddSong("Płyta z1 B", song3);
                AddSong("Płyta z2 C", song4);
                AddSong("Płyta z2 D", song5);
                AddSong("Płyta z2 D", song6);
                

            }



            context.SaveChanges();
        }
    }
}


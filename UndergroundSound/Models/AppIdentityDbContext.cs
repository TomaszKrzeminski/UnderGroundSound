using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace UndergroundSound.Models
{
    public class AppIdentityDbContext:IdentityDbContext<AppUser>
    {

        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext>options):base(options)
        {

        }

        public DbSet<MusicGenre> MusicGenres { get; set; }
        public DbSet<Band> Bands { get; set; }
        public DbSet<BandsRecord> BandsRecords { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<MusicVideo> MusicVideos { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<Place> Places { get; set; }

        public DbSet<CityPoland> Cities { get; set; }
        public DbSet<ProvincePoland> ProvincesPoland { get; set; }
        public DbSet<CountyPoland> CountysPoland { get; set; }
    }
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
        Cities = new HashSet<City>();
    }
    public int CountyPolandId { get; set; }
    public string CountyName { get; set; }
    public virtual ICollection<City> Cities { get; set; }
    public ProvincePoland ProvincePoland { get; set; }



    public class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public CountyPoland CountryPoland { get; set; }
    }
}
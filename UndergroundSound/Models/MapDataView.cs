using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UndergroundSound.Models
{
    public class MapDataView
    {
        IRepository repository;
        public MapDataView(IRepository _repo)
        {
            repository = _repo;
            GetProvinces();
        }


        public void GetProvinces()
        {
            ProvinceList = repository.GetAllProvinces();
        }


        public List<ProvincePoland> ProvinceList { get; set; }

    }
}









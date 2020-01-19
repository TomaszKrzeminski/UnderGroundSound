using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UndergroundSound.Models
{
    public class ProvinceDisplay:ViewComponent
    {

        public IViewComponentResult Invoke(string Name = "")
        {
            if (Name != "")
            {
                MapViewData data = new MapViewData();
                Province province = data.ProvinceList.Where(n => n.ProvinceName == Name).First();
                return View("CountySelectListView", province);
            }
            else
            {
               
                return View("CountySelectListView", new Province());
            }
        }



    }
}

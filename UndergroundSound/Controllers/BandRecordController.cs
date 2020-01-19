using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UndergroundSound.Models;

namespace UndergroundSound.Controllers
{
    public class BandRecordController : Controller
    {

        IRepository repository;
        UserManager<AppUser> UserMgr;
        public BandRecordController(IRepository repo, UserManager<AppUser> userMgr)
        {
            repository = repo;
            UserMgr = userMgr;
        }

        private Task<AppUser> GetCurrentUserAsync() => UserMgr.GetUserAsync(HttpContext.User);



        public IActionResult RecordList(int Id)
        {

            ViewBag.BandId = Id;
            List<BandsRecord> list = repository.BandsRecordByBandId(Id);
            return View(list);
        }


        public ActionResult AddRecord(int Id)
        {

            AddRecordViewModel model = new AddRecordViewModel();
            model.BandId = Id;

            return View(model);
        }


        [HttpPost]
        public ActionResult AddRecord(AddRecordViewModel model)
        {


            if (ModelState.IsValid)
            {
                bool value = repository.AddRecordToBand(model);
                if (value)
                {
                    return RedirectToAction("RecordList", new { Id = model.BandId });
                }
                else
                {
                    return View("AddRecord");
                }
            }
            else
            {
                return View("AddRecord");
            }





        }



        public ActionResult RemoveRecord(int Id)
        {

            BandsRecord record = repository.RemoveBandsRecord(Id);

            if (record != null)
            {
                return RedirectToAction("RecordList", new { Id = record.BandsRecordId });
            }
            else
            {
                return View("Error", $"Płyta  nie usunięta");
            }

        }



        public ActionResult EditRecord(int Id)
        {

            BandsRecord record = repository.GetBandsRecordById(Id);

            if (record != null)
            {
                return View(record);
            }
            else
            {
                return View("Error", $"Błąd przy edycji płyty");
            }




        }

        [HttpPost]
        public ActionResult EditRecord(BandsRecord record)
        {

            int value = repository.ChangeBandsRecord(record);

            if (value>0)
            {
              return  RedirectToAction("RecordList", new { Id = value });
            }
            else
            {
                return View("Error",$"Błąd przy zmianie płyty");
            }


        }














    }
}
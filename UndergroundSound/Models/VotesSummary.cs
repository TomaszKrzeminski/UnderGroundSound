using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System;
using System.Threading.Tasks;

namespace UndergroundSound.Models
{
    public class VotesSummary : ViewComponent
    {
        private IRepository repository;
        private UserManager<AppUser> userManager;
        public VotesSummary(IRepository repoparam, UserManager<AppUser> user)
        {
            repository = repoparam;
            userManager = user;
        }

        private Task<AppUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);

        public void Check_Votes_CountAsync()
        {

            AppUser user =  GetCurrentUserAsync().Result;

            if (user.Last_Day_When_Votes_Added == new DateTime(1, 1, 1, 0, 0, 0) && user.VoteCount == 0)
            {
                user.Last_Day_When_Votes_Added = DateTime.Now;
                user.VoteCount = 2;

                repository.AddVoteDateAndDateCount(user.Id);

            }

         bool check=    repository.CheckAndUpdateVoteDate(user.Id);

            if (check)
            {
                IdentityResult x =  userManager.UpdateAsync(user).Result;
            }


        }


        public IViewComponentResult Invoke()
        {


            Check_Votes_CountAsync();

            try
            {
                string text;
                AppUser User = userManager.GetUserAsync(HttpContext.User).Result;

                DateTime DateNow = DateTime.Now;
                DateTime LastUpdate = User.Last_Day_When_Votes_Added;
                int hours = (int)DateNow.Subtract(LastUpdate).Hours;

                text = $"<a class={"nav-item nav-link"} >Pozostało głosów {User.VoteCount} dostępne nowe za {24-hours} h </a>";

                return new HtmlContentViewComponentResult(new HtmlString(text));
            }
            catch (Exception ex)
            {
                return Content("<a>Brak Danych</a>");
            }






        }

    }
}

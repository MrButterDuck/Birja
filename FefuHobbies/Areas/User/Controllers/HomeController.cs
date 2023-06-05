using FefuHobbies.Domain;
using FefuHobbies.Models;
using Microsoft.AspNetCore.Mvc;

namespace FefuHobbies.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly DataManager dataManager;
        public HomeController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public IActionResult Index()
        {
            MainViewModel model = new MainViewModel();
            model.last = dataManager.Cards.GetCards().TakeLast(4);
            model.second = dataManager.Cards.GetCards().Take(4);
            return View(model);
        }
        public IActionResult About()
        {
            return View();
        }
    }
}

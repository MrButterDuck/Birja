using FefuHobbies.Domain;
using Microsoft.AspNetCore.Mvc;

namespace FefuHobbies.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly DataManager dataManager;
        public HomeController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public IActionResult Index()
        {
            return View(dataManager.Cards.GetCards());
        }
    }
}

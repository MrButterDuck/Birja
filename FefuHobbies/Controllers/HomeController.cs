using FefuHobbies.Domain.Entities;
using FefuHobbies.Domain;
using FefuHobbies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FefuHobbies.Controllers
{
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
            model.last = dataManager.Cards.Last().TakeLast(4);
            model.second = dataManager.Cards.ByType("Точка-интереса").Take(4);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Page(string keyWords, int page = 1)
        {
            int pageSize = 2;   // количество элементов на странице
            IQueryable<Card> source = dataManager.Cards.FindCards(keyWords, true);
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Cards = items
            };
            return View(viewModel);
        }
        public IActionResult About()
        {
            return View();
        }

    }
}

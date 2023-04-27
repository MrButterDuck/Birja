using FefuHobbies.Domain;
using FefuHobbies.Domain.Entities;
using FefuHobbies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IActionResult> Index()
        {
            int pageSize = 20;   // количество элементов на странице
            IQueryable<Card> source = dataManager.Cards.FindCards(" ", false);
            var count = await source.CountAsync();
            var items = await source.Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, 1, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Cards = items,
                keyWords = " "
            };
            return View(viewModel);
        }
    }
}

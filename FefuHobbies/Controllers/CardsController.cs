using FefuHobbies.Domain;
using FefuHobbies.Domain.Entities;
using FefuHobbies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FefuHobbies.Controllers
{
    public class CardsController : Controller
    {
        private readonly DataManager dataManager;
        public CardsController(DataManager dataManager)
        {
            this.dataManager = dataManager;
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
                Cards = items,
                keyWords = keyWords
            };
            return View(viewModel);


        }
        public IActionResult CardView(ulong id, string kWords = null, int p = default)
        {
            /*CardViewModel entity = new CardViewModel
            {
                card = dataManager.Cards.GetCardById(id),
                keyWords=kWords,
                page = p
            };*/
            return View();
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> PointsOfInterst(int page = 1)
        {
            int pageSize = 20;   // количество элементов на странице
            IQueryable<Card> source = dataManager.Cards.GetCards();
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Cards = items,
                keyWords = "Точка-интереса"
            };
            return View("Page", viewModel);
        }
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Events(int page = 1)
        {
            int pageSize = 20;   // количество элементов на странице
            IQueryable<Card> source = dataManager.Cards.GetCards();
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Cards = items
            };
            return View("Page", viewModel);
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Last(int page = 1)
        {
            int pageSize = 20;   // количество элементов на странице
            IQueryable<Card> source = dataManager.Cards.GetCards();
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Cards = items
            };
            return View("Page", viewModel);
        }
    }
}

using FefuHobbies.Domain;
using FefuHobbies.Domain.Entities;
using FefuHobbies.Models;
using FefuHobbies.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace FefuHobbies.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CardsController: Controller
	{
		private readonly DataManager dataManager;
		private readonly IWebHostEnvironment hostEnvironment;
		public CardsController(DataManager dataManager, IWebHostEnvironment hostEnvironment)
		{
			this.dataManager = dataManager;
			this.hostEnvironment = hostEnvironment;
		}

		public IActionResult Edit(ulong id)
		{
			var entity = id == default ? new Card() : dataManager.Cards.GetCardById(id);
			return View(entity);
		}

		[HttpPost]
		public IActionResult Edit(Card model, IFormFile titleImageFile)
		{
            if (ModelState.IsValid)
            {
                if (titleImageFile != null)
                {
                    model.ImagePath = titleImageFile.FileName;
                    using (var stream = new FileStream(Path.Combine(hostEnvironment.WebRootPath, "CardImages/", titleImageFile.FileName), FileMode.Create))
                    {
                        titleImageFile.CopyTo(stream);
                    }
                }
                if (model.startTime == null || model.endTime == null)
                {
                    model.startTime = "круглосуточно";
                    model.endTime = "круглосуточно";
                }
                if (model.Date == null)
                {
                    model.Date = "еженедельно";
                }
                dataManager.Cards.SaveCard(model);
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
            }
            return View(model);
        }

		[HttpPost]
		public IActionResult Delete(ulong id)
		{
			dataManager.Cards.DeleteCard(id);
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
        }

        public IActionResult CardView(ulong id, string kWords = null, int p = default)
        {
            CardViewModel entity = new CardViewModel
            {
                card = dataManager.Cards.GetCardById(id),
                keyWords = kWords,
                page = p
            };
            return View(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Page(string keyWords, int page = 1, bool isPublished = true)
		{
            int pageSize = 2;   // количество элементов на странице
            IQueryable<Card> source = dataManager.Cards.FindCards(keyWords, isPublished);
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

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> PointsOfInterst(int page = 1)
        {
            int pageSize = 20;   // количество элементов на странице
            IQueryable<Card> source = dataManager.Cards.ByType("Точка-интереса");
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
        public async Task<IActionResult> Events(int page = 1)
        {
            int pageSize = 20;   // количество элементов на странице
            IQueryable<Card> source = dataManager.Cards.excludeType("Точка-интереса");
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
            IQueryable<Card> source = dataManager.Cards.Last();
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
    }
}

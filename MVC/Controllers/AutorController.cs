using System.Threading.Tasks;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class AutorController : Controller
    {
        private readonly IAutorService _autorService;

        public AutorController(IAutorService autorService)
        {
            _autorService = autorService;
        }

        public async Task<IActionResult> Index(string search)
        {
            ViewBag.Search = search;
            return View(await _autorService.GetAllAsync(search));
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _autorService.GetByIdAsync(id));
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(AutorModel autorModel)
        {
            var autorId = await _autorService.AddAsync(autorModel);

            return RedirectToAction(nameof(Details), new { id = autorId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _autorService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AutorModel autorModel)
        {
            await _autorService.EditAsync(autorModel);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return View(await _autorService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(AutorModel autorModel)
        {
            await _autorService.RemoveAsync(autorModel);

            return RedirectToAction(nameof(Index));
        }
    }
}

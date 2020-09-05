using System.Threading.Tasks;
using Data.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class AutorController : Controller
    {
        private readonly IAutorRepository _autorRepository;

        public AutorController(IAutorRepository autorRepository)
        {
            _autorRepository = autorRepository;
        }

        public async Task<IActionResult> Index(string search)
        {
            ViewBag.Search = search;
            return View(await _autorRepository.GetAllAsync(search));
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _autorRepository.GetByIdAsync(id));
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(AutorModel autorModel)
        {
            var autorId = await _autorRepository.AddAsync(autorModel);

            return RedirectToAction(nameof(Details), new { id = autorId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _autorRepository.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AutorModel autorModel)
        {
            await _autorRepository.EditAsync(autorModel);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return View(await _autorRepository.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(AutorModel autorModel)
        {
            await _autorRepository.RemoveAsync(autorModel);

            return RedirectToAction(nameof(Index));
        }
    }
}

using System.Threading.Tasks;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class LivroController : Controller
    {
        private readonly ILivroService _livroService;

        public LivroController(ILivroService livroService)
        {
            _livroService = livroService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _livroService.GetAllAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _livroService.GetByIdAsync(id));
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(LivroModel livroModel)
        {
            var livroId = await _livroService.AddAsync(livroModel);

            return RedirectToAction(nameof(Details), new { id = livroId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _livroService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(LivroModel livroModel)
        {
            await _livroService.EditAsync(livroModel);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return View(await _livroService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(LivroModel livroModel)
        {
            await _livroService.RemoveAsync(livroModel);

            return RedirectToAction(nameof(Index));
        }
    }
}

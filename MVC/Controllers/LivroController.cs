using System.Threading.Tasks;
using Data.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class LivroController : Controller
    {
        private readonly ILivroRepository _livroRepository;

        public LivroController(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _livroRepository.GetAllAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _livroRepository.GetByIdAsync(id));
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(LivroModel livroModel)
        {
            var livroId = await _livroRepository.AddAsync(livroModel);

            return RedirectToAction(nameof(Details), new { id = livroId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _livroRepository.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(LivroModel livroModel)
        {
            await _livroRepository.EditAsync(livroModel);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return View(await _livroRepository.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(LivroModel livroModel)
        {
            await _livroRepository.RemoveAsync(livroModel);

            return RedirectToAction(nameof(Index));
        }
    }
}

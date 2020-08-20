﻿using Data.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class LivroController : Controller
    {
        private readonly LivroRepository _livroRepository;

        public LivroController(LivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        public IActionResult Index()
        {
            return View(_livroRepository.GetAll());
        }

        public IActionResult Details(int id)
        {
            return View(_livroRepository.GetById(id));
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(LivroModel livroModel)
        {
            _livroRepository.Add(livroModel);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(_livroRepository.GetById(id));
        }

        [HttpPost]
        public IActionResult Edit(LivroModel livroModel)
        {
            _livroRepository.Edit(livroModel);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(_livroRepository.GetById(id));
        }

        [HttpPost]
        public IActionResult Delete(LivroModel livroModel)
        {
            _livroRepository.Remove(livroModel);

            return RedirectToAction(nameof(Index));
        }
    }
}

﻿using System;

namespace MVC.Models
{
    public class LivroModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Isbn { get; set; }
        public DateTime Publicacao { get; set; }

        public int AutorId { get; set; }
        public AutorModel Autor { get; set; }
    }
}

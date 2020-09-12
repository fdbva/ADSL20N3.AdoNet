using Domain.Model.Models;

namespace MVC.Models
{
    public class LivroAutorCreateViewModel
    {
        public AutorModel Autor { get; set; }
        public LivroModel Livro { get; set; }

        public LivroAutorCreateModel ToModel()
        {
            return new LivroAutorCreateModel
            {
                Autor = Autor,
                Livro = Livro
            };
        }
    }
}

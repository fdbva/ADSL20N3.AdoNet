using System.Collections.Generic;
using System.Linq;
using Domain.Models;

namespace Data.Repositories
{
    public class LivroInMemoryRepository
    {
        public static List<LivroModel> Livros { get; } = new List<LivroModel>();

        public IEnumerable<LivroModel> GetAll()
        {
            return Livros;
        }

        public LivroModel GetById(int id)
        {
            var livro = Livros.First(x => x.Id == id);

            return livro;
        }

        public void Add(LivroModel livroModel)
        {
            Livros.Add(livroModel);
        }

        public void Edit(LivroModel livroModel)
        {
            var livroInMemory = GetById(livroModel.Id);

            livroInMemory.Isbn = livroModel.Isbn;
            livroInMemory.Publicacao = livroModel.Publicacao;
            livroInMemory.Titulo = livroModel.Titulo;
            livroInMemory.AutorId = livroModel.AutorId;
            livroInMemory.Autor = livroModel.Autor;
        }

        public void Remove(LivroModel livroModel)
        {
            var livroInMemory = GetById(livroModel.Id);

            Livros.Remove(livroInMemory);
        }
    }
}

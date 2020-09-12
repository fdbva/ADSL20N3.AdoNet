using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;

namespace Domain.Service
{
    public class LivroService : ILivroService
    {
        private readonly IAutorRepository _autorRepository;
        private readonly ILivroRepository _livroRepository;

        public LivroService(
            IAutorRepository autorRepository,
            ILivroRepository livroRepository)
        {
            _autorRepository = autorRepository;
            _livroRepository = livroRepository;
        }

        public async Task<IEnumerable<LivroModel>> GetAllAsync()
        {
            return await _livroRepository.GetAllAsync();
        }

        public async Task<LivroModel> GetByIdAsync(int id)
        {
            return await _livroRepository.GetByIdAsync(id);
        }

        public async Task<int> AddAsync(LivroAutorCreateModel livroAutorCreateModel)
        {
            if (livroAutorCreateModel.Livro.AutorId > 0)
            {
                return await _livroRepository.AddAsync(livroAutorCreateModel.Livro);
            }

            //Validação -> devemos validar regras de negócio!
            //var autor = livroAutorCreateModel.Autor;
            //if (string.IsNullOrWhiteSpace(autor.Nome) ||
            //    string.IsNullOrWhiteSpace(autor.UltimoNome) ||
            //    autor.Nascimento is null)
            //{
            //    throw new ArgumentNullException(nameof(livroAutorCreateModel.Autor), "Dados de autor não podem ser vazios!");
            //}


            //TODO: Pesquisar uma solução melhor de Transaction com ADO.NET e arquitetura em camadas!!
            SqlTransaction sqlTransaction = null;
            SqlConnection sqlConnection = null;
            try
            {
                int autorId;
                (autorId, sqlConnection, sqlTransaction) =
                    await _autorRepository.AddAsync(livroAutorCreateModel.Autor);

                livroAutorCreateModel.Livro.AutorId = autorId;

                var livroId =
                    await _livroRepository.AddAsync(livroAutorCreateModel.Livro, sqlConnection, sqlTransaction);

                sqlTransaction.Commit();

                return livroId;
            }
            catch (Exception e)
            {
                sqlTransaction?.Rollback();
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                if (sqlConnection != null)
                {
                    await sqlConnection.CloseAsync();
                    await sqlConnection.DisposeAsync();
                }

                if (sqlTransaction != null)
                {
                    await sqlTransaction.DisposeAsync();
                }
            }
        }

        public async Task EditAsync(LivroModel livroModel)
        {
            await _livroRepository.EditAsync(livroModel);
        }

        public async Task RemoveAsync(LivroModel livroModel)
        {
            await _livroRepository.RemoveAsync(livroModel);
        }
    }
}

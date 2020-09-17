using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Domain.Model.Interfaces.Context;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;

namespace Domain.Service
{
    public class LivroService : ILivroService
    {
        private readonly IAdoNetScopedContext _adoNetScopedContext;
        private readonly IAutorRepository _autorRepository;
        private readonly ILivroRepository _livroRepository;

        public LivroService(
            IAdoNetScopedContext adoNetScopedContext,
            IAutorRepository autorRepository,
            ILivroRepository livroRepository)
        {
            _adoNetScopedContext = adoNetScopedContext;
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

            await _adoNetScopedContext.BeginTransactionAsync();

            var autorId = await _autorRepository.AddAsync(livroAutorCreateModel.Autor);

            livroAutorCreateModel.Livro.AutorId = autorId;

            var livroId =
                await _livroRepository.AddAsync(livroAutorCreateModel.Livro);

            await _adoNetScopedContext.CommitAsync();

            return livroId;
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

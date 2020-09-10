using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;

namespace Domain.Service
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _livroRepository;

        public LivroService(
            ILivroRepository livroRepository)
        {
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

        public async Task<int> AddAsync(LivroModel livroModel)
        {
            return await _livroRepository.AddAsync(livroModel);
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

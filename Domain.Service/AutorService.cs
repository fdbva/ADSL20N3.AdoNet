using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;

namespace Domain.Service
{
    public class AutorService : IAutorService
    {
        private readonly IAutorRepository _autorRepository;

        public AutorService(
            IAutorRepository autorRepository)
        {
            _autorRepository = autorRepository;
        }

        public async Task<IEnumerable<AutorModel>> GetAllAsync(string search)
        {
            return await _autorRepository.GetAllAsync(search);
        }

        public async Task<AutorModel> GetByIdAsync(int id)
        {
            return await _autorRepository.GetByIdAsync(id);
        }

        public async Task<int> AddAsync(AutorModel autorModel)
        {
            var autorId = await _autorRepository.AddAsync(autorModel);

            return autorId;
        }

        public async Task EditAsync(AutorModel autorModel)
        {
            await _autorRepository.EditAsync(autorModel);
        }

        public async Task RemoveAsync(AutorModel autorModel)
        {
            await _autorRepository.RemoveAsync(autorModel);
        }
    }
}

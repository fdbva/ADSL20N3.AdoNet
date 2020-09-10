using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model.Models;

namespace Domain.Model.Interfaces.Services
{
    public interface ILivroService
    {
        Task<IEnumerable<LivroModel>> GetAllAsync();
        Task<LivroModel> GetByIdAsync(int id);
        Task<int> AddAsync(LivroModel autorModel);
        Task EditAsync(LivroModel autorModel);
        Task RemoveAsync(LivroModel autorModel);
    }
}

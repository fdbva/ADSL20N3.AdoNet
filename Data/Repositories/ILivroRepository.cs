using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Data.Repositories
{
    public interface ILivroRepository
    {
        Task<IEnumerable<LivroModel>> GetAllAsync();
        Task<LivroModel> GetByIdAsync(int id);
        Task<int> AddAsync(LivroModel autorModel);
        Task EditAsync(LivroModel autorModel);
        Task RemoveAsync(LivroModel autorModel);
    }
}

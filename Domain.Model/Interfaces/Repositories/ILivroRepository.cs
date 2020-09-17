using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model.Models;

namespace Domain.Model.Interfaces.Repositories
{
    public interface ILivroRepository
    {
        Task<IEnumerable<LivroModel>> GetAllAsync();
        Task<LivroModel> GetByIdAsync(int id);
        Task<int> AddAsync(
            LivroModel livroModel);
        Task EditAsync(LivroModel autorModel);
        Task RemoveAsync(LivroModel autorModel);
    }
}

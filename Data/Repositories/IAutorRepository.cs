using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Data.Repositories
{
    public interface IAutorRepository
    {
        Task<IEnumerable<AutorModel>> GetAllAsync(string search);
        Task<AutorModel> GetByIdAsync(int id);
        Task<int> AddAsync(AutorModel autorModel);
        Task EditAsync(AutorModel autorModel);
        Task RemoveAsync(AutorModel autorModel);
    }
}

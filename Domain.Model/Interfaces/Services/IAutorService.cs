using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model.Models;

namespace Domain.Model.Interfaces.Services
{
    public interface IAutorService
    {
        Task<IEnumerable<AutorModel>> GetAllAsync(string search);
        Task<AutorModel> GetByIdAsync(int id);
        Task<int> AddAsync(AutorModel autorModel);
        Task EditAsync(AutorModel autorModel);
        Task RemoveAsync(AutorModel autorModel);
    }
}

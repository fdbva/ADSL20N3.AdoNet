using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Domain.Model.Models;

namespace Domain.Model.Interfaces.Repositories
{
    public interface IAutorRepository
    {
        Task<IEnumerable<AutorModel>> GetAllAsync(string search);
        Task<AutorModel> GetByIdAsync(int id);
        Task<(int autorId, SqlConnection sqlConnection, SqlTransaction sqlTransaction)> AddAsync(AutorModel autorModel);
        Task EditAsync(AutorModel autorModel);
        Task RemoveAsync(AutorModel autorModel);
    }
}

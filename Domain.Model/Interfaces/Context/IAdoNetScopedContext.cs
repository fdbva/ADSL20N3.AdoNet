using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Domain.Model.Interfaces.Context
{
    public interface IAdoNetScopedContext : IAsyncDisposable
    {
        SqlCommand CreateCommand();
        Task BeginTransactionAsync();
        Task CommitAsync();
    }
}

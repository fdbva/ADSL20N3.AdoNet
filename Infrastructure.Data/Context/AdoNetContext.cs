using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Domain.Model.Interfaces.Context;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data.Context
{
    public class AdoNetScopedContext : IAdoNetScopedContext
    {
        private SqlConnection _sqlConnection;
        private DbTransaction _sqlTransaction;
        private bool _commited = false;

        public AdoNetScopedContext(
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("BibliotecaDatabase");

            _sqlConnection = new SqlConnection(connectionString);
            _sqlConnection.Open();
        }

        public async Task BeginTransactionAsync()
        {
            _sqlTransaction = await _sqlConnection.BeginTransactionAsync();
            _commited = false;
        }

        public SqlCommand CreateCommand()
        {
            var command = _sqlConnection.CreateCommand();

            if (_sqlTransaction != null)
            {
                command.Transaction = (SqlTransaction)_sqlTransaction;
            }

            return command;
        }

        public async Task CommitAsync()
        {
            if (_sqlTransaction == null)
            {
                throw new InvalidOperationException("Transaction have already been already been commited. Check your transaction handling.");
            }
            await _sqlTransaction.CommitAsync();
            _commited = true;
        }

        public async ValueTask DisposeAsync()
        {
            if (_sqlTransaction != null)
            {
                if (!_commited)
                {
                    await _sqlTransaction.RollbackAsync();
                }

                await _sqlTransaction.DisposeAsync();
                _sqlTransaction = null;
            }

            if (_sqlConnection != null)
            {
                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
                _sqlConnection = null;
            }
        }
    }
}

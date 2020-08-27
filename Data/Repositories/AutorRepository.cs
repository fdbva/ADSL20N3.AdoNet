using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace Data.Repositories
{
    public class AutorRepository
    {
        private static string _connectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ADSL20N3;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static List<AutorModel> Autores { get; } = new List<AutorModel>();

        public IEnumerable<AutorModel> GetAll()
        {
            const string commandText =
                "SELECT Id, Nome, UltimoNome, Nascimento FROM Autor";

            using var sqlConnection = new SqlConnection(_connectionString);
            using var sqlCommand = new SqlCommand(commandText, sqlConnection)
            {
                CommandType = CommandType.Text
            };

            sqlConnection.Open();

            var reader = sqlCommand.ExecuteReader();

            var autores = new List<AutorModel>();
            while (reader.Read())
            {
                var id = reader.GetFieldValue<int>(0);
                var nome = reader.GetFieldValue<string>(1);
                var ultimoNome = reader.GetFieldValue<string>(2);
                var nascimento = reader.GetFieldValue<DateTime>(3);
                var autorModel = new AutorModel
                {
                    Id = id,
                    Nome = nome,
                    UltimoNome = ultimoNome,
                    Nascimento = nascimento
                };
                autores.Add(autorModel);
            }
            return autores;
        }

        public async Task<IEnumerable<AutorModel>> GetAllAsync()
        {
            const string commandText = 
                "SELECT Id, Nome, UltimoNome, Nascimento FROM Autor";

            await using var sqlConnection = new SqlConnection(_connectionString);
            await using var sqlCommand = new SqlCommand(commandText, sqlConnection)
            {
                CommandType = CommandType.Text
            };

            await sqlConnection.OpenAsync();
            var reader = await sqlCommand.ExecuteReaderAsync();

            var autores = new List<AutorModel>();
            while (await reader.ReadAsync())
            {
                var id = await reader.GetFieldValueAsync<int>(0);
                var nome = await reader.GetFieldValueAsync<string>(1);
                var ultimoNome = await reader.GetFieldValueAsync<string>(2);
                var nascimento = await reader.GetFieldValueAsync<DateTime>(3);
                var autorModel = new AutorModel
                {
                    Id = id,
                    Nome = nome,
                    UltimoNome = ultimoNome,
                    Nascimento = nascimento
                };
                autores.Add(autorModel);
            }
            return autores;
        }

        public async Task<AutorModel> GetByIdAsync(int id)
        {
            const string commandText =
                "SELECT Id, Nome, UltimoNome, Nascimento FROM Autor WHERE Id = @id;";

            await using var sqlConnection = new SqlConnection(_connectionString);
            await using var sqlCommand = new SqlCommand(commandText, sqlConnection)
            {
                CommandType = CommandType.Text
            };

            sqlCommand.Parameters
                .Add("@id", SqlDbType.Int)
                .Value = id;

            await sqlConnection.OpenAsync();
            var reader = await sqlCommand.ExecuteReaderAsync();

            var canRead = await reader.ReadAsync();
            if (!canRead)
                return null;

            var autor = new AutorModel
            {
                Id = await reader.GetFieldValueAsync<int>(0),
                Nome = await reader.GetFieldValueAsync<string>(1),
                UltimoNome = await reader.GetFieldValueAsync<string>(2),
                Nascimento = await reader.GetFieldValueAsync<DateTime>(3)
            };
            return autor;
        }

        public void Add(AutorModel autorModel)
        {
            Autores.Add(autorModel);
        }

        public async Task EditAsync(AutorModel autorModel)
        {
            var autorInMemory = await GetByIdAsync(autorModel.Id);

            autorInMemory.Nome = autorModel.Nome;
            autorInMemory.UltimoNome = autorModel.UltimoNome;
            autorInMemory.Nascimento = autorModel.Nascimento;
        }

        public async Task RemoveAsync(AutorModel autorModel)
        {
            var autorInMemory = await GetByIdAsync(autorModel.Id);

            Autores.Remove(autorInMemory);
        }
    }
}

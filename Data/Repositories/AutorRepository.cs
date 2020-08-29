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

            var idColumnIndex = reader.GetOrdinal("Id");
            var nomeColumnIndex = reader.GetOrdinal("Nome");
            var ultimoNomeColumnIndex = reader.GetOrdinal("UltimoNome");
            var nascimentoColumnIndex = reader.GetOrdinal("Nascimento");

            var autores = new List<AutorModel>();
            while (await reader.ReadAsync())
            {
                var id = await reader.GetFieldValueAsync<int>(idColumnIndex);
                var nome = await reader.GetFieldValueAsync<string>(nomeColumnIndex);
                var ultimoNome = await reader.GetFieldValueAsync<string>(ultimoNomeColumnIndex);
                var nascimento = await reader.GetFieldValueAsync<DateTime>(nascimentoColumnIndex);
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

        public async Task<int> AddAsync(AutorModel autorModel)
        {
            const string commandText =
@"INSERT INTO Autor
	(Nome, UltimoNome, Nascimento)
    OUTPUT INSERTED.Id
	VALUES (@nome, @ultimoNome, @nascimento);";

            await using var sqlConnection = new SqlConnection(_connectionString);
            await using var sqlCommand = new SqlCommand(commandText, sqlConnection)
            {
                CommandType = CommandType.Text
            };

            sqlCommand.Parameters
                .Add("@nome", SqlDbType.NVarChar)
                .Value = autorModel.Nome;
            sqlCommand.Parameters
                .Add("@ultimoNome", SqlDbType.NVarChar)
                .Value = autorModel.UltimoNome;
            sqlCommand.Parameters
                .Add("@nascimento", SqlDbType.DateTime2)
                .Value = autorModel.Nascimento;

            await sqlConnection.OpenAsync();

            var outputId = (int)await sqlCommand.ExecuteScalarAsync();

            return outputId;
        }

        public async Task EditAsync(AutorModel autorModel)
        {
            const string commandText =
@"UPDATE Autor
	SET Nome = @nome, UltimoNome = @ultimoNome, Nascimento = @nascimento
	WHERE Id = @id;";

            await using var sqlConnection = new SqlConnection(_connectionString);
            await using var sqlCommand = new SqlCommand(commandText, sqlConnection)
            {
                CommandType = CommandType.Text
            };

            sqlCommand.Parameters
                .Add("@nome", SqlDbType.NVarChar)
                .Value = autorModel.Nome;
            sqlCommand.Parameters
                .Add("@ultimoNome", SqlDbType.NVarChar)
                .Value = autorModel.UltimoNome;
            sqlCommand.Parameters
                .Add("@nascimento", SqlDbType.DateTime2)
                .Value = autorModel.Nascimento;
            sqlCommand.Parameters
                .Add("@id", SqlDbType.Int)
                .Value = autorModel.Id;

            await sqlConnection.OpenAsync();

            await sqlCommand.ExecuteScalarAsync();
        }

        public async Task RemoveAsync(AutorModel autorModel)
        {
            const string commandText = "DELETE FROM Autor WHERE Id = @id";

            await using var sqlConnection = new SqlConnection(_connectionString);
            await using var sqlCommand = new SqlCommand(commandText, sqlConnection)
            {
                CommandType = CommandType.Text
            };

            sqlCommand.Parameters
                .Add("@id", SqlDbType.Int)
                .Value = autorModel.Id;

            await sqlConnection.OpenAsync();

            await sqlCommand.ExecuteScalarAsync();
        }
    }
}

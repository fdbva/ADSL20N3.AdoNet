using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Domain.Models;

namespace Data.Repositories
{
    public class LivroSqlRepository
    {
        private static string _connectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ADSL20N3;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public async Task<IEnumerable<LivroModel>> GetAllAsync()
        {
            const string commandText =
                "SELECT Id, Titulo, Isbn, Publicacao, AutorId FROM Livro";

            await using var sqlConnection = new SqlConnection(_connectionString);
            await using var sqlCommand = new SqlCommand(commandText, sqlConnection)
            {
                CommandType = CommandType.Text
            };

            await sqlConnection.OpenAsync();
            var reader = await sqlCommand.ExecuteReaderAsync();

            var idColumnIndex = reader.GetOrdinal("Id");
            var nomeColumnIndex = reader.GetOrdinal("Titulo");
            var isbnColumnIndex = reader.GetOrdinal("Isbn");
            var publicacaoColumnIndex = reader.GetOrdinal("Publicacao");
            var autorIdColumnIndex = reader.GetOrdinal("AutorId");

            var autores = new List<LivroModel>();
            while (await reader.ReadAsync())
            {
                var id = await reader.GetFieldValueAsync<int>(idColumnIndex);
                var nome = await reader.GetFieldValueAsync<string>(nomeColumnIndex);
                var isbn = await reader.GetFieldValueAsync<string>(isbnColumnIndex);
                var publicacao = await reader.GetFieldValueAsync<DateTime>(publicacaoColumnIndex);
                var autorId = await reader.GetFieldValueAsync<int>(autorIdColumnIndex);
                var autorModel = new LivroModel
                {
                    Id = id,
                    Titulo = nome,
                    Isbn = isbn,
                    Publicacao = publicacao,
                    AutorId = autorId
                };
                autores.Add(autorModel);
            }
            return autores;
        }

        public async Task<LivroModel> GetByIdAsync(int id)
        {
            const string commandText =
                "SELECT Id, Titulo, Isbn, Publicacao, AutorId FROM Livro WHERE Id = @id;";

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

            var autor = new LivroModel
            {
                Id = await reader.GetFieldValueAsync<int>(0),
                Titulo = await reader.GetFieldValueAsync<string>(1),
                Isbn = await reader.GetFieldValueAsync<string>(2),
                Publicacao = await reader.GetFieldValueAsync<DateTime>(3),
                AutorId = await reader.GetFieldValueAsync<int>(4),
            };
            return autor;
        }

        public async Task<int> AddAsync(LivroModel autorModel)
        {
            const string commandText =
@"INSERT INTO Livro
	(Titulo, Isbn, Publicacao, AutorId)
    OUTPUT INSERTED.Id
	VALUES (@titulo, @isbn, @publicacao, @autorId);";

            await using var sqlConnection = new SqlConnection(_connectionString);
            await using var sqlCommand = new SqlCommand(commandText, sqlConnection)
            {
                CommandType = CommandType.Text
            };

            sqlCommand.Parameters
                .Add("@titulo", SqlDbType.NVarChar)
                .Value = autorModel.Titulo;
            sqlCommand.Parameters
                .Add("@isbn", SqlDbType.NVarChar)
                .Value = autorModel.Isbn;
            sqlCommand.Parameters
                .Add("@publicacao", SqlDbType.DateTime2)
                .Value = autorModel.Publicacao;
            sqlCommand.Parameters
                .Add("@autorId", SqlDbType.Int)
                .Value = autorModel.AutorId;

            await sqlConnection.OpenAsync();

            var outputId = (int)await sqlCommand.ExecuteScalarAsync();

            return outputId;
        }

        public async Task EditAsync(LivroModel autorModel)
        {
            const string commandText =
@"UPDATE Livro
	SET Titulo = @titulo, Isbn = @isbn, Publicacao = @publicacao, AutorId = @autorId
	WHERE Id = @id;";

            await using var sqlConnection = new SqlConnection(_connectionString);
            await using var sqlCommand = new SqlCommand(commandText, sqlConnection)
            {
                CommandType = CommandType.Text
            };

            sqlCommand.Parameters
                .Add("@titulo", SqlDbType.NVarChar)
                .Value = autorModel.Titulo;
            sqlCommand.Parameters
                .Add("@isbn", SqlDbType.NVarChar)
                .Value = autorModel.Isbn;
            sqlCommand.Parameters
                .Add("@publicacao", SqlDbType.DateTime2)
                .Value = autorModel.Publicacao;
            sqlCommand.Parameters
                .Add("@autorId", SqlDbType.Int)
                .Value = autorModel.AutorId;
            sqlCommand.Parameters
                .Add("@id", SqlDbType.Int)
                .Value = autorModel.Id;

            await sqlConnection.OpenAsync();

            await sqlCommand.ExecuteScalarAsync();
        }

        public async Task RemoveAsync(LivroModel autorModel)
        {
            const string commandText = "DELETE FROM Livro WHERE Id = @id";

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

using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Domain.Model.Interfaces.Context;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Models;

namespace Infrastructure.Data.Repositories
{
    public class AutorSqlRepository : IAutorRepository
    {
        private readonly IAdoNetScopedContext _adoNetScopedContext;

        public AutorSqlRepository(
            IAdoNetScopedContext adoNetScopedContext)
        {
            _adoNetScopedContext = adoNetScopedContext;
        }

        public async Task<IEnumerable<AutorModel>> GetAllAsync(string search)
        {
            var commandText = 
                "SELECT Id, Nome, UltimoNome, Nascimento FROM Autor";

            var searchHasValue = !string.IsNullOrWhiteSpace(search);
            if (searchHasValue)
            {
                commandText += " WHERE Nome LIKE @search OR UltimoNome Like @search";
            }

            var sqlCommand = _adoNetScopedContext.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = commandText;

            if (searchHasValue)
            {
                sqlCommand.Parameters
                    .Add("@search", SqlDbType.NVarChar)
                    .Value = $"%{search}%";
            }
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

            var sqlCommand = _adoNetScopedContext.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = commandText;

            sqlCommand.Parameters
                .Add("@id", SqlDbType.Int)
                .Value = id;

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

        public async Task<int> AddAsync(
            AutorModel autorModel)
        {
            const string commandText =
@"INSERT INTO Autor
	(Nome, UltimoNome, Nascimento)
    OUTPUT INSERTED.Id
	VALUES (@nome, @ultimoNome, @nascimento);";

            var sqlCommand = _adoNetScopedContext.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = commandText;

            sqlCommand.Parameters
                .Add("@nome", SqlDbType.NVarChar)
                .Value = autorModel.Nome;
            sqlCommand.Parameters
                .Add("@ultimoNome", SqlDbType.NVarChar)
                .Value = autorModel.UltimoNome;
            sqlCommand.Parameters
                .Add("@nascimento", SqlDbType.DateTime2)
                .Value = autorModel.Nascimento;

            var outputId = (int)await sqlCommand.ExecuteScalarAsync();

            return outputId;
        }

        public async Task EditAsync(AutorModel autorModel)
        {
            const string commandText =
@"UPDATE Autor
	SET Nome = @nome, UltimoNome = @ultimoNome, Nascimento = @nascimento
	WHERE Id = @id;";

            var sqlCommand = _adoNetScopedContext.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = commandText;

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

            await sqlCommand.ExecuteScalarAsync();
        }

        public async Task RemoveAsync(AutorModel autorModel)
        {
            const string commandText = "DELETE FROM Autor WHERE Id = @id";

            var sqlCommand = _adoNetScopedContext.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = commandText;

            sqlCommand.Parameters
                .Add("@id", SqlDbType.Int)
                .Value = autorModel.Id;

            await sqlCommand.ExecuteScalarAsync();
        }
    }
}

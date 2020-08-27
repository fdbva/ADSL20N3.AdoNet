using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
            const string commandText = "SELECT * FROM Autor";

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

        public AutorModel GetById(int id)
        {
            var autor = Autores.First(x => x.Id == id);

            return autor;
        }

        public void Add(AutorModel autorModel)
        {
            Autores.Add(autorModel);
        }

        public void Edit(AutorModel autorModel)
        {
            var autorInMemory = GetById(autorModel.Id);

            autorInMemory.Nome = autorModel.Nome;
            autorInMemory.UltimoNome = autorModel.UltimoNome;
            autorInMemory.Nascimento = autorModel.Nascimento;
        }

        public void Remove(AutorModel autorModel)
        {
            var autorInMemory = GetById(autorModel.Id);

            Autores.Remove(autorInMemory);
        }
    }
}

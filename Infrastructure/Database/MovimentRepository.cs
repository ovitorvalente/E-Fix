using Dapper;
using E_Fix.Application.Interfaces;
using E_Fix.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace E_Fix.Infrastructure.Database
{
    public class MovimentRepository : IMovimentRepository
    {
        private readonly string _connectionString;

        public MovimentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Moviment> GetMoviments()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = @"SELECT 
                                    Data,
                                    Ide,
                                    Sequencia,
                                    Total_Final
                                 FROM Movimento
                                 WHERE Total_Final BETWEEN 0.01 AND 0.10
                                ";
                return connection.Query<Moviment>(query).ToList();
            }
        }

        public void UpdateMoviments(List<int> sequencias)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Movimento SET Total_Final = 0 WHERE Sequencia IN @sequencias";
                connection.Execute(query, new { Sequencias = sequencias});
            }
        }
    }
}

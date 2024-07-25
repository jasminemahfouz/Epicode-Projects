using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Hotels.Models;

namespace Hotels.DAO
{
    public class CameraDao : ICameraDao
    {
        private readonly string _connectionString;

        public CameraDao(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Camera>> GetAllAsync()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                const string query = "SELECT * FROM Camere";
                return await conn.QueryAsync<Camera>(query);
            }
        }

        public async Task<Camera> GetByIdAsync(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                const string query = "SELECT * FROM Camere WHERE Id = @Id";
                return await conn.QueryFirstOrDefaultAsync<Camera>(query, new { Id = id });
            }
        }

        public async Task AddAsync(Camera camera)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                const string query = @"
                    INSERT INTO Camere (Descrizione, Tipologia, Prezzo)
                    VALUES (@Descrizione, @Tipologia, @Prezzo)";
                await conn.ExecuteAsync(query, camera);
            }
        }

        public async Task UpdateAsync(Camera camera)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                const string query = @"
                    UPDATE Camere SET 
                    Descrizione = @Descrizione, 
                    Tipologia = @Tipologia,
                    Prezzo = @Prezzo
                    WHERE Id = @Id";
                await conn.ExecuteAsync(query, camera);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                const string query = "DELETE FROM Camere WHERE Id = @Id";
                await conn.ExecuteAsync(query, new { Id = id });
            }
        }
    }
}

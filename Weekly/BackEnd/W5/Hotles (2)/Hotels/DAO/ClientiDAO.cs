using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Hotels.Models;

namespace Hotels.DAO
{
    public class ClienteDao : IClienteDao
    {
        private readonly string _connectionString;

        public ClienteDao(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                const string query = "SELECT * FROM Clienti";
                return await conn.QueryAsync<Cliente>(query);
            }
        }

        public async Task<Cliente> GetByIdAsync(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                const string query = "SELECT * FROM Clienti WHERE id = @id";
                return await conn.QueryFirstOrDefaultAsync<Cliente>(query, new { id });
            }
        }

        public async Task AddAsync(Cliente cliente)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                const string query = @"
                    INSERT INTO Clienti (codice_fiscale, cognome, nome, città, provincia, email, cellulare)
                    VALUES (@CodiceFiscale, @Cognome, @Nome, @Citta, @Provincia, @Email, @Cellulare)";
                await conn.ExecuteAsync(query, cliente);
            }
        }

        public async Task UpdateAsync(Cliente cliente)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                const string query = @"
                    UPDATE Clienti SET 
                    codice_fiscale = @CodiceFiscale, 
                    cognome = @Cognome, 
                    nome = @Nome, 
                    città = @Citta, 
                    provincia = @Provincia, 
                    email = @Email, 
                    cellulare = @Cellulare
                    WHERE id = @Id";
                await conn.ExecuteAsync(query, cliente);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                const string query = "DELETE FROM Clienti WHERE id = @id";
                await conn.ExecuteAsync(query, new { id });
            }
        }
    }
}

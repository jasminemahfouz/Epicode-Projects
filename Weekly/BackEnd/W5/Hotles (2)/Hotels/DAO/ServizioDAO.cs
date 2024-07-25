using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Hotels.Models;

namespace Hotels.DAO
{
    public class ServizioDao : IServizioDao
    {
        private readonly string _connectionString;

        public ServizioDao(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Servizio>> GetAllAsync()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                const string query = "SELECT * FROM Servizi";
                return await conn.QueryAsync<Servizio>(query);
            }
        }

        public async Task<Servizio> GetByIdAsync(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                const string query = "SELECT * FROM Servizi WHERE id = @id";
                return await conn.QueryFirstOrDefaultAsync<Servizio>(query, new { id });
            }
        }

        public async Task AddAsync(Servizio servizio)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                const string query = @"
                    INSERT INTO Servizi (descrizione, prezzo)
                    VALUES (@Descrizione, @Prezzo)";
                await conn.ExecuteAsync(query, servizio);
            }
        }

        public async Task UpdateAsync(Servizio servizio)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                const string query = @"
                    UPDATE Servizi SET 
                    descrizione = @Descrizione, 
                    prezzo = @Prezzo
                    WHERE id = @Id";
                await conn.ExecuteAsync(query, servizio);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                const string query = "DELETE FROM Servizi WHERE id = @id";
                await conn.ExecuteAsync(query, new { id });
            }
        }

        public async Task<IEnumerable<Servizio>> GetByPrenotazioneIdAsync(int prenotazioneId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                const string query = @"
                    SELECT s.*
                    FROM Servizi s
                    JOIN Prenotazioni_Servizi ps ON s.id = ps.servizio_id
                    WHERE ps.prenotazione_id = @prenotazione_id";
                return await conn.QueryAsync<Servizio>(query, new { prenotazione_id = prenotazioneId });
            }
        }
    }
}

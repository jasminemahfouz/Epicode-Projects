using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Hotels.Models;

namespace Hotels.DAO
{
    public class PrenotazioneDao : IPrenotazioneDao
    {
        private readonly string _connectionString;

        public PrenotazioneDao(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Prenotazione>> GetAllAsync()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                const string query = @"
                    SELECT p.*, c.id AS cliente_id, c.cognome, c.nome, cam.id AS camera_id, cam.descrizione, cam.prezzo 
                    FROM Prenotazioni p
                    JOIN Clienti c ON p.cliente_id = c.id
                    JOIN Camere cam ON p.camera_id = cam.id";
                var prenotazioni = await conn.QueryAsync<Prenotazione, Cliente, Camera, Prenotazione>(
                    query,
                    (prenotazione, cliente, camera) =>
                    {
                        prenotazione.Cliente = cliente;
                        prenotazione.Camera = camera;
                        return prenotazione;
                    },
                    splitOn: "cliente_id,camera_id"
                );
                return prenotazioni;
            }
        }

        public async Task<Prenotazione> GetByIdAsync(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                const string query = @"
                    SELECT p.*, c.id AS cliente_id, c.cognome, c.nome, cam.id AS camera_id, cam.descrizione, cam.prezzo 
                    FROM Prenotazioni p
                    JOIN Clienti c ON p.cliente_id = c.id
                    JOIN Camere cam ON p.camera_id = cam.id
                    WHERE p.id = @id";
                var prenotazione = await conn.QueryAsync<Prenotazione, Cliente, Camera, Prenotazione>(
                    query,
                    (prenotazione, cliente, camera) =>
                    {
                        prenotazione.Cliente = cliente;
                        prenotazione.Camera = camera;
                        return prenotazione;
                    },
                    new { id },
                    splitOn: "cliente_id,camera_id"
                );
                return prenotazione.FirstOrDefault();
            }
        }

        public async Task AddAsync(Prenotazione prenotazione)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                const string query = @"
                    INSERT INTO Prenotazioni (data_prenotazione, numero_progressivo, anno, dal, al, caparra, tariffa, tipologia_soggiorno, cliente_id, camera_id)
                    VALUES (@DataPrenotazione, @NumeroProgressivo, @Anno, @Dal, @Al, @Caparra, @Tariffa, @TipologiaSoggiorno, @ClienteId, @CameraId);
                    SELECT CAST(SCOPE_IDENTITY() as int)";
                prenotazione.Id = await conn.QuerySingleAsync<int>(query, prenotazione);
                await UpdateServiziAsync(prenotazione.Id, prenotazione.ServiziSelezionati);
            }
        }

        public async Task UpdateAsync(Prenotazione prenotazione)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                const string query = @"
                    UPDATE Prenotazioni
                    SET data_prenotazione = @DataPrenotazione,
                        numero_progressivo = @NumeroProgressivo,
                        anno = @Anno,
                        dal = @Dal,
                        al = @Al,
                        caparra = @Caparra,
                        tariffa = @Tariffa,
                        tipologia_soggiorno = @TipologiaSoggiorno,
                        cliente_id = @ClienteId,
                        camera_id = @CameraId
                    WHERE id = @Id";
                await conn.ExecuteAsync(query, prenotazione);
                await UpdateServiziAsync(prenotazione.Id, prenotazione.ServiziSelezionati);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        const string deleteServiziQuery = "DELETE FROM Prenotazioni_Servizi WHERE prenotazione_id = @prenotazione_id";
                        await conn.ExecuteAsync(deleteServiziQuery, new { prenotazione_id = id }, transaction);

                        const string deleteQuery = "DELETE FROM Prenotazioni WHERE Id = @Id";
                        await conn.ExecuteAsync(deleteQuery, new { Id = id }, transaction);

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public async Task<int> GetLastIdAsync()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                const string query = "SELECT ISNULL(MAX(Id), 0) FROM Prenotazioni";
                return await conn.QuerySingleAsync<int>(query);
            }
        }

        public async Task UpdateServiziAsync(int prenotazioneId, List<int> serviziSelezionati)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        const string deleteQuery = "DELETE FROM Prenotazioni_Servizi WHERE prenotazione_id = @prenotazione_id";
                        await conn.ExecuteAsync(deleteQuery, new { prenotazione_id = prenotazioneId }, transaction);

                        if (serviziSelezionati != null && serviziSelezionati.Count > 0)
                        {
                            const string insertQuery = "INSERT INTO Prenotazioni_Servizi (prenotazione_id, servizio_id) VALUES (@prenotazione_id, @servizio_id)";
                            foreach (var servizioId in serviziSelezionati)
                            {
                                await conn.ExecuteAsync(insertQuery, new { prenotazione_id = prenotazioneId, servizio_id = servizioId }, transaction);
                            }
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}

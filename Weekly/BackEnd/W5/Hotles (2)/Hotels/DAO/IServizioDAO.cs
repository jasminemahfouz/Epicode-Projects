using System.Collections.Generic;
using System.Threading.Tasks;
using Hotels.Models;

namespace Hotels.DAO
{
    public interface IServizioDao
    {
        Task<IEnumerable<Servizio>> GetAllAsync();
        Task<Servizio> GetByIdAsync(int id);
        Task AddAsync(Servizio servizio);
        Task UpdateAsync(Servizio servizio);
        Task DeleteAsync(int id);
        Task<IEnumerable<Servizio>> GetByPrenotazioneIdAsync(int prenotazioneId);
    }
}

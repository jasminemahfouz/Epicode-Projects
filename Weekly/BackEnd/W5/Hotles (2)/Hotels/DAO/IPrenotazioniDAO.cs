using System.Collections.Generic;
using System.Threading.Tasks;
using Hotels.Models;

namespace Hotels.DAO
{
    public interface IPrenotazioneDao
    {
        Task<IEnumerable<Prenotazione>> GetAllAsync();
        Task<Prenotazione> GetByIdAsync(int id);
        Task AddAsync(Prenotazione prenotazione);
        Task UpdateAsync(Prenotazione prenotazione);
        Task DeleteAsync(int id);
        Task<int> GetLastIdAsync();
        Task UpdateServiziAsync(int prenotazioneId, List<int> serviziSelezionati);
    }
}

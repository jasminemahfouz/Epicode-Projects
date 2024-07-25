using Hotels.Models;
using System.Collections.Generic;

namespace Hotels.DAO
{
    public interface IPrenotazioneDao
    {
        IEnumerable<Prenotazione> GetAll();
        Prenotazione GetById(int id);
        void Add(Prenotazione prenotazione);
        void Update(Prenotazione prenotazione);
        void Delete(int id);
        int GetLastId();
        void UpdateServizi(int prenotazioneId, List<int> serviziSelezionati);
    }
}

using System.Collections.Generic;

namespace CinemaMultisala.Models
{
    public class Sala
    {
        public string Nome { get; set; }
        public int Capienza { get; set; } = 120;
        public int BigliettiVenduti { get; set; }
        public int BigliettiRidotti { get; set; }

        public List<Biglietto> Biglietti { get; set; } = new List<Biglietto>();
    }
}

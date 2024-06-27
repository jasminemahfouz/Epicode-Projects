namespace CinemaMultisala.Models
{
    public class Biglietto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Sala { get; set; }
        public string TipoBiglietto { get; set; } // "Intero" o "Ridotto"
    }
}

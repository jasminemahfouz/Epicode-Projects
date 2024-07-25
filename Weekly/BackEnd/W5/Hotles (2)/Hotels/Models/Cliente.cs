using System.ComponentModel.DataAnnotations;

namespace Hotels.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required]
        [StringLength(16)]
        public string CodiceFiscale { get; set; }

        [Required]
        [StringLength(50)]
        public string Cognome { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        [StringLength(50)]
        public string Citta { get; set; }

        [Required]
        [StringLength(50)]
        public string Provincia { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Telefono { get; set; }

        [Phone]
        public string Cellulare { get; set; }
    }
}

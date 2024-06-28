
using System.ComponentModel.DataAnnotations;

namespace ScarpeCo.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string CoverImage { get; set; }

        [Required]
        public string AdditionalImage1 { get; set; }

        [Required]
        public string AdditionalImage2 { get; set; }

        public bool IsPurchased { get; set; } 
    }
}

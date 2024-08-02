using System.ComponentModel.DataAnnotations.Schema;

namespace Pizzacode.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public TimeSpan DeliveryTime { get; set; }
        public string Ingredients { get; set; }
    }
}

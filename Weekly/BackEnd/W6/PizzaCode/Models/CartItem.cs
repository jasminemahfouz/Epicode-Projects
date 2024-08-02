namespace Pizzacode.Models
{
    public class CartItem
    {
        public int PizzaId { get; set; }
        public Pizza Pizza { get; set; }
        public int Quantity { get; set; }
    }
}
namespace Pizzacode.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } // In un'app reale, utilizzare hashing sicuro
        public string Role { get; set; } // "Admin" o "Customer"
    }
}
using Hotels.Models;
using Hotels.Services;
using System.Collections.Generic;
using System.Linq;

namespace Hotels.Services
{
    public class AuthService : IAuthService
    {
        private readonly List<User> _users = new List<User>
        {
            new User { Id = 1, Username = "marco", Password = "marco", Role = "admin" },
        };

        public User Login(string username, string password)
        {
            return _users.SingleOrDefault(u => u.Username == username && u.Password == password);
        }
    }
}

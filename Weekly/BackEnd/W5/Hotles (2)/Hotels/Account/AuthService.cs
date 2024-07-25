using Hotels.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotels.Services
{
    public class AuthService : IAuthService
    {
        private readonly List<User> _users = new List<User>
        {
            new User { Id = 1, Username = "marco", Password = "hashed_marco_password", Role = "admin" },
        };

        private readonly ILogger<AuthService> _logger;

        public AuthService(ILogger<AuthService> logger)
        {
            _logger = logger;
        }

        public async Task<User> LoginAsync(string username, string password)
        {
            // Simulating asynchronous operation
            await Task.Delay(100);

            var hashedPassword = HashPassword(password);
            var user = _users.SingleOrDefault(u => u.Username == username && u.Password == hashedPassword);

            if (user != null)
            {
                _logger.LogInformation($"User {username} logged in successfully.");
            }
            else
            {
                _logger.LogWarning($"Invalid login attempt for user {username}.");
            }

            return user;
        }

        private string HashPassword(string password)
        {
            // Implement a real hashing method here
            // This is just a placeholder for demonstration purposes
            return "hashed_" + password;
        }
    }
}

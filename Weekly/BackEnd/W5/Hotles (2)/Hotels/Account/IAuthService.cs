using Hotels.Models;
using System.Threading.Tasks;

namespace Hotels.Services
{
    public interface IAuthService
    {
        Task<User> LoginAsync(string username, string password);
    }
}

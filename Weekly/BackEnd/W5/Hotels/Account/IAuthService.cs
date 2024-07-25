using Hotels.Models;

namespace Hotels.Services
{
    public interface IAuthService
    {
        User Login(string username, string password);
    }
}

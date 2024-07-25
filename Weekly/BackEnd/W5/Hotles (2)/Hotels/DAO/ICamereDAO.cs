using System.Collections.Generic;
using System.Threading.Tasks;
using Hotels.Models;

namespace Hotels.DAO
{
    public interface ICameraDao
    {
        Task<IEnumerable<Camera>> GetAllAsync();
        Task<Camera> GetByIdAsync(int id);
        Task AddAsync(Camera camera);
        Task UpdateAsync(Camera camera);
        Task DeleteAsync(int id);
    }
}

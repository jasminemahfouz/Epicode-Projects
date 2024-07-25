using System.Collections.Generic;
using System.Threading.Tasks;
using Hotels.Models;

namespace Hotels.DAO
{
    public interface IClienteDao
    {
        Task<IEnumerable<Cliente>> GetAllAsync();
        Task<Cliente> GetByIdAsync(int id);
        Task AddAsync(Cliente cliente);
        Task UpdateAsync(Cliente cliente);
        Task DeleteAsync(int id);
    }
}

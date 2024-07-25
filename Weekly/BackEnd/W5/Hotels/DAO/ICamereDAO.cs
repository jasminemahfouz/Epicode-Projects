using System.Collections.Generic;
using Hotels.Models;

namespace Hotels.DAO
{
    public interface ICameraDao
    {
        IEnumerable<Camera> GetAll();
        Camera GetById(int id);
        void Add(Camera camera);
        void Update(Camera camera);
        void Delete(int id);
    }
}

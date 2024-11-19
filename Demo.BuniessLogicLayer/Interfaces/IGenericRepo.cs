using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.BuniessLogicLayer.Interfaces
{
    public interface IGenericRepo<T>
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetAsync(int id);

        Task AddAsync(T item);

        void Update(T item);

        void Delete(T item);
    }
}
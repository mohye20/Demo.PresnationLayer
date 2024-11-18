using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BuniessLogicLayer.Interfaces
{
    public interface IGenericRepo<T>
    {
        IEnumerable<T> GetAll();

        T Get(int id);

        void Add(T item);

        void Update(T item);

        void Delete(T item);
    }
}
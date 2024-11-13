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

        int Add(T item);

        int Update(T item);

        int Delete(int id);
    }
}
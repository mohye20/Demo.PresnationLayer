using Demo.BuniessLogicLayer.Interfaces;
using Demo.DataAcessLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BuniessLogicLayer.Repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly MVCAppDbContext _dbContext;

        //public GenericRepo() not valid
        //{
            
        //}
        public GenericRepo(MVCAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Add(T item)
        {
            _dbContext.Add(item);
            return _dbContext.SaveChanges();
        }

        public int Delete(int item)
        {
            _dbContext.Remove(item);
            return _dbContext.SaveChanges();
        }

        public T Get(int id)

        => _dbContext.Set<T>().Find(id);


        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().ToList();
        }

        public int Update(T item)
        {
            _dbContext.Update(item);
            return _dbContext.SaveChanges();
        }
    }
}
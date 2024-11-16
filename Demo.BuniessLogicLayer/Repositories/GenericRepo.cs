using Demo.BuniessLogicLayer.Interfaces;
using Demo.DataAcessLayer.Context;
using Demo.DataAcessLayer.Models;
using Microsoft.EntityFrameworkCore;
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

        public int Delete(T item)
        {
            _dbContext.Remove(item);
            return _dbContext.SaveChanges();
        }

        public T Get(int id)

        => _dbContext.Set<T>().Find(id);


        public IEnumerable<T> GetAll()
        {

            if (typeof(T) == typeof(Employee))
                return (IEnumerable<T>) _dbContext.Employees.Include(E => E.Department).ToList();

            return _dbContext.Set<T>().ToList();
        }

        public int Update(T item)
        {
            _dbContext.Update(item);
            return _dbContext.SaveChanges();
        }
    }
}